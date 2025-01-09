using CustomProject.Scenes;
using SplashKitSDK;

namespace CustomProject
{
    public class SceneFacade
    {
        private StartScene _startScene;
        private WinScene _winScene;
        private EndScene _endScene;
        private bool _winSoundPlayed;

        public SceneFacade()
        {
            _startScene = StartScene.Instance();
            _winScene = WinScene.Instance();
            _endScene = EndScene.Instance();
            _winSoundPlayed = false;
        }

        public void DrawStartScene() //Draw start scene before entering the game
        {
            _startScene.Draw();
        }

        public void DrawWinScene() //If player pass the attack phase, this scene appears and sound effects will be played
        {
            _winScene.Draw();
            if (!_winSoundPlayed)
            {
                _winScene.PlaySound();
                _winSoundPlayed = true;
            }
        }
        // Draw the end scene with given parameters
        public void DrawEndScene(int survivedTime, int totalDeaths, int highestSurvivedTime, int highestDesNum, int desNum) //If player collide with the obstacles, the player will lose and this screen shows up with their statistics.
        {
            _endScene.Draw(survivedTime, totalDeaths, highestSurvivedTime, highestDesNum, desNum);
        }
    }
}
