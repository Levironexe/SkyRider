using CustomProject.Interface;
using SplashKitSDK;

namespace CustomProject.GameObject
{
    public class MultipleBullet : IBullet
    {
        private float Speed { get; set; }

        public MultipleBullet(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
            Width = 3;
            Height = 12;
        }

        public void Draw(Window window)//Draw multiple type bullet
        {
            window.FillRectangle(Color.Yellow, X, Y, Width, Height);
            window.FillRectangle(Color.Yellow, X - 10, Y + 12, Width, Height);
            window.FillRectangle(Color.Yellow, X + 10, Y + 12 , Width, Height);
        }

        public void Update()//Update position of bullet
        {
            Y -= Speed;
        }

        public bool IsOffScreen(Window window) //Check for off screen
        {
            return Y < 0;
        }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}