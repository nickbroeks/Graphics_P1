using OpenTK;
using System;

namespace Template {
    //Abstract class to hold a materials like Images, PLain colors or generated Textures
    abstract class Material {
        public static Material GreenPlastic;
        public static Material PurplePlastic;
        public static Material RedGlossy;
        public static Material GreenGlossy;
        public static Material BlueGlossy;
        public static Material WhitePlastic;
        public static Material WhiteGlossy;
        public static Material Checker;
        public static Material SmallChecker;
        public static Material MirrorSmallChecker;
        public static Material Marble { get { if (marble == null) { marble = new Image(@"..\..\assets\Marble.jpg"); } return marble; } }
        public static Material Metal { get { if (metal == null) { metal = new Image(@"..\..\assets\Metal.jpg"); } return metal; } }
        public static Material Liquid { get { if (liquid == null) { liquid = new Image(@"..\..\assets\Liquid.jpg"); } return liquid; } }
        public static Material Mirror;
        public static Material MirrorRed;
        public static Material MirrorGreen;
        public static Material MirrorBlue;

        public static Material marble;
        public static Material metal;
        public static Material liquid;
        static Material()
        {
            GreenPlastic = new Plain(Color.Green, Color.Black, 1, Color.Green);
            PurplePlastic = new Plain(Color.Purple, Color.Black, 1, Color.Purple);
            
            RedGlossy = new Plain(Color.Red, new Color(150), 2, Color.Red);
            GreenGlossy = new Plain(Color.Green, new Color(150), 2, Color.Green);
            BlueGlossy = new Plain(Color.Blue, new Color(150), 2, Color.Blue);

            WhitePlastic = new Plain(new Color(200), new Color(0,0,0), 1, new Color(200));
            WhiteGlossy = new Plain(new Color(200), new Color(0.2f), 1250, new Color(200));
            Checker = new Texture((map) => {
                if ((((int)(map.X * 2) + (int)(map.Y * 2)) & 1) == 0) {
                     return Color.Black;
                }
                return Color.White;
            });
            SmallChecker = new Texture((map) => {
                if ((((int)(map.X * 40) + (int)(map.Y * 40)) & 1) == 0) {
                    return Color.Black;
                }
                return Color.White;
            });
            MirrorSmallChecker = new Texture((map) => {
                if ((((int)(map.X * 40) + (int)(map.Y * 40)) & 1) == 0) {
                    return Color.Black;
                }
                return Color.White;
            }, true);
            Mirror = new Plain(new Color(0), new Color(255), 0, new Color(10), true) ;
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
