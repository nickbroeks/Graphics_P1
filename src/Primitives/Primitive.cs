using OpenTK;
using System.Diagnostics;

namespace Template {
    abstract class Primitive {
        readonly Material material;
        public const float EPSILON = 0.0001f;
        protected AABB box;
        public AABB Box { get { return box; } }
        public Primitive(Material material)
        {
            this.material = material;
        }

        public abstract Vector3 Normal(Ray ray);
        public abstract bool Intersects(Ray ray);
        public abstract bool ShadowIntersects(Ray ray);
        public bool AABBIntersects(Ray ray)
        {
            bool result = box.Intersects(ray);
            return result;
            
        }
        public abstract void Debug(RayTracer rayTracer, int depth = 1);
        public abstract Vector2 Map(Ray ray);
        public Color Kd(Vector2 map) { return material.Kd(map); }
        public Color Ks(Vector2 map) { return material.Ks(map); }
        public float N(Vector2 map) { return material.N(map); }
        public Color Ka(Vector2 map) { return material.Ka(map); }
        public bool IsMirror { get { return material.isMirror; } }
        internal abstract void PreProcess();
    }
}
