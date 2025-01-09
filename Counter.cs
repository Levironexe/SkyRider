using System;



public class Counter
{
    private TimeFunc seconds;
    private SurvivedTime? _longestTime;

    public Counter()
    {
        seconds = new TimeFunc("Seconds", 65);
    }

    public void Ticks()
    {
        seconds.Decrement();
        if (seconds.Ticks == 0)
        {
            seconds.Reset();
        }
    }
    public int ShowTime()
    {
        return seconds.Ticks;
    }
    public void LongestTime() 
    {
        _longestTime.HighestScore(seconds.Ticks);
    }
}

