using System;
using OpenTK;

namespace Template {
    class AABB {
        public readonly Vector3 min, max;

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public bool Intersects(Ray ray)
        {
            float tmin = float.NegativeInfinity; float tmax = float.PositiveInfinity;
            for (int i = 0; i < 3; i++) {
                float t1 = (min[i] - ray.origin[i]) / ray.direction[i];
                float t2 = (max[i] - ray.origin[i]) / ray.direction[i];
                if (t1 > t2) { float temp = t1; t1 = t2; t2 = temp; }
                tmin = Math.Max(tmin, t1);
                tmax = Math.Min(tmax, t2);
            }
            return tmin <= tmax && tmax > 0;
        }
    }
}
