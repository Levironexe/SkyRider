using CustomProject.GameObject;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CustomProject.Interface;
namespace CustomProject
{
    public class GameObjectFacade
    {
        private Player _player;
        private List<Obstacle> _obstacles;
        private List<IBullet> _bullets;
        private static SoundEffect _shootingSound;
        public BulletFactory.BulletType CurrentBulletType { get; set; } = BulletFactory.BulletType.APBullet;
        private const string ShootingSoundPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\shoot.wav";

        public GameObjectFacade()
        {
            _player = new Player(150, 150, 60, 60);
            _bullets = new List<IBullet>();
            _obstacles = new List<Obstacle>
            {
            new Obstacle(SplashKit.Rnd(-100, 800-(int)(559*0.35)), -50, 5, 50, 50),
            new Obstacle(SplashKit.Rnd(-100, 800-(int)(559*0.35)), -150, 4, 50, 50),
            new Obstacle(SplashKit.Rnd(-100, 800-(int)(559*0.35)), -250, 3, 50, 50),
            new Obstacle(SplashKit.Rnd(-100, 800-(int)(559*0.35)), -550, 7, 50, 50)
            };
        }


        public async void DrawObjects(Window window) //Draw player, bullets, obstacles, and aids
        {
            _player?.Draw(window);
            foreach (var obstacle in _obstacles)
            {
                obstacle.Draw(window);
                obstacle.Update(window);
            }
            foreach (var bullet in _bullets)
            {
                bullet.Draw(window);
            }
        }

        public void UpdateSpeed(double speed) //Update speed for obstacles
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.IncreaseSpeed(speed);
            }
        }

        public void DespawnObstacles() //Despawn obstacles if player wins
        {
            _obstacles.RemoveAll(obstacle => obstacle._y <= -50);
        }
        public void PlayerShoot()
        {
            IBullet bullet = BulletFactory.CreateBullet(CurrentBulletType,
                (2 * (_player.X + 246 * 7 / 20) + (246 * 3 / 10)) / 2 - 2,
                (_player.Y + 343 * 7 / 20),
                8);
            _bullets.Add(bullet);
        }

        public void UpdateBullet(Window window) //If bullets are off the screen, remove it
        {
            List<IBullet> bulletsToRemove = new List<IBullet>();
            foreach (var bullet in _bullets)
            {
                if (bullet.IsOffScreen(window))
                {
                    bulletsToRemove.Add(bullet);
                }
                else
                {
                    bullet.Update();
                }
            }
            foreach (var bullet in bulletsToRemove)
            {
                _bullets.Remove(bullet);
            }
        }
        public void Move() //Handle player movement and shooting
        {
            bool isMoving = false;
            _shootingSound = SplashKit.LoadSoundEffect("shoot", ShootingSoundPath);
            if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y >= 0 - 302 * 0.35) _player.Move(0, -7);
            if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y <= 380) _player.Move(0, 5);
            if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X >= 0 - 246 * 0.35)
            {
                _player.Move(-7, 0);
                _player.TurnLeft();
                isMoving = true;
            }
            if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X <= 640)
            {
                _player.Move(7, 0); 
                _player.TurnRight();
                isMoving = true;
            }
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                PlayerShoot();
                SplashKit.PlaySoundEffect(_shootingSound);
            }
            if (!isMoving)
            {
                _player.ResetImage();
            }
        }
        public Player Player { get { return _player; } }
        public List<Obstacle> Obstacles { get { return _obstacles; } }
        public List<IBullet> Bullets { get { return _bullets; } }
    }
}

