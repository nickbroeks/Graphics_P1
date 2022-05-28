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
                float t1 = (min[i] - ray.Origin[i]) / ray.direction[i];
                float t2 = (max[i] - ray.Origin[i]) / ray.direction[i];
                tmin = Math.Max(tmin, Math.Min(t1, t2));
                tmax = Math.Min(tmax, Math.Max(t1, t2));
            }
            return tmin <= tmax && tmax > 0;
        }
        public void Debug(RayTracer rayTracer, int depth)
        {
            Color color = 1f / depth *Color.Cyan;
            int x1 = rayTracer.SceneToScreenX(min.X);
            int y1 = rayTracer.SceneToScreenY(min.Z);
            int x2 = rayTracer.SceneToScreenX(max.X);
            int y2 = rayTracer.SceneToScreenY(max.Z);
            rayTracer.screen.Line(x1, y1, x1, y2, color.value);
            rayTracer.screen.Line(x1, y1, x2, y1, color.value);
            rayTracer.screen.Line(x2, y2, x1, y2, color.value);
            rayTracer.screen.Line(x2, y2, x2, y1, color.value);
        }
    }
}
