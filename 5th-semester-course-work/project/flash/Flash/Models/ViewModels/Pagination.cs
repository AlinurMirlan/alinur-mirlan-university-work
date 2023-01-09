namespace Flash.Models.ViewModels
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string? ReturnPage { get; set; }

        public Pagination(int currentPage, int pageCount, string? returnPage)
        {
            CurrentPage = currentPage;
            PageCount = pageCount;
            ReturnPage = returnPage;
        }
    }
}
