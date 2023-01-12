using Flash.Models;

namespace Flash.Services.Repositories
{
    public interface IFlashcardRepository
    {
        /// <summary>
        /// Adds a flashcard.
        /// </summary>
        /// <param name="flashcard">Flashcard to add.</param>
        /// <returns></returns>
        public Task<Flashcard> AddAsync(Flashcard flashcard);

        /// <summary>
        /// Gets a flashcard with the given Id.
        /// </summary>
        /// <param name="flashcardId">Id of the flashcard.</param>
        /// <returns>User</returns>
        public Task<Flashcard?> GetAsync(int flashcardId);

        /// <summary>
        /// Updates a flashcard with new content.
        /// </summary>
        /// <param name="flashcardId">Id of the flashcard to update.</param>
        /// <param name="front">Updated front-side of the flashcard.</param>
        /// <param name="back">Updated back-side of the flashcard.</param>
        /// <returns></returns>
        public Task UpdateFlashcardAsync(int flashcardId, string? front, string? back);

        /// <summary>
        /// Renews a flashcard's interval.
        /// </summary>
        /// <param name="flashcardId">Id of the flashcard to update.</param>
        /// <param name="newInterval">New interval.</param>
        /// <returns></returns>
        public Task UpdateFlashcardAsync(int flashcardId, int newInterval);

        /// <summary>
        /// Deletes a flashcard.
        /// </summary>
        /// <param name="deckId">Id of the flashcard.</param>
        /// <returns></returns>
        public Task DeleteAsync(int flashcardId);

        /// <summary>
        /// Gets a portion/part of the deck's Flashcards (pagination).
        /// </summary>
        /// <param name="deckId">Id of the deck.</param>
        /// <param name="page">Portion/part.</param>
        /// <param name="pageCount">Amount of portions/parts overall.</param>
        /// <returns>Portion of the deck's Flashcards</returns>
        public IEnumerable<Flashcard> GetDueFlashcardsPartitioned(int deckId, int page, out int pageCount);

        /// <summary>
        /// Gets a portion/part of the deck's Flashcards with the search term (pagination).
        /// </summary>
        /// <param name="deckId">Id of the deck.</param>
        /// <param name="page">Portion/part.</param>
        /// <param name="searchTerm">Search term predicate to apply for Flashcard's Front and Back properties when getting Flashcards.</param>
        /// <param name="pageCount">Amount of portions/parts overall.</param>
        /// <returns>Portion of the deck's Flashcards</returns>
        public IEnumerable<Flashcard> GetFlashcardsPartitioned(int deckId, int page, string? searchTerm, out int pageCount);
    }
}
