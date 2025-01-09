using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Scenes
{
    public class WinScene
    {
        private static WinScene? _instance;
        private static readonly object _lock = new object();
        private const string AfterBackgroundImagePath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\background2.jpg";
        private static string WinSoundPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\win.wav";
        private static Bitmap? _afterBackground;
        private static SoundEffect? _winSound;

        private static Font? _font;
        public WinScene()
        {
            _font = SplashKit.LoadFont("Verdana", "verdana.ttf");
            _afterBackground = new Bitmap("after_background", AfterBackgroundImagePath);
            _winSound = SplashKit.LoadSoundEffect("win sound", WinSoundPath);

        }

        public static WinScene Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new WinScene();
                    }
                }
            }
            return _instance;
        }
        public void Draw() //Draw texts and buttons
        {
            SplashKit.DrawBitmap(_afterBackground, 0, 0);
            SplashKit.DrawText("Victory", Color.Red, _font, 80, 150, 150);
            SplashKit.DrawText("You have passed the enemy strike", Color.Red, _font, 30, 150, 250);
            DrawingButton.DrawButton("Retry", 315, 417, _font);
        }
        public void PlaySound()
        {
            SplashKit.PlaySoundEffect(_winSound);
        }
    }
}

