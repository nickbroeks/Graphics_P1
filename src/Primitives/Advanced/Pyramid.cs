using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template {
    class Pyramid : Primitive {
        public List<Primitive> objects;

        public Pyramid(Material material) : base(material)
        {
            objects = new List<Primitive>
            {
                new Triangle(new Vector3(4, -1, 4), new Vector3(6, -1, 4), new Vector3(4, -1, 6), material),
                new Triangle(new Vector3(6, -1, 6), new Vector3(6, -1, 4), new Vector3(4, -1, 6), material),
                new Triangle(new Vector3(4, -1, 4), new Vector3(6, -1, 4), new Vector3(5, .5f, 5), material),
                new Triangle(new Vector3(6, -1, 6), new Vector3(6, -1, 4), new Vector3(5, .5f, 5), material),
                new Triangle(new Vector3(4, -1, 4), new Vector3(4, -1, 6), new Vector3(5, .5f, 5), material),
                new Triangle(new Vector3(6, -1, 6), new Vector3(4, -1, 6), new Vector3(5, .5f, 5), material)
            };
        }

        public override void Debug(RayTracer rayTracer, int depth)
        {
            foreach (Primitive prim in objects) {
                prim.Debug(rayTracer, depth+1);
            }
            box.Debug(rayTracer, depth);
        }

        public override bool Intersects(Ray ray)
        {
            if (!AABBIntersects(ray)) return false;
            bool result = false;
            foreach (Primitive prim in objects) {
                if (prim.Intersects(ray)) result = true;
            }
            return result;
        }

        public override Vector2 Map(Ray ray)
        {
            return ray.Collider.Map(ray);
        }

        public override Vector3 Normal(Ray ray)
        {
            return ray.Collider.Normal(ray);
        }

        public override bool ShadowIntersects(Ray ray)
        {
            if (!AABBIntersects(ray)) return false;
            foreach (Primitive prim in objects) {
                if (prim.ShadowIntersects(ray)) return true;
            }
            return false;
        }

        internal override void PreProcess()
        {
            Vector3 boxMin;
            Vector3 boxMax;
            boxMin = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            boxMax = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            foreach (Primitive prim in objects) {
                prim.PreProcess();
                AABB smallBox = prim.Box;
                for (int axis = 0; axis < 3; axis++) {
                    boxMin[axis] = Math.Min(smallBox.min[axis], boxMin[axis]);
                    boxMax[axis] = Math.Max(smallBox.max[axis], boxMax[axis]);
                }
            }
            this.box = new AABB(boxMin, boxMax);
        }
    }
}
