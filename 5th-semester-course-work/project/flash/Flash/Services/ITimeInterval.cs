using Flash.Models;

namespace Flash.Services
{
    public interface ITimeInterval
    {
        /// <summary>
        /// Converts days to _y. _m. _d. format where _ is a number and y - years, m - months, and d - days.
        /// </summary>
        /// <param name="daysCount">Amount of days to represent as a time interval.</param>
        /// <returns>_y. _m. _d. formatted string.</returns>
        public string GetTimeInterval(int daysCount);

        /// <summary>
        /// Calculates a new repetition interval for a flashcard based on how difficult it was to remember it.
        /// </summary>
        /// <param name="difficulty">Difficulty of remembering a flashcard.</param>
        /// <param name="difficultyRate">Retention rate(essentially, a multiplier for the next interval) of the provided difficulty.</param>
        /// <param name="interval">Previous repetition interval of the flashcard.</param>
        /// <returns>New repetition interval.</returns>
        public int GetNewInterval(RetentionDifficulty difficulty, float difficultyRate, int interval);
    }
}
