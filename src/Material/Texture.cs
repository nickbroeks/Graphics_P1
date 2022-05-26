
using System;
using OpenTK;

namespace Template {
    class Texture : Material {
        Func<Vector2, Color> mapping;

        public Texture(Func<Vector2, Color> mapping, bool isMirror = false) : base(isMirror) 
        {
            this.mapping = mapping;
        }
        public override Color Kd(Vector2 map)
        {
            return mapping(map);
        }

        public override Color Ks(Vector2 map)
        {
            return mapping(map);
        }

        public override float N(Vector2 map)
        {
            return 1;
        }
        public override Color Ka(Vector2 map)
        {
            return mapping(map);
        }
    }


}
