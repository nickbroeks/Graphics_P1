using System;
using OpenTK;

namespace Template {
    class Sphere : Primitive {
        
        public Vector3 position;
        public float radius;
        public float r2;

        public Sphere(Vector3 position, float radius, Material mat) : base(mat)
        {
            this.position = position;
            this.radius = radius;
            this.r2 = radius * radius;
        }

        public override bool Intersects(Ray ray)
        {
            //if (!AABBIntersects(ray)) return false; //Not really worth it yet
            Vector3 c = position - ray.origin;
            float t = Vector3.Dot(c, ray.direction);
            Vector3 q = c + -t * ray.direction;
            float p2 = q.LengthSquared;
            if (p2 > r2) return false;
            t -= (float)Math.Sqrt(r2 - p2);
            if (t > ray.T || t <= EPSILON) return false;
            ray.T = t;
            return true;
        }

        public override Vector3 Normal(Vector3 at, Vector3 from)
        {
            Vector3 normal = at - position;
            return normal; //TODO: Maybe add inside normals?
        }

        public override void Debug(RayTracer rayTracer)
        {
            int angles = 360;
            for (int i = 0; i < angles; i++) {
                float degree = (float)Math.PI*2 / angles * i;
                float degree2 = (float)Math.PI*2 / angles * (i + 1);
                int x1 = rayTracer.SceneToScreenX(
                    (float)Math.Cos(degree) * radius + position.X);
                int y1 = rayTracer.SceneToScreenY(
                    (float)Math.Sin(degree) * radius + position.Z);
                int x2 = rayTracer.SceneToScreenX(
                    (float)Math.Cos(degree2) * radius + position.X);
                int y2 = rayTracer.SceneToScreenY(
                    (float)Math.Sin(degree2) * radius + position.Z);
                rayTracer.screen.Line(x1, y1, x2, y2, Kd(Vector2.Zero).value);
            }
        }

        public override bool ShadowIntersects(Ray ray)
        {
            Vector3 c = position - ray.origin;
            float t = Vector3.Dot(c, ray.direction);
            Vector3 q = c - t * ray.direction;
            float p2 = q.LengthSquared;
            if (p2 > r2) return false;
            if (EPSILON < t && t < ray.T - EPSILON)
                return true;
            return false;
        }

        public override Vector2 Map(Vector3 point)
        {
            double y = Math.Acos(Math.Min(1, Math.Max(-1,(point.Y - position.Y) / radius))) / (float)(Math.PI);
            return new Vector2(
                (float)(Math.Atan2(point.Z - position.Z, point.X - position.X)+Math.PI) / (float)(2*Math.PI),
                (float)y
                
            );
        }

        internal override void PreProcess()
        {
            box = new AABB(position - new Vector3(radius), position + new Vector3(radius));
        }

        public override bool AABBIntersects(Ray ray)
        {
            return box.Intersects(ray);
        }
    }
}
