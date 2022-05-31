using System;
using OpenTK;

namespace Template
{
    class Triangle : Primitive
    {
        private Vector3 normal, nA, nB, nC;
        private Vector3 A, B, C, AB, BC, AC, BA; // vertex positions
        readonly float d;
        public Vector2 uvA, uvB, uvC; // per-vertex texture coordinates
        readonly float areaABC;

        public Triangle(Vector3 A, Vector3 B, Vector3 C, Material material) : base(material)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            //Default values for texturing triangles
            this.uvA = Vector2.UnitX; 
            this.uvB = Vector2.Zero;
            this.uvC = Vector2.UnitY;

            AB = B - A;
            BA = A - B;
            AC = C - A;
            BC = C - B;
            normal = Vector3.Cross(AB, AC).Normalized();
            //Default values for normal vectors is the normal of the vector
            nA = normal;
            nB = normal;
            nC = normal;
            d = Vector3.Dot(normal, A);
            areaABC = Vector3.Cross(BC, BA).Length / 2;
        }
        public Triangle(Vector3 A, Vector3 B, Vector3 C, Vector3 nA, Vector3 nB, Vector3 nC, Material material) : base(material)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.uvA = Vector2.UnitX;
            this.uvB = Vector2.Zero;
            this.uvC = Vector2.UnitY;
            this.nA = nA;
            this.nB = nB;
            this.nC = nC;
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
            if (!AABBIntersects(ray)) return false;
            float t =  (d - Vector3.Dot(ray.Origin, normal)) / Vector3.Dot(ray.direction, normal);

            if (t <= EPSILON || t >= ray.T) return false;
            Vector3 P = ray.Origin + t * ray.direction;
            if (Vector3.Dot(Vector3.Cross(AB, (P - A)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(BC, (P - B)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(-AC, (P - C)), normal) < 0) return false;
            ray.T = t;
            ray.Collider = this;
            return true;
        }

        public override void Debug(RayTracer rayTracer, int depth)
        {
            int x1 = rayTracer.SceneToScreenX(A.X);
            int y1 = rayTracer.SceneToScreenY(A.Z);
            int x2 = rayTracer.SceneToScreenX(B.X);
            int y2 = rayTracer.SceneToScreenY(B.Z);
            int x3 = rayTracer.SceneToScreenX(C.X);
            int y3 = rayTracer.SceneToScreenY(C.Z);
            rayTracer.screen.Line(x1, y1, x2, y2, Kd(Vector2.Zero).value);
            rayTracer.screen.Line(x1, y1, x3, y3, Kd(Vector2.Zero).value);
            rayTracer.screen.Line(x3, y3, x2, y2, Kd(Vector2.Zero).value);
            box.Debug(rayTracer, depth);
        }

        public override Vector2 Map(Ray ray)
        {
            Vector3 BP = B - ray.Point;
            Vector3 CP = C - ray.Point;
            float areaPBC = Vector3.Cross(BC, BP ).Length / 2;
            float areaPCA = Vector3.Cross(AC, CP).Length / 2;
            float a = areaPBC / areaABC;
            float b = areaPCA / areaABC;
            float g = 1 - a - b;
            float uP = a * uvA.X + b * uvB.X + g * uvC.X;
            float vP = a * uvA.Y + b * uvB.Y + g * uvC.Y;
            return new Vector2 (uP, vP);
        }

        public override Vector3 Normal(Ray ray)
        {
            Vector3 BP = B - ray.Point;
            Vector3 CP = C - ray.Point;
            float areaPBC = Vector3.Cross(BC, BP).Length / 2;
            float areaPCA = Vector3.Cross(AC, CP).Length / 2;
            float a = areaPBC / areaABC;
            float b = areaPCA / areaABC;
            float g = 1 - a - b;
            Vector3 result = a * nA + b * nB + g * nC;
            if (ray.Origin.X * normal.X + ray.Origin.Y * normal.Y + ray.Origin.Z * normal.Z >= d) {
                return result;
            } else { return -result; }
        }

        public override bool ShadowIntersects(Ray ray)
        {
           float t = (d - Vector3.Dot(ray.Origin, normal)) / Vector3.Dot(ray.direction, normal);

            if (t <= EPSILON || t > ray.T) return false;
            Vector3 P = ray.Origin + t * ray.direction;
            if (Vector3.Dot(Vector3.Cross(AB, (P - A)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(BC, (P - B)), normal) < 0) return false;
            if (Vector3.Dot(Vector3.Cross(-AC, (P - C)), normal) < 0) return false;
            return true;
        }

        internal override void PreProcess()
        {
            Vector3 boxMin;
            Vector3 boxMax;
            boxMin = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); 
            boxMax = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            foreach (Vector3 vertex in new Vector3[] { A, B, C })
            {
                for (int axis = 0; axis < 3; axis++)
                {
                    boxMin[axis] = Math.Min(vertex[axis], boxMin[axis]);
                    boxMax[axis] = Math.Max(vertex[axis], boxMax[axis]);
                }
            }
            this.box = new AABB(boxMin, boxMax);
        }
    }
}