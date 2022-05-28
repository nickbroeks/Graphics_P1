using System;
using OpenTK;

namespace Template {
    class Plane : Primitive {

        private Vector3 normal;
        private readonly float distance;
        private Vector3 origin;
        private Vector3 u, v;

        public Plane(Vector3 normal, float distance, Material mat) : base(mat)
        {
            this.normal = normal.Normalized();
            this.distance = distance;
            this.origin = normal * distance;
            this.u = Vector3.Cross(normal, new Vector3(1, 0, 0));
            //If normal and (1,0,0) are parallel, change e1
            if (u == new Vector3(0, 0, 0)) {
                u = Vector3.Cross(normal, new Vector3(0, 0, 1));
            }
            this.v = Vector3.Cross(normal, u);
        }

        public override bool ShadowIntersects(Ray ray)
        {
            float dot = Vector3.Dot(ray.direction, normal);
            if (dot == 0) return false;
            float t = -Vector3.Dot(ray.Origin - origin, normal) / dot;
            if (EPSILON < t && t < ray.T - EPSILON) {
                return true;
            }
            return false;
        }
        public override bool Intersects(Ray ray)
        {
            float dot = Vector3.Dot(ray.direction, normal);
            if (Math.Abs(dot) < EPSILON) return false;
            float t = -Vector3.Dot(ray.Origin - origin, normal) / dot;
            if (t <= EPSILON || t > ray.T) return false;
            ray.T = t;
            ray.Collider = this;
            return true;
        }
        public override Vector3 Normal(Ray ray)
        {
            if (ray.Origin.X * normal.X + ray.Origin.Y * normal.Y + ray.Origin.Z * normal.Z >= distance) {
                return normal;
            } else { return -normal; }
        }

        public override void Debug(RayTracer rayTracer, int depth)
        {
        }

        public override Vector2 Map(Ray ray)
        {
            float x = Vector3.Dot(ray.Point, u) / 20;
            float y = Vector3.Dot(ray.Point, v) / 20;
            if ((x - (int)x + 1) % 1 < 0) {
                Console.WriteLine("test");
            }
            return new Vector2((x - (int)x + 1) % 1, (y - (int)y + 1) % 1);
        }

        internal override void PreProcess()//Not possible for planes
        {
            
        }
    }
}
