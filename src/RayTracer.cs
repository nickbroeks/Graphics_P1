using System;
using OpenTK;

namespace Template {

    class RayTracer {
        public Surface screen;
        public Camera camera;
        public Scene scene;

        public RayTracer()
        {
            scene = new Scene();
            camera = new Camera();
            screen = new Surface(1000, 1000);
        }

        public void Render()
        {
            float r = 0.7f + (float)scene.random.NextDouble() * 0.3f;
            for (int y = 0; y < screen.height; y++) {
                float cameraY = (float)y / screen.height;
                for (int x = 0; x < screen.width; x++) {
                    float cameraX = (float)x / screen.width;
                    Ray ray = camera.ray(cameraX, cameraY);
                    Intersection? i = scene.Intersect(ray);
                    if (i == null) continue;
                    Intersection intersection = (Intersection)i;
                    Color color = intersection.collider.color;
                    float diff = Vector3.Dot(intersection.normal, ray.direction);
                    color *= diff;
                    screen.Plot(x, y, color.value);
                }
            }
        }
    }
}
