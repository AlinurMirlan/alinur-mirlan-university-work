namespace Flash.Models.ViewModels
{
    public class DeckOnPage
    {
        public Deck Deck { get; set; }
        public int Page { get; set; }
        public string? SearchTerm { get; set; }

        public DeckOnPage(Deck deck, int page, string? searchTerm)
        {
            Deck = deck;
            Page = page;
            SearchTerm = searchTerm;
        }
    }
}
