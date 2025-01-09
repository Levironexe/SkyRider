using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.GameObject
{
    public class GameObject //This is the parent class of Player and Obstacle
    {
        protected Bitmap _image;
        public float _x, _y, _width, _height;
        private static GameObject _instance;
        private static readonly object _lock = new object();

        protected GameObject(float x, float y, float width, float height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }
        public static GameObject Instance() //Singleton implementation
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameObject(0, 0, 0, 0);
                    }
                }
            }
            return _instance;
        }
        public float X //For public access
        {
            get { return _x; }
            set { _x = value; }
        }        
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public virtual void Draw(Window window) { }
    }

    
    
}
