using CustomProject;
using CustomProject.Interface;
using ShapeDrawer;
using SplashKitSDK;
using System;
using System.Globalization;
using System.IO;
public class SurvivedTime : IGameStats
{
    private static SurvivedTime _instance;
    private static readonly object _lock = new object();
    private int _death;
    private int _highestMinute;
    private int _highestSecond;
    private Stack<int> _highScore = new Stack<int>();
    private int highscore;
    private GameObjectFacade _saveData;

    private SurvivedTime()
    {
        _death = 0;
    }

    public static SurvivedTime Instance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SurvivedTime();
                }
            }
        }
        return _instance;
    }

    public void IncreaseNum(int death)//If player collideds with obstacle, it will increase this value
    {
        _death += death;
    }
    public int HighestScore(int highestMinute)//Passed the score and check for if this score is higher than the previous one. If so, pushes it to the stack and returns its value
    {
        _highestMinute = highestMinute;
        if (_highScore.Count() == 0)
        {
            _highScore.Push(highestMinute);
        }
        else
        {
            if (_highScore.Peek()  < highestMinute)
            {
                _highScore.Push(highestMinute);
            }
        }
        return _highScore.Peek();
    }
    public void SaveHighestScore(string filename) //After storing the highest score to the stack, I set this method to write it to a .txt file in order to load it later
    {
        StreamWriter highScoreWriter;
        highScoreWriter = new StreamWriter(filename);
        highScoreWriter.WriteLine(_highScore.Peek());
        highScoreWriter.Close();
    }
    public void LoadHighestScore(string filename) //Read the first line of the .txt file to get the high score data
    {
        if (!File.Exists(filename))
        {
            StreamWriter highScoreWriter = new StreamWriter(filename);
        }

        using (StreamReader highScoreReader = new StreamReader(filename))
        {
            try
            {
                string line = highScoreReader.ReadLine();
                if (int.TryParse(line, out int loadedScore))
                {
                    if (_highScore.Count == 0 || _highScore.Peek() < loadedScore)
                    {
                        _highScore.Push(loadedScore);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }
    public void ResetGameData(string filename) //Clear high score stack and then replace the first line of the .txt file to 0
    {
        while (_highScore.Count() > 0)
        {
            _highScore.Pop();
        }
        StreamWriter highScoreWriter;
        highScoreWriter = new StreamWriter(filename);
        highScoreWriter.WriteLine("0");
        highScoreWriter.Close();
    }
    public int Death => _death;
}
