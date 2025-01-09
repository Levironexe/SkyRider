using CustomProject.Interface;
using SplashKitSDK;

namespace CustomProject.GameObject
{
    public class APBullet : IBullet
    {

        private float Speed { get; set; }

        public APBullet(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
            Width = 4;
            Height = 8;
        }

        public void Draw(Window window)//Draw AP type bullet
        {
            window.FillRectangle(Color.Red, X, Y, Width, Height);
        }

        public void Update()
        {
            Y -= Speed;
        }

        public bool IsOffScreen(Window window)
        {
            return Y < 0;
        }
        public float X{ get; set; }
        public float Y { get;  set; }
        public float Width { get;  set; }
        public float Height { get; set; }
    }
}