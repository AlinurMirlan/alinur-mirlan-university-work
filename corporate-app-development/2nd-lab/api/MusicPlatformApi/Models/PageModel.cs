namespace MusicPlatformApi.Models
{
    public class PageModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
    }
}
