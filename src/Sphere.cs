using System;
using OpenTK;

namespace Template {
    class Sphere : Primitive {
        
        private Vector3 position;
        private float radius;
        private float r2;

        public Sphere(Vector3 position, float radius, Color color) : base(color)
        {
            this.position = position;
            this.radius = radius;
            this.r2 = radius * radius;
        }
        
        public override Intersection? Intersect(Ray ray)
        {
            Vector3 c = position - ray.origin;
            float t = Vector3.Dot(c, ray.direction);
            Vector3 q = c + -t * ray.direction;
            float p2 = q.LengthSquared;
            if (p2 > r2) return null;
            t -= (float)Math.Sqrt(r2 - p2);
            if (t > ray.t || t <= 0) return null;
            ray.t = t;
            return new Intersection(t, position - ray.Point(), this);
        }
    }
}
