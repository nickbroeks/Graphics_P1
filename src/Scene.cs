using System;
using System.Collections.Generic;
using OpenTK;

namespace Template {

    class Scene {

        public List<Primitive> objects;
        public List<Light> lights;
        public Color ambient; //Light value used used for the shadows
        public Random random;

        public Scene ()
        {
            random = new Random();
            objects = new List<Primitive>();
            lights = new List<Light>();

            //Uncomment one of these to see different scenes.
            //Scene1();
            Scene2();
            //Scene3();
        }

        //Function that will return an intersection of the ray intersects with any object
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
        //Function that returns which color the intersections should get
        public Color Illuminate(Intersection intSec)
        {
            Primitive collider = intSec.collider;
            Color color = Color.Black;
            foreach (Light light in lights) {
                float distance = Vector3.Distance(intSec.Point, light.Location);
                if (distance > 20f) { continue; }; //If the light is far away, consider it to do nothing
                Ray ray = new Ray(intSec.Point, light.Location - intSec.Point, distance);
                distance /= 4; //Add a distance modifier to increase the intensity of lights
                
                //If the light ray hits any object, don't consider this light
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
                            intSec.ray.direction,
                            ray.direction - 2 * angle * intSec.normal
                        )), intSec.N)
                    );
                }
                
            }
            return color + intSec.Ka * ambient;
        }

        internal void PreProcess()//Function to make AABB's for each primitive
        {
            foreach (Primitive prim in objects) {
                prim.PreProcess();
            }
        }
        //Functions to setup a Scene
        private void Scene1()
        {
            objects.Add(new Sphere(new Vector3(2, 0, 5), 1f, Material.Marble));
            objects.Add(new Sphere(new Vector3(5, 0f, 5), 1f, Material.GreenGlossy));
            objects.Add(new Sphere(new Vector3(8, 0f, 5), 1f, Material.MirrorBlue));
            objects.Add(new Plane(Vector3.UnitY, -1f, Material.MirrorSmallChecker));
            lights.Add(new Light(new Vector3(5f, 5f, 2f), new Color(255)));
            ambient = new Color(30);
        }

        private void Scene2()
        {
            objects.Add(new Sphere(new Vector3(5, 0, 5), 2f, Material.Marble));
            objects.Add(new Plane(Vector3.UnitX, 0f, Material.Mirror));
            objects.Add(new Plane(Vector3.UnitX, 10f, Material.Mirror));
            objects.Add(new Plane(Vector3.UnitZ, 0f, Material.Mirror));
            objects.Add(new Plane(Vector3.UnitZ, 10f, Material.Mirror));
            objects.Add(new Plane(Vector3.UnitY, -5f, Material.SmallChecker));
            ambient = new Color(90);
        }
        private void Scene3()
        {
            objects.Add(new Plane(Vector3.UnitY, -3f, Material.SmallChecker));
            objects.Add(new Pyramid(Material.Metal));
            lights.Add(new Light(new Vector3(9f, 4f, 9f), new Color(255, 0, 0)));
            lights.Add(new Light(new Vector3(1f, 4f, 9f), new Color(0, 255, 0)));
            lights.Add(new Light(new Vector3(5f, 4f, 1f), new Color(0, 0, 255)));
        }
    }
}