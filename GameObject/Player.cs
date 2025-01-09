using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProject.Interface;
using SplashKitSDK;

namespace CustomProject.GameObject
{
    public class Player : GameObject, IGameObject
    {
        private double _scale = 0.3;
        private string _path = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\jet.png";
        private Bitmap _defaultImage;
        private Bitmap _leftImage;
        private Bitmap _rightImage;

        public Player(float x, float y, float width, float height) : base(x, y, width, height)
        {
            _defaultImage = new Bitmap("PlayerDefault", _path);
            _leftImage = new Bitmap("PlayerLeft", "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\jet-left.png");
            _rightImage = new Bitmap("PlayerRight", "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\jet-right.png");
            _image = _defaultImage;
        }

        public override void Draw(Window window)//Draw the image of Player Jet and scale it 0.3
        {
            DrawingOptions options = SplashKit.OptionDefaults();
            options.ScaleX = (float)_scale;
            options.ScaleY = (float)_scale;
            window.DrawBitmap(_image, _x, _y, options);
        }

        public void Move(float dx, float dy)//For player movement ingame
        {
            _x += dx;
            _y += dy;
        }

        public void TurnLeft()//This will change the image whenver player press "<" button
        {
            _image = _leftImage;
        }

        public void TurnRight()//This will change the image whenever player press ">" butotn
        {
            _image = _rightImage;
        }

        public void ResetImage()//Change the image back to default if none of the buttons mentioned are pressed
        {
            _image = _defaultImage;
        }
    }
}

