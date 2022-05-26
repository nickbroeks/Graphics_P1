using System;
using OpenTK;

namespace Template
{
    class Triangle : Primitive
    {

        private Vector3 position;
        private Vector3 normal;
        private Vector3 A, B, C, AB, AC; // vertex positions
       // private Vector3 NA, NB, NC; // per-vertex shading normals
        //private Vector2 uvA, uvB, uvC; // per-vertex texture coordinates
        public Triangle(Vector3 position, Color color) : base(color)
        {
            this.position = position;            
        }

        public override Intersection? Intersect(Ray ray)
        {
            AB = B - A;
            AC = C - A;
            normal = Vector3.Cross(AB, AC).Normalized();
            float d = Vector3.Dot(normal, A);
            Vector3 D = position - ray.direction;
            Vector3 E = ray.origin + ray.direction;
            float t =  (d - Vector3.Dot(E, normal)) / Vector3.Dot(D, normal);

            if (t <= EPSILON || t > ray.T) return false;




        } 
    }
}