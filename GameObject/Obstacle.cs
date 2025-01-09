using CustomProject.Interface;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.GameObject
{
    public class Obstacle : GameObject, IGameObject
    {
        private double _speed;
        private double _scale = 0.3;

        public Obstacle(float x, float y, double speed, float width, float height) : base(x, y, width, height)
        {
            _image = new Bitmap("Obstacle", "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\obstacle.png");
            _speed = speed;
        }

        public override void Draw(Window window)//Draw the image of Enemy Jet and scale it 0.3
        {
            DrawingOptions options = SplashKit.OptionDefaults();
            options.ScaleX = (float)_scale;
            options.ScaleY = (float)_scale;
            window.DrawBitmap(_image, _x, _y, options);
        }

        public void Update(Window window)// Update position og obstacle and check if it's off-screen. If so, move it back to the top of the screen.
        {
            _y += (float)_speed;

            if (_y > window.Height)
            {
                _y = -_image.Height;
                _x = SplashKit.Rnd(window.Width) - (int)(559 * 0.35);
            }
        }
        public void IncreaseSpeed(double increment)//For speed increasing feature ingame
        {
            _speed += increment;
        }


    }
}
