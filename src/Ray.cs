using System;
using OpenTK;

namespace Template {
    class Ray {
        private static readonly float MAXT = float.MaxValue;
        public Vector3 origin;
        public Vector3 direction;
        public float t;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction.Normalized();
            this.t = MAXT;
        }

        public Vector3 Point()
        {
            return origin + t * direction;
        }
    }
}
