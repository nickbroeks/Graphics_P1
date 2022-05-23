using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Template {

    class Scene {

        private List<Primitive> objects;
        private List<Light> ligths;
        public Random random;

        public Scene ()
        {
            random = new Random();
            objects = new List<Primitive>();
            ligths = new List<Light>();
            //for (int i = 0; i < 3; i++) {
            //    float x = -10 + 20 * (float)random.NextDouble();
            //    float y = -1 + 2 * (float)random.NextDouble();
            //    float z = random.Next(2, 10);
            //    float r = 1f + 0.1f * (float)random.NextDouble();
            //    Color[] colors = new Color[] { Color.Red, Color.Green, Color.Blue };
            //    Color c = colors[random.Next(colors.Length)];
            //    objects.Add(new Sphere(new Vector3(x, y, z), r, c));
            //}
            objects.Add(new Sphere(new Vector3(0,0,0), 1, Color.Red));
            objects.Add(new Sphere(new Vector3(0, 0, 1), 1, Color.Green));
            objects.Add(new Sphere(new Vector3(0, -1, 1), 1.5f, Color.Blue));

        }

        public Intersection? Intersect(Ray ray)
        {
            Intersection? closest = null;
            foreach (Primitive prim in objects) {
                Intersection? newClosest = prim.Intersect(ray);
                if (!newClosest.HasValue) continue;
                Intersection intersection = newClosest.Value;
                closest = intersection;
            }
            return closest;
        }
    }
}
