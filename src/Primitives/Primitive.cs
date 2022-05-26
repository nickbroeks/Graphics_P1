using System;
using OpenTK;

namespace Template {
    abstract class Primitive {
        Material material;
        public const float EPSILON = 0.0001f;
        protected AABB box;

        public Primitive(Material material)
        {
            this.material = material;
        }

        public abstract Vector3 Normal(Vector3 at, Vector3 from);
        public abstract bool Intersects(Ray ray);
        public abstract bool ShadowIntersects(Ray ray);
        public abstract bool AABBIntersects(Ray ray);
        public abstract void Debug(RayTracer rayTracer);
        public abstract Vector2 Map(Vector3 point);
        public Color Kd(Vector2 map) { return material.Kd(map); }
        public Color Ks(Vector2 map) { return material.Ks(map); }
        public float N(Vector2 map) { return material.N(map); }
        public Color Ka(Vector2 map) { return material.Ka(map); }
        public bool IsMirror { get { return material.isMirror; } }
        internal abstract void PreProcess();
    }
}
