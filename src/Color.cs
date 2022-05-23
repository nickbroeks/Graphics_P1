using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template {
    struct Color {
        public static readonly Color Red;
        public static readonly Color Green;
        public static readonly Color Blue;

        static Color()
        {
            Red = new Color(255, 0, 0);
            Green = new Color(0, 255, 0);
            Blue = new Color(0, 0, 255);
        }
        private float r, g, b;
        public int R { get { return (int)(r * 255); } }
        public int G { get { return (int)(g * 255); } }
        public int B { get { return (int)(b * 255); } }
        public int value { get { return (R << 16) + (G << 8) + B; } }
        public Color(int r, int g, int b)
        {
            this.r = Math.Max(0, Math.Min(1, r / 255f));
            this.g = Math.Max(0, Math.Min(1, g / 255f));
            this.b = Math.Max(0, Math.Min(1, b / 255f));
        }

        public static Color operator *(float mult, Color c)
        {
            int r = (int)(c.R * mult);
            int g = (int)(c.G * mult);
            int b = (int)(c.B * mult);
            return new Color(r, g, b);
        }
        public static Color operator *(Color c,float mult)
        {
            int r = (int)(c.R * mult);
            int g = (int)(c.G * mult);
            int b = (int)(c.B * mult);
            return new Color(r, g, b);
        }
    }
}
