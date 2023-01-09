namespace Flash.Models.ViewModels
{
    public class CardOnBrowsePage
    {
        public Flashcard Flashcard { get; set; }
        public int Page { get; set; }
        public string? SearchTerm { get; set; }

        public CardOnBrowsePage(Flashcard flashcard, int page, string? searchTerm)
        {
            Flashcard = flashcard;
            Page = page;
            SearchTerm = searchTerm;
        }
    }
}
