using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Interface
{
    public interface IGameStats //Interface for GameStats class
    {
        public void IncreaseNum(int num);
        public int HighestScore(int highestMinute);
        public void SaveHighestScore(string filename);
        public void LoadHighestScore(string filename);
        public void ResetGameData(string filename);
    }
}
