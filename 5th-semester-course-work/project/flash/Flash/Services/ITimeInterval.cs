using Flash.Models;

namespace Flash.Services
{
    public interface ITimeInterval
    {
        public string GetTimeInterval(int daysCount);
        public int GetNewInterval(RetentionDifficulty difficulty, float difficultyRate, int interval);
    }
}
