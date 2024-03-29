﻿using System;
using System.Threading.Tasks;
using OpenTK;

namespace Template {

    class RayTracer {
        public Surface screen;
        public Camera camera;
        public Scene scene;
        private readonly FpsMonitor fpsm;
        private readonly bool multithread = true;
        //Adjust this value change the amount of samples taken. 1 means no antialising
        private readonly int antiAliasing = 1; 

        public RayTracer()
        { 
            scene = new Scene();
            camera = new Camera(
                new Vector3(0.5f, 0f, 0.5f),
                new Vector3(1, 0, 1), 80f);
            screen = new Surface(800, 400);
            
            fpsm = new FpsMonitor();
            scene.PreProcess(); // Initialise the AABB's
        }

        public void Debug()
        {
            screen.Print($"{fpsm.value:0.00} FPS", 0, 0, Color.White.value);
            int x = SceneToScreenX(camera.Position.X);
            int y = SceneToScreenY(camera.Position.Z);
            screen.Plot(x, y, Color.White.value);

            x = SceneToScreenX(camera.topLeft.X);
            y = SceneToScreenY(camera.topLeft.Z);

            int x1 = SceneToScreenX(camera.topRight.X);
            int y1 = SceneToScreenY(camera.topRight.Z);
            screen.Line(x, y, x1, y1, Color.White.value);
            screen.Plot(x, y, Color.Red.value);

            foreach (Primitive obj in scene.objects) {
                obj.Debug(this);
            }
            foreach (Light obj in scene.lights) {
                obj.Debug(this);
            }

        }
        public int SceneToScreenX(float x) { return (int)((screen.width - 1) * (x) / 22); }
        public int SceneToScreenY(float y) { return (int)((screen.height - 1) * (y) / 11); }
        public float ScreenToSceneX(int x) { return x * 22f / (screen.width - 1); }
        public float ScreenToSceneY(int y) { return y * 11f / (screen.height - 1); }

        public void Render()
        {
            fpsm.Update();
            screen.Clear(0);
            Debug();
            if (multithread) Parallel.For(0, screen.height, y => { RenderRow(y); });
            else for (int y = 0; y < screen.height; y++) { RenderRow(y); }
            
        }
        //Render one row of the image
        public void RenderRow(int y)
        {
            float cameraY = (float)y / screen.height;
            Random rnd = new Random();
            for (int x = 0; x < screen.width / 2; x++) {
                bool shoudlBeDebugged = y == screen.height / 2 && x % 10 == 0;
                
                float cameraX = (float)x / screen.width * 2;

                Color color = Color.Black;
                if (antiAliasing > 1) {
                   
                    for (int p = 0; p < antiAliasing; p++) {
                        float randomX = (float)rnd.NextDouble() / screen.width * 2;
                        float randomY = (float)rnd.NextDouble() / screen.height;

                        Ray ray = camera.Ray(cameraX + randomX, cameraY + randomY);
                        Color newColor = Trace(ray, shoudlBeDebugged, 5);
                        color += newColor / antiAliasing;
                    }
                } else {
                    Ray ray = camera.Ray(cameraX, cameraY);
                    color = Trace(ray, shoudlBeDebugged, 5);
                }
                               
                screen.Plot(x + screen.width / 2, y, color.value);
            }
        }
        
        //Recursively find the correct color to be displayed
        public Color Trace(Ray ray, bool debug, int n)
        {
            Intersection? i = scene.Intersect(ray);
            if (i == null) return Color.Black;
            Intersection intersection = i.Value;
            if (debug) DebugRay(ray, intersection);
            if (intersection.collider.IsMirror) {
                if (n == 0) return Color.Black;
                float angle = Vector3.Dot(intersection.normal, ray.direction);
                Ray reflect = new Ray(intersection.Point, ray.direction - 2 * angle * intersection.normal);
                return scene.Illuminate(intersection) + intersection.collider.Ks(intersection.map) * Trace(reflect, debug, n - 1);
            }
            return scene.Illuminate(intersection);
        }
        ///display the given ray on the debug screen
        private void DebugRay(Ray ray, Intersection intersection)
        {
            int x = SceneToScreenX(ray.Point.X);
            if (x < 0 || x > screen.width / 2) return;
            int y = SceneToScreenY(ray.Point.Z);
            int cX = SceneToScreenX(ray.Origin.X);
            int cY = SceneToScreenY(ray.Origin.Z); 
            int nX = SceneToScreenX(ray.Point.X + intersection.normal.X / 5);
            int nY = SceneToScreenY(ray.Point.Z + intersection.normal.Z / 4);
            screen.Line(cX, cY, x, y, Color.Red.value);
            screen.Line(nX, nY, x, y, Color.Blue.value);
        }
    }
}
