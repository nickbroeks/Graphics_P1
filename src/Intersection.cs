using System;
using OpenTK;

namespace Template {
    struct Intersection {

        public float distance;
        public Vector3 normal;
        public Primitive collider;

        public Intersection(float distance, Vector3 normal, Primitive collider = null)
        {
            this.distance = distance;
            this.normal = normal.Normalized();
            this.collider = collider;
        }
    }
}
