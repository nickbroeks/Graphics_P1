using System;
using OpenTK;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template {
    class Plain : Material {
        private readonly Color kd;
        private readonly Color ks;
        private readonly float n;
        private readonly Color ka;

        public Plain(Color kd, Color ks, float n, Color ka, bool isMirror = false) : base(isMirror)
        {
            this.kd = kd;
            this.ks = ks;
            this.n = n;
            this.ka = ka;
        }
        public override Color Kd(Vector2 map) { return kd; }

        public override Color Ks(Vector2 map) { return ks; }

        public override float N(Vector2 map) { return n; }
        public override Color Ka(Vector2 map) { return ka; }
    }
}
