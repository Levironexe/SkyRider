using CustomProject.Interface;
using System.Diagnostics.Contracts;

public class ObstacleDestroyed : IGameStats
{
    private static ObstacleDestroyed _instance;
    private static readonly object _lock = new object();
    private int _desNum;
    private int _highestNum;
    private Stack<int> _mostDes = new Stack<int>();

    private ObstacleDestroyed()
    {
        _desNum = 0;
    }

    public static ObstacleDestroyed Instance()//Singleton implementation
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ObstacleDestroyed();
                }
            }
        }
        return _instance;
    }

    public int HighestScore(int highestNum)//Passed the score and check for if this score is higher than the previous one. If so, pushes it to the stack and returns its value
    {
        _highestNum = highestNum;
        if (_mostDes.Count() == 0)
        {
            _mostDes.Push(_highestNum);
        }
        else
        {
            if (_mostDes.Peek() < highestNum)
            {
                _mostDes.Push(highestNum);
            }
        }
        return _mostDes.Peek();
    }
    public void IncreaseNum(int death)//If bullet collideds with obstacle, it will increase this value
    {
        _desNum += death;
    }
    public void SaveHighestScore(string filename)//This method will save the highest score to a text file
    {
        StreamWriter highScoreWriter;
        highScoreWriter = new StreamWriter(filename);
        highScoreWriter.WriteLine(_mostDes.Peek());
        highScoreWriter.Close();
    }
    public void LoadHighestScore(string filename)//This method will load that highest score stored in the file
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
                    if (_mostDes.Count == 0 || _mostDes.Peek() < loadedScore)
                    {
                        _mostDes.Push(loadedScore);
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
        while (_mostDes.Count() > 0)
        {
            _mostDes.Pop();
        }
        StreamWriter highScoreWriter;
        highScoreWriter = new StreamWriter(filename);
        highScoreWriter.WriteLine("0");
        highScoreWriter.Close();
    }

    public void NewGame()
    {
        _desNum = 0;
    }
    public int Death => _desNum;
}
