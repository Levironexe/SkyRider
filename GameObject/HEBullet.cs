using CustomProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace CustomProject.GameObject
{
    public class HEBullet : IBullet
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        private float Speed { get; set; }

        public HEBullet(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
            Width = 6;
            Height = 10;
        }
        public void Draw(Window window)//Draw HE type bullet
        {
            window.FillRectangle(Color.Blue, X, Y, Width, Height);
            window.DrawRectangle(Color.White, X, Y, Width, Height);
        }

        public void Update() //Update the position of bullet in the screen
        {
            Y -= Speed;
        }

        public bool IsOffScreen(Window window) //Check if the bullet is off screen
        {
            return Y < 0;
        }
    }
}
