using System;
using OpenTK;

namespace Template {
    class Ray {
        private static readonly float MAXT = float.MaxValue;
        public Vector3 origin;
        public Vector3 direction;
        private float t;
        private Vector3 point;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction.Normalized();
            this.T = MAXT;
        }
        public Ray(Vector3 origin, Vector3 direction, float t)
        {
            this.origin = origin;
            this.direction = direction.Normalized();
            this.T = t;
        }
        public Vector3 Point
        {
            get { return point; }
            private set { point = value;}
        }
        public float T { 
            get { return t; } 
            set { t = value; Point = origin + value * direction; } }
    }
}
