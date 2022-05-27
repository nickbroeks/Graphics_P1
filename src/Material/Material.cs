using OpenTK;
using System;

namespace Template {
    abstract class Material {
        public static Material GreenPlastic;
        public static Material PurplePlastic;
        public static Material WhitePlastic;
        public static Material WhiteGlossy;
        public static Material Checker;
        public static Material SmallChecker;
        public static Material Marble;
        public static Material Metal;
        public static Material Liquid;
        public static Material Mirror;
        public static Material MirrorRed;
        public static Material MirrorGreen;
        public static Material MirrorBlue;
        static Material()
        {
            GreenPlastic = new Plain(Color.Green, Color.Black, 1, Color.Green);
            PurplePlastic = new Plain(Color.Purple, Color.Black, 1, Color.Purple);
            
            WhitePlastic = new Plain(new Color(200), new Color(0,0,0), 1, new Color(200));
            WhiteGlossy = new Plain(new Color(200), new Color(0.2f), 1250, new Color(200));
            Checker = new Texture((map) => {
                if ((((int)(map.X * 2) + (int)(map.Y * 2)) & 1) == 0) {
                     return Color.Black;
                }
                return Color.White;
            });
            SmallChecker = new Texture((map) => {
                if ((((int)(map.X * 10 + 1) + (int)(map.Y * 10)) & 1) == 0) {
                    return Color.Black;
                }
                return Color.White;
            });
            Marble = new Image(@"..\..\assets\Marble.jpg");
            Metal = new Image(@"..\..\assets\Metal.jpg");
            Liquid = new Image(@"..\..\assets\Liquid.jpg");

            Mirror = new Plain(Color.White, new Color(200, 200, 200), 0, Color.White, true);
            MirrorRed = new Plain(Color.Red, new Color(200), 0, Color.Red, true);
            MirrorGreen = new Plain(Color.Green, new Color(200), 0, Color.Green, true);
            MirrorBlue = new Plain(Color.Blue, new Color(200), 0, Color.Blue, true);
        }
        public Material(bool isMirror = false)
        {
            this.isMirror = isMirror;
        }
        public abstract Color Kd(Vector2 map);
        public abstract Color Ks(Vector2 map);
        public abstract float N(Vector2 map);
        public abstract Color Ka(Vector2 map);

        public bool isMirror;


    }
}
