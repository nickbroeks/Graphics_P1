using System;
using OpenTK;

namespace Template {
    struct Intersection {

        public Ray ray;
        public Vector3 normal;
        public Primitive collider;
        public Vector2 map;

        public Intersection(Ray ray, Vector3 normal, Primitive collider = null)
        {
            this.ray = ray;
            this.normal = normal.Normalized();
            this.collider = collider;
            map = collider.Map(ray);
        }

        public Color Kd { get { return collider.Kd(map); } }
        public Color Ks { get { return collider.Ks(map); } }
        public float N { get { return collider.N(map); } }
        public Color Ka { get { return collider.Ka(map); } }
        public Vector3 Point { get { return ray.Point; } }
    }
}
