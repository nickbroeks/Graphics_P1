using System;
using System.Threading.Tasks;
using OpenTK;

namespace Template {

    class RayTracer {
        public Surface screen;
        public Camera camera;
        public Scene scene;
        public float time;
        private readonly FpsMonitor fpsm;
        private readonly bool multithread, rotate;

        public RayTracer()
        { 
            scene = new Scene();
            camera = new Camera(
                new Vector3(1f, 4f, 5f),
                new Vector3(2, -3, -3),
                Vector3.UnitY, 80f);
            screen = new Surface(800, 400);
            
            fpsm = new FpsMonitor();
            time = 1.4f;
            scene.PreProcess();
            multithread = true;
            rotate = true;
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

        }
        public int SceneToScreenX(float x) { return (int)((screen.width - 1) * (x) / 22); }
        public int SceneToScreenY(float y) { return (int)((screen.height - 1) * (y) / 11); }
        public float ScreenToSceneX(int x) { return x * 22f / (screen.width - 1); }
        public float ScreenToSceneY(int y) { return y * 11f / (screen.height - 1); }
        public void Render()
        {
            fpsm.Update();
            screen.Clear(0);
            if (rotate) {
                time += 0.04f;
                camera.Position = 6f * new Vector3(-(float)Math.Cos(time), 0, -(float)Math.Sin(time)) + new Vector3(5, 0, 5);
                camera.LookAt = new Vector3((float)Math.Cos(time), 0, (float)Math.Sin(time));
            }
            
            if (multithread) Parallel.For(0, screen.height, y => { RenderRow(y); });
            else for (int y = 0; y < screen.height; y++) { RenderRow(y); }
            Debug();
        }
        public void RenderRow(int y)
        {
            float cameraY = (float)y / screen.height;
            for (int x = 0; x < screen.width / 2; x++) {
                bool debug = y == screen.height / 2 && x % 10 == 0;

                float cameraX = (float)x / screen.width * 2;
                Ray ray = camera.Ray(cameraX, cameraY);//TODO Instead of making 1 ray, make 9

                Color color = Trace(ray, debug, 5); //TODO Calulate 9 colors
                //TODO Color color = average of 9 colors
                screen.Plot(x + screen.width / 2, y, color.value);
            }
        }
        
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
                return intersection.collider.Ks(intersection.map) * Trace(reflect, debug, n - 1);
            }
            return scene.Illuminate(intersection, ray);
        }
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
