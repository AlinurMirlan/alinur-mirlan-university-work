using Flash.Models;
using System.Text;

namespace Flash.Services
{
    public class TimeInterval : ITimeInterval
    {
        public int GetNewInterval(RetentionDifficulty difficulty, float difficultyRate, int interval)
        {
            int newInterval = interval;
            if (interval == 0)
            {
                switch (difficulty)
                {
                    case RetentionDifficulty.Again:
                        newInterval = 0;
                        break;
                    case RetentionDifficulty.Hard:
                        newInterval = 0;
                        break;
                    case RetentionDifficulty.Good:
                        newInterval = 1;
                        break;
                }

                return newInterval;
            }

            switch (difficulty)
            {
                case RetentionDifficulty.Again:
                    newInterval = 0;
                    break;
                case RetentionDifficulty.Hard:
                    newInterval = (int)Math.Round(interval * difficultyRate);
                    break;
                case RetentionDifficulty.Good:
                    newInterval += (int)Math.Round(interval * difficultyRate);
                    break;
            }

            return newInterval;
        }

        public string GetTimeInterval(int daysCount)
        {
            StringBuilder stringBuilder = new();
            var (years, months, days) = GetDateParts(daysCount);
            bool isFlashcardNew = true;
            if (years != 0)
            {
                stringBuilder.Append($"{years}y. ");
                isFlashcardNew = false;
            }
            if (months != 0)
            {
                stringBuilder.Append($"{months}m.");
                isFlashcardNew = false;
            }
            if (days != 0)
            {
                stringBuilder.Append($"{days}d.");
                isFlashcardNew = false;
            }

            if (isFlashcardNew)
            {
                stringBuilder.Append($"0d.");
            }
            return stringBuilder.ToString();
        }

        private static (int, int, int) GetDateParts(int days)
        {
            int years = days / 365;
            int months = (days - (years * 365)) / 30;
            days -= (months * 30);
            return (years, months, days);
        }
    }
}
