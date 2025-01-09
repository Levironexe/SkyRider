using CustomProject.Interface;
using SplashKitSDK;
using System;

namespace CustomProject
{
    public class StartScene : IScene
    {
        private static StartScene? _instance;
        private static readonly object _lock = new object();
        private static Font? _font;
        private const string AfterBackgroundImagePath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\background2.jpg";
        private static Bitmap? _afterBackground;

        public StartScene()
        {
            _afterBackground = new Bitmap("after_background", AfterBackgroundImagePath);
            _font = SplashKit.LoadFont("Verdana", "verdana.ttf");
        }

        public static StartScene Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new StartScene();
                    }
                }
            }
            return _instance;
        }
        public void Draw() //Draw texts and buttons
        {
            SplashKit.DrawBitmap(_afterBackground,0,0);
            DrawingButton.DrawButton("Play", 315, 250, _font);
            DrawingButton.DrawButton("Reset game data", 315, 332, _font);
            DrawingButton.DrawButton("Exit", 315, 414, _font);
            SplashKit.DrawText("SKY RIDER", Color.OrangeRed, _font, 95, 145, 120);
        }
    }
}
