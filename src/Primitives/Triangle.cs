using System;
using OpenTK;

namespace Template
{
    class Triangle : Primitive
    {
        private Vector3 normal;
        private Vector3 A, B, C, AB, BC, AC, BA, P; // vertex positions
        float d;
        Vector2 uvA, uvB, uvC; // per-vertex texture coordinates
        float areaABC;

        public Triangle(Vector3 A, Vector3 B, Vector3 C, Material material) : base(material)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.uvA = Vector2.UnitX;
            this.uvB = Vector2.Zero;
            this.uvC = Vector2.UnitY;
            AB = B - A;
            BA = A - B;
            AC = C - A;
            BC = C - B;
            normal = Vector3.Cross(AB, AC).Normalized();
            d = Vector3.Dot(normal, A);
            areaABC = Vector3.Cross(BC, BA).Length / 2;
        }
        public override bool Intersects(Ray ray)
        {
            float t =  (d - Vector3.Dot(ray.origin, normal)) / Vector3.Dot(ray.direction, normal);

            if (t <= EPSILON || t > ray.T) return false;
            P= ray.origin + t * ray.direction;
            if (Vector3.Dot(Vector3.Cross(AB, (P - A)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(BC, (P - B)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(-AC, (P - C)), normal) < 0) return false;
            ray.T = t;
            return true;
        }

        public override bool AABBIntersects(Ray ray)
        {
            return true;
            //throw new NotImplementedException();
        }

        public override void Debug(RayTracer rayTracer)
        {
            
        }

        public override Vector2 Map(Vector3 point)
        {
            Vector3 BP = B - point;
            Vector3 CP = C - point;
            float areaPBC = Vector3.Cross(BC, BP ).Length / 2;
            float areaPCA = Vector3.Cross(AC, CP).Length / 2;
            float a = areaPBC / areaABC;
            float b = areaPCA / areaABC;
            float g = 1 - a - b;
            float uP = a * uvA.X + b * uvB.X + g * uvC.X;
            float vP = a * uvA.Y + b * uvB.Y + g * uvC.Y;
            return new Vector2 (uP, vP);
            //throw new NotImplementedException();
        }

        public override Vector3 Normal(Vector3 at, Vector3 from)
        {
            return normal;
            //throw new NotImplementedException();
        }

        public override bool ShadowIntersects(Ray ray)
        {
            return false;
           float t = (d - Vector3.Dot(ray.origin, normal)) / Vector3.Dot(ray.direction, normal);

            if (t <= EPSILON || t > ray.T) return false;
            Vector3 P = ray.origin + t * ray.direction;
            if (Vector3.Dot(Vector3.Cross(AB, (P - A)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(BC, (P - B)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(-AC, (P - C)), normal) < 0) return false;
            return true;
        }

        internal override void PreProcess()
        {
            /*Vector3 boxMin;
            Vector3 boxMax;
            boxMin = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); 
            boxMax = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            foreach (Vector3 of )
            foreach (axis)
            {
                boxMin[axis] = min(vertex[axis], box.min[axis]);
                boxMax[axis] = max(vertex[axis], box.max[axis]);
            }
            for (int axis = 0; axis < 3; i++)
            {
                float t1 = (min[i] - ray.origin[i]) / ray.direction[i];
                float t2 = (max[i] - ray.origin[i]) / ray.direction[i];
                if (t1 > t2) { float temp = t1; t1 = t2; t2 = temp; }
                tmin = Math.Max(tmin, t1);
                tmax = Math.Min(tmax, t2);
            }*/
        }
    }
}