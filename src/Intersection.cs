using System;
using OpenTK;

namespace Template {
    struct Intersection {

        public float distance;
        public Vector3 normal, point;
        public Primitive collider;
        public Vector2 map;

        public Intersection(float distance, Vector3 point, Vector3 normal, Primitive collider = null)
        {
            this.distance = distance;
            this.point = point;
            this.normal = normal.Normalized();
            this.collider = collider;
            map = collider.Map(point);
        }

        public Color Kd { get { return collider.Kd(map); } }
        public Color Ks { get { return collider.Ks(map); } }
        public float N { get { return collider.N(map); } }
        public Color Ka { get { return collider.Ka(map); } }
    }
}
