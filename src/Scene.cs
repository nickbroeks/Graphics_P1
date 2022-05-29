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
            objects.Add(new Plane(Vector3.UnitY, -1f, Material.MirrorSmallChecker));
            //objects.Add(new Plane(Vector3.UnitY, 5f, Material.WhitePlastic));

            objects.Add(new Sphere(new Vector3(2, 0, 5), 1f, Material.MirrorRed));
            objects.Add(new Sphere(new Vector3(5, 0f, 5), 1f, Material.MirrorGreen));
            objects.Add(new Sphere(new Vector3(8, 0f, 5), 1f, Material.MirrorBlue));
            //objects.Add(new Sphere(new Vector3(9, 0f, 8), 1f, Material.PurplePlastic));
            //objects.Add(new Sphere(new Vector3(8, -3f, 5), 1.5f, Material.GreenPlastic));
            //lights.Add(new Light(new Vector3(9f, 4f, 9f), new Color(255, 0, 0)));
            //lights.Add(new Light(new Vector3(1f, 4f, 9f), new Color(0, 255, 0)));
            //lights.Add(new Light(new Vector3(5f, 4f, 1f), new Color(0, 0, 255)));
            //objects.Add(new Triangle(new Vector3(4, -1, 4), new Vector3(6, -1, 4), new Vector3(4, -1, 6), Material.Metal));
            //objects.Add(new Triangle(new Vector3(6, -1, 6), new Vector3(6, -1, 4), new Vector3(4, -1, 6), Material.Metal));
            //objects.Add(new Triangle(new Vector3(4, -1, 4), new Vector3(6, -1, 4), new Vector3(5, .5f, 5), Material.Metal));
            //objects.Add(new Triangle(new Vector3(6, -1, 6), new Vector3(6, -1, 4), new Vector3(5, .5f, 5), Material.Metal));
            //objects.Add(new Triangle(new Vector3(4, -1, 4), new Vector3(4, -1, 6), new Vector3(5, .5f, 5), Material.Metal));
            //objects.Add(new Triangle(new Vector3(6, -1, 6), new Vector3(4, -1, 6), new Vector3(5, .5f, 5), Material.Metal));
            //objects.Add(new Pyramid(Material.Metal));
            lights.Add(new Light(new Vector3(5f, 5f, 8f), new Color(255)));
            ambient = new Color(30, 30, 30);
        }

        public Intersection? Intersect(Ray ray)
        {
            Primitive closest = null;
            foreach (Primitive prim in objects) {
                if (!prim.Intersects(ray)) continue;
                closest = prim;
            }
            if (closest == null) return null;
            return new Intersection(ray, closest.Normal(ray), closest);
        }
        public Color Illuminate(Intersection intSec, Ray primary)
        {
            Primitive collider = intSec.collider;
            Color color = Color.Black;
            foreach (Light light in lights) {
                float distance = Vector3.Distance(intSec.Point, light.Location);
                Ray ray = new Ray(intSec.Point, light.Location - intSec.Point, distance);
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
                if (collider.IsMirror) {
                    color += 1 / (float)Math.Pow(distance, 2) *
                    light.Intensity * (
                        intSec.Kd * Math.Max(0, angle)
                    );
                } else {
                    color += 1 / (float)Math.Pow(distance, 2) *
                    light.Intensity * (
                        intSec.Kd * Math.Max(0, angle)
                         + intSec.Ks * (float)Math.Pow(Math.Max(0, Vector3.Dot(
                            primary.direction,
                            ray.direction - 2 * angle * intSec.normal
                        )), intSec.N)
                    );
                }
                
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