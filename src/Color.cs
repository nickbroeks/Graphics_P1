using System;

namespace Template {
    struct Color {
        public static readonly Color Red;
        public static readonly Color Green;
        public static readonly Color Blue;
        public static readonly Color Yellow;
        public static readonly Color Cyan;
        public static readonly Color Purple;

        public static readonly Color White;
        public static readonly Color Black;

        static Color()
        {
            Red = new Color(255, 0, 0);
            Green = new Color(0, 255, 0);
            Blue = new Color(0, 0, 255);
            Yellow = new Color(255, 255, 0);
            Cyan = new Color(0, 255, 255);
            Purple = new Color(255, 0, 255);
            White = new Color(255, 255, 255);
            Black = new Color(0, 0, 0);
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
        public Color(int v)
        {
            float val = Math.Max(0, Math.Min(1, v / 255f));
            this.r = val;
            this.g = val;
            this.b = val;
        }
        public Color(System.Drawing.Color color)
        {
            this.r = color.R / 255f;
            this.g = color.G / 255f;
            this.b = color.B / 255f;

        }
        public Color(float v)
        {
            float val = Math.Max(0, Math.Min(1, v));
            this.r = val;
            this.g = val;
            this.b = val;
        }
        public Color(float r, float g, float b)
        {
            this.r = Math.Max(0, Math.Min(1, r));
            this.g = Math.Max(0, Math.Min(1, g));
            this.b = Math.Max(0, Math.Min(1, b));
        }

        public static Color operator *(float mult, Color c)
        {
            float r = (c.r * mult);
            float g = (c.g * mult);
            float b = (c.b * mult);
            return new Color(r, g, b);
        }
        public static Color operator *(Color c, float mult)
        {
            return mult * c;
        }

        public static Color operator *(Color left, Color right)
        {
            return new Color(left.r * right.r, left.g * right.g, left.b * right.b);
        }

        public static Color operator +(Color left, Color right)
        {
            return new Color(left.r + right.r, left.g + right.g, left.b + right.b);
        }
    }
}
