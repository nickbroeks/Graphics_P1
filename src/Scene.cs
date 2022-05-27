using System;
using System.Collections.Generic;
using OpenTK;

namespace Template {

    class Scene {

        public List<Primitive> objects;
        public List<Light> lights;
        public Color ambient;
        public Random random;

        public Scene ()
        {
            random = new Random();
            objects = new List<Primitive>();
            lights = new List<Light>();
            //objects.Add(new Sphere(new Vector3(5, 0, 5), 2f, Material.Marble));
            //objects.Add(new Plane(Vector3.UnitX, 0f, Material.Mirror));
            //objects.Add(new Plane(Vector3.UnitX, 10f, Material.Mirror));
            //objects.Add(new Plane(Vector3.UnitZ, 0f, Material.Mirror));
            //objects.Add(new Plane(Vector3.UnitZ, 10f, Material.Mirror));
           // objects.Add(new Plane(Vector3.UnitY, -5f, Material.Liquid));
            //objects.Add(new Plane(Vector3.UnitY, 5f, Material.WhitePlastic));


            //for (int x = 1; x <= 9; x += 2) {
            //    for (int z = 1; z <= 9; z += 2) {
            //        objects.Add(new Sphere(new Vector3(x, -2, z), 0.4f, Material.SmallChecker));
            //    }
            //}
            //objects.Add(new Sphere(new Vector3(2, 0, 4), 1, Color.Purple, new Color(255,0, 155), 250f, Color.Black));
            //objects.Add(new Sphere(new Vector3(-3, 0, 3), 1.5f, Color.Green, Color.White * 0.2f, 4f, Color.Black));
            //objects.Add(new Sphere(new Vector3(0, 0, 1), 0.5f, Color.Blue, Color.White, 100f, Color.Black));
            //objects.Add(new Sphere(new Vector3(2, 0, 2), 0.5f, Material.PurplePlastic));
            //objects.Add(new Sphere(new Vector3(5, -2f, 5), 1f, Material.Marble));
            //lights.Add(new Light(new Vector3(9f, 4f, 9f), new Color(255, 0, 0)));
            //lights.Add(new Light(new Vector3(1f, 4f, 9f), new Color(0, 255, 0)));
            //lights.Add(new Light(new Vector3(5f, 4f, 1f), new Color(0, 0, 255)));
            objects.Add(new Triangle(new Vector3(4, -2, 4), new Vector3(6, 0, 4), new Vector3(5, 1, 5), Material.Metal));
            lights.Add(new Light(new Vector3(5f, 5f, 0f), new Color(255)));
            ambient = new Color(60, 60, 60);
        }

        public Intersection? Intersect(Ray ray)
        {
            Primitive closest = null;
            foreach (Primitive prim in objects) {
                if (!prim.Intersects(ray)) continue;
                closest = prim;
            }
            if (closest == null) return null;
            return new Intersection(ray.T, ray.Point, closest.Normal(ray.Point, ray.origin), closest);
        }
        public Color Illuminate(Intersection intSec, Ray primary)
        {
            Primitive collider = intSec.collider;
            Color color = Color.Black;
            foreach (Light light in lights) {
                float distance = Vector3.Distance(intSec.point, light.Location);
                Ray ray = new Ray(intSec.point, light.Location - intSec.point, distance);
                distance /= 4;
                if (distance > 15f) { continue; };
                bool earlyOut = false;
                foreach (Primitive prim in objects) {
                    if (prim != collider && prim.ShadowIntersects(ray)) {
                        earlyOut = true;
                        break;
                    }
                }
                if (earlyOut) continue;
                float angle = Vector3.Dot(intSec.normal, ray.direction);
                color += 1 / (float)Math.Pow(distance, 2) *
                light.Intensity * (
                    intSec.Kd * Math.Max(0, angle)
                     + intSec.Ks * (float)Math.Pow(Math.Max(0, Vector3.Dot(
                        primary.direction,
                        ray.direction - 2 * angle * intSec.normal
                    )), intSec.N)
                );
            }
            return color + intSec.Ka * ambient;
        }

        internal void PreProcess()
        {
            foreach (Primitive prim in objects) {
                prim.PreProcess();
            }
        }
    }
}