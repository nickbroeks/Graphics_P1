using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template {
    abstract class Primitive {
        public Color color;

        public Primitive(Color color)
        {
            this.color = color;
        }

        public abstract Intersection? Intersect(Ray ray);
    }
}
