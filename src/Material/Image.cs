using System;
using System.Drawing;
using OpenTK;

namespace Template {
    class Image : Material {
        Color[,] image;
        private int width, height;

        public Image(string assetName, bool isMirror = false) : base(isMirror)
        {

            Bitmap bitmap = new Bitmap(assetName);
            width = bitmap.Width;
            height = bitmap.Height;
            image = new Color[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    image[x, y] = new Color(bitmap.GetPixel(x, y));
                }
            }
        }
        public override Color Kd(Vector2 map)
        {
            int x = Math.Max(0, Math.Min(width - 1, (int)(map.X * width)));
            int y = Math.Max(0, Math.Min(height - 1, (int)(map.Y * height)));
            try {
                return image[x, y];
            } catch (Exception e) {
                Console.WriteLine(e);
            }
            return Color.White;
        }

        public override Color Ks(Vector2 map)
        {
            int x = Math.Max(0, Math.Min(width - 1, (int)(map.X * width)));
            int y = Math.Max(0, Math.Min(height - 1, (int)(map.Y * height)));
            try {
                return image[x, y];
            } catch (Exception e) {
                Console.WriteLine(e);
            }
            return Color.White;
        }

        public override float N(Vector2 map)
        {
            return 1000;
        }
        public override Color Ka(Vector2 map)
        {
            int x = Math.Max(0, Math.Min(width - 1, (int)(map.X * width)));
            int y = Math.Max(0, Math.Min(height - 1, (int)(map.Y * height)));
            try {
                return image[x, y];
            } catch (Exception e) {
                Console.WriteLine(e);
            }
            return Color.White;
        }
    }


}