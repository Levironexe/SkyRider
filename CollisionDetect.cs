using CustomProject.GameObject;
using CustomProject.Interface;
using SplashKitSDK;
using System.Collections.Generic;


public class CollisionDetector
{
    private int x1 = 246; //These are the calculated coordinates of the image
    private int y1 = 343;
    private int x2 = 559;
    private int y2 = 302;

    private static CollisionDetector? _instance;
    private static readonly object _lock = new object();


    private CollisionDetector() { }


    public static CollisionDetector Instance() //Singleton implementation
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new CollisionDetector();
                }
            }
        }
        return _instance;
    }

    public bool DetectCollision(Player _player, List<Obstacle> obstacles)//Detect collisison between bullet and obstacles
    {
        Rectangle playerRect = SplashKit.RectangleFrom(_player.X + x1 * 0.35, _player.Y + y1 * 0.35, 246 * 0.3, 343 * 0.3);
        Point2D point1 = new Point2D() { X = playerRect.X, Y = playerRect.Y + playerRect.Height };
        Point2D point3 = new Point2D() { X = playerRect.X + playerRect.Width, Y = playerRect.Y + playerRect.Height };
        Point2D point2 = new Point2D() { X = playerRect.X + playerRect.Width / 2, Y = playerRect.Y };
        Triangle playerTri = SplashKit.TriangleFrom(point1, point2, point3);
        foreach (var obstacle in obstacles)
        {
            Rectangle obstacleRect = SplashKit.RectangleFrom((obstacle.X + x2 * 0.35) + 10, obstacle.Y + y2 * 0.35 + 30, (559 * 0.3) - 20, (302 * 0.3) - 50);
            Point2D point4 = new Point2D() { X = obstacleRect.X, Y = obstacleRect.Y };
            Point2D point5 = new Point2D() { X = obstacleRect.X + obstacleRect.Width, Y = obstacleRect.Y };
            Point2D point6 = new Point2D() { X = obstacleRect.X + obstacleRect.Width / 2, Y = obstacleRect.Y + obstacleRect.Height };
            Triangle obstacleTri = SplashKit.TriangleFrom(point5, point4, point6);

            if (SplashKit.TrianglesIntersect(playerTri, obstacleTri))
            {
                return true;
            }
        }
        return false;
    }
    public bool DetectBulletCollision(List<IBullet> bullets, List<Obstacle> obstacles) //Detect collisison between bullet and obstacles
    {
        bool collided = false;
        List<IBullet> bulletsToRemove = new List<IBullet>();
        foreach (var bullet in bullets)
        {
            Rectangle bulletRect = SplashKit.RectangleFrom(bullet.X, bullet.Y, bullet.Width, bullet.Height);
            foreach (var obstacle in obstacles)
            {
                Rectangle obstacleRect = SplashKit.RectangleFrom((obstacle.X + x2 * 0.35) + 10, obstacle.Y + y2 * 0.35 + 30, (559 * 0.3) - 20, (302 * 0.3) - 50);
                Point2D point4 = new Point2D() { X = obstacleRect.X, Y = obstacleRect.Y };
                Point2D point5 = new Point2D() { X = obstacleRect.X + obstacleRect.Width, Y = obstacleRect.Y };
                Point2D point6 = new Point2D() { X = obstacleRect.X + obstacleRect.Width / 2, Y = obstacleRect.Y + obstacleRect.Height };
                Triangle obstacleTri = SplashKit.TriangleFrom(point5, point4, point6);
                if (SplashKit.TriangleRectangleIntersect(obstacleTri, bulletRect))
                {
                    bulletsToRemove.Add(bullet);
                    obstacle.X = SplashKit.Rnd(SplashKit.Rnd(800) - (int)(559 * 0.35));
                    obstacle.Y = -250;
                    collided = true;
                }
            }
        }

        // Remove collided bullets
        foreach (var bullet in bulletsToRemove)//The bullets added to the list will be deleted
        {
            bullets.Remove(bullet);
        }

        return collided; //Return the boolean value
    }
}

    
   

    
