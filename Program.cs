using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomProject.GameObject;
using CustomProject.Scenes;
using SplashKitSDK;

namespace CustomProject
{
    public class Program
    {
        private static GameObjectFacade _gameObject;
        private static SceneFacade _scene;
        private static CollisionDetector _collisionDetector;
        private static Font _font;
        private static Window _window;
        private static Bitmap _background;
        private static Bitmap _afterBackground;
        private static SoundEffect _countdownSound;
        private static SoundEffect _loseSound;
        private static SoundEffect _startSound;
        private static SoundEffect _explosionSound;
        private static Music _backgroundMusic;
        private static Music _jetEngine;
        private static SurvivedTime _gameStats;
        private static Counter _gameTimer;
        private static ObstacleDestroyed _obstacleDestroyed;

        private static bool _loseDetected = false;
        private static bool _gameStarted = false;
        private static bool _startCounting = true;
        private static int _countdown = 3;
        private static bool _countdownFinished = false;
        private static bool _inLobby = true;
        private static int _speedIncreasePeriod = 0;
        private static int _runTime = 0;
        private static bool _won = false;
        private static bool hasAid = false;

        private const string GameTitle = "Sky Rider";
        private const int WindowWidth = 800;
        private const int WindowHeight = 600;
        private const string FontPath = "verdana.ttf";
        private const string BackgroundImagePath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\background1.jpg";
        private const string AfterBackgroundImagePath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Images\\background2.jpg";
        private const string CountdownSoundPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\countdown.wav";
        private const string StartSoundPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\start.wav";
        private const string LoseSoundPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\lose (2).wav";
        private const string BackgroundMusicPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\background.mp3";
        private const string JetEngineMusicPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\jet-engine.mp3";
        private const string ExplosionSoundPath = "C:\\Users\\Phuoc\\PhuocCon\\Major\\COS20007 - OOP\\6.5HD\\CustomProject\\Resources\\Sounds\\explosion.mp3";

        public static async Task Main()
        {
            InitializeResources();
            await GameLoop();
        }

        private static void InitializeResources()
        {
            _window = new Window(GameTitle, WindowWidth, WindowHeight);
            _background = new Bitmap("background", BackgroundImagePath);
            _afterBackground = new Bitmap("after_background", AfterBackgroundImagePath);
            _font = SplashKit.LoadFont("Verdana", FontPath);
            _countdownSound = SplashKit.LoadSoundEffect("countdown", CountdownSoundPath);
            _startSound = SplashKit.LoadSoundEffect("start", StartSoundPath);
            _loseSound = SplashKit.LoadSoundEffect("lose", LoseSoundPath);
            _backgroundMusic = SplashKit.LoadMusic("background", BackgroundMusicPath);
            _jetEngine = SplashKit.LoadMusic("jet_engine", JetEngineMusicPath);
            _explosionSound = SplashKit.LoadSoundEffect("explode", ExplosionSoundPath);


            InitializeGame();
        }

        private static void InitializeGame()
        {
            _collisionDetector = CollisionDetector.Instance();
            _gameStats = SurvivedTime.Instance();
            _obstacleDestroyed = ObstacleDestroyed.Instance();

            _gameStarted = false;
            _countdownFinished = false;
            _countdown = 3;
            _startCounting = true;
            _loseDetected = false;
            _gameObject = new GameObjectFacade();
            _scene = new SceneFacade();
            _gameTimer = new Counter();
            _ = StartCountdown();
            _gameStats.LoadHighestScore("TimeSurvived.txt");
            _obstacleDestroyed.LoadHighestScore("EnemyDestroyed.txt");
            _obstacleDestroyed.NewGame();

        }

        private static async Task GameLoop()
        {
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                _window.Clear(Color.White);

                if (_inLobby)
                {
                    DrawLobby();
                }
                else if (_countdownFinished)
                {
                    _gameStarted = true;
                    await RunGame();
                }
                else if (!_countdownFinished)
                {
                    DisplayCountdown();
                }

                _window.Refresh(12);
            }

            _window.Close();
        }

        private static void DrawLobby()
        {
            _scene.DrawStartScene();
        }

        private static async Task RunGame()
        {
            _window.DrawBitmap(_background, 0, 0);
            if (_collisionDetector.DetectCollision(_gameObject.Player, _gameObject.Obstacles))
            {
                _window.DrawBitmap(_afterBackground, 0, 0);
                HandleCollision();
                DisplayEndScene();
                _gameStarted = false;
                _runTime = 0;
            }
            else
            {
                UpdateGame();
            }
        }

        private static async void HandleCollision()
        {
            if (!_loseDetected)
            {
                _loseDetected = true;
                _startCounting = false;
                _speedIncreasePeriod = 0;
                SplashKit.PlaySoundEffect(_explosionSound);
                SplashKit.StopMusic();
                await Task.Delay(200);
                SplashKit.PlaySoundEffect(_loseSound);
                 _gameStats.IncreaseNum(1);
                _gameStats.SaveHighestScore("TimeSurvived.txt");
                _obstacleDestroyed.SaveHighestScore("EnemyDestroyed.txt");
            }
        }
        private async static void UpdateGame()
        {
            SplashKit.DrawText($"Attack phase countdown: {_gameTimer.ShowTime()}", Color.Black, _font, 20, 10, 10);
            SplashKit.DrawText($"Destroyed Obstacles: {_obstacleDestroyed.Death}", Color.Black, _font, 20, 10, 50);

            _gameObject.UpdateBullet(_window);
            _gameObject.DrawObjects(_window);
            _gameObject.Move();
            HandleBuleltType();

            if (_speedIncreasePeriod == 5)
            {
                _gameObject.UpdateSpeed(0.5);
                _speedIncreasePeriod = 0;
            }

            if (_runTime >= 25)
            {
                _gameObject.DespawnObstacles();
                SplashKit.StopMusic();
                _speedIncreasePeriod = 0;
                _loseDetected = false;
                _scene.DrawWinScene();
            }
            if (_collisionDetector.DetectBulletCollision(_gameObject.Bullets, _gameObject.Obstacles))
            {
                SplashKit.PlaySoundEffect(_explosionSound);
                Console.WriteLine("Bullet hit");
                ObstacleDestroyed.Instance().IncreaseNum(1);
            }
        }
        public static void HandleBuleltType()
        {
            if (SplashKit.KeyTyped(KeyCode.Num1Key))
            {
                _gameObject.CurrentBulletType = BulletFactory.BulletType.APBullet;
            }            
            if (SplashKit.KeyTyped(KeyCode.Num2Key))
            {
                _gameObject.CurrentBulletType = BulletFactory.BulletType.HEBullet;
            }            
            if (SplashKit.KeyTyped(KeyCode.Num3Key))
            {
                _gameObject.CurrentBulletType = BulletFactory.BulletType.MultipleBullet;
            }
        }
        private static async Task StartCountdown()
        {
            if (!_inLobby)
            {
                for (int i = _countdown; i >= 1; i--)
                {
                    _countdown = i;
                    SplashKit.PlaySoundEffect(_countdownSound);
                    await Task.Delay(1000);
                }

                SplashKit.PlaySoundEffect(_startSound);
                _countdown = 0;
                await Task.Delay(1000);

                SplashKit.PlayMusic(_backgroundMusic, -1);
                SplashKit.PlayMusic(_jetEngine, -1, 15);

                _countdownFinished = true;
            }
        }
        private static void DisplayCountdown()
        {
            _window.Clear(Color.White);

            if (_countdown > 0)
            {
                _window.DrawBitmap(_background, 0, 0);
                SplashKit.DrawText(_countdown.ToString(), Color.Black, _font, 60, 380, 250);
            }
            else
            {
                _window.DrawBitmap(_background, 0, 0);
                SplashKit.DrawText("Start!", Color.Black, _font, 60, 330, 250);
            }
        }

        private static void DisplayEndScene()
        {
            int totalDeaths = _gameStats.Death;
            _scene.DrawEndScene(60 - _gameTimer.ShowTime(), totalDeaths, _gameStats.HighestScore(60 - _gameTimer.ShowTime()), _obstacleDestroyed.HighestScore(_obstacleDestroyed.Death), _obstacleDestroyed.Death);
        }

        public static void ButtonClicked(string text)
        {
            switch (text)
            {
                case "Play":
                    _inLobby = false;
                    InitializeGame();
                    _ = StartScoreTracking();
                    break;
                case "Retry":
                    InitializeGame();
                    _runTime = 0;
                    _ = StartScoreTracking();
                    break;
                case "Exit":
                    _window.Close();
                    break;
                case "Reset game data":
                    _gameStats.ResetGameData("TimeSurvived.txt");                    
                    _obstacleDestroyed.ResetGameData("EnemyDestroyed.txt");
                    break;
            }
        }

        private static async Task StartScoreTracking()
        {
            while (_startCounting)
            {
                _gameTimer.Ticks();
                await Task.Delay(1000);
                _speedIncreasePeriod++;
                _runTime++;
            }
        }
    }
}
