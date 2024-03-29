﻿using System;
using OpenTK;

namespace Template {
    /// <summary>
    /// Class that represent a ray, shot from an origin into a direction. It also keeps track of the closest point
    /// </summary>
    class Ray {
        private static readonly float MAXT = float.MaxValue;
        private Vector3 origin;
        public Vector3 direction;

        private float t;
        private Vector3 point;
        private Primitive collider;

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
        public Vector3 Origin { get { return origin; } }
        public float T { 
            get { return t; } 
            set { t = value; Point = origin + value * direction; } }
        
        public Primitive Collider { get { return collider; } set { collider = value; } }
    }
}
