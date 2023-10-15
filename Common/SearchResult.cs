namespace Future_Generation.Common
{
    public class SearchResult<T>
    {
        public int TotalRaws { get; set; }
        public List<T> List { get; set; } = default!;

    }
}
