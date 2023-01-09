namespace Flash.Models.ViewModels
{
    public class CardOnRehearsePage
    {
        public Flashcard Flashcard { get; set; }
        public Deck Deck { get; set; }
        public int Page { get; set; }

        public CardOnRehearsePage(Flashcard flashcard, Deck deck, int page)
        {
            Flashcard = flashcard;
            Deck = deck;
            Page = page;
        }
    }
}
