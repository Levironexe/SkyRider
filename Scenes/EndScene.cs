using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Scenes
{
    public class EndScene
    {
        private static EndScene? _instance;
        private static readonly object _lock = new object();

        private static Font? _font;

        public EndScene()
        {
            _font = SplashKit.LoadFont("Verdana", "verdana.ttf");
        }
        public static EndScene Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new EndScene();
                    }
                }
            }
            return _instance;
        }
        public void Draw(int suvivedTime, int totalDeaths, int highestSurvivedTime, int highestDesNum, int desNum) //Draw texts and buttons
        {
            SplashKit.DrawText("Game over!", Color.Red, _font, 60, 240, 50);
            SplashKit.DrawText($"Total crash: {totalDeaths}", Color.DarkOrange, _font, 28, 250, 150);
            SplashKit.DrawText($"Enemy destroyed: {desNum}", Color.DarkOrange, _font, 28, 250, 200);
            SplashKit.DrawText($"Highest: {highestDesNum}", Color.DarkOrange, _font, 28, 250, 250);
            SplashKit.DrawText($"Survived time: {suvivedTime}s", Color.DarkOrange, _font, 28, 250, 300);
            SplashKit.DrawText($"Highest: {highestSurvivedTime}s", Color.DarkOrange, _font, 28, 250, 350);
            DrawingButton.DrawButton("Retry", 315, 417, _font);
            DrawingButton.DrawButton("Exit", 315, 495, _font);
        }
    }
}
