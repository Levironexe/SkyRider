using SplashKitSDK;

namespace CustomProject.Interface
{
    public interface IBullet //Interface for bullet classes
    {
        // Properties for position
        float X { get; set; }
        float Y { get; set; }

        // Properties for size
        float Width { get; set; }
        float Height { get; set; }

        // Methods for bullet behavior
        void Update();
        public void Draw(Window window);
        bool IsOffScreen(Window window);
    }
}
