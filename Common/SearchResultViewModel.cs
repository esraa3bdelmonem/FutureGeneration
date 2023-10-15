namespace Future_Generation.Common
{
    public class SearchResultViewModel<T>
    {
        public SearchResult<T>? SearchResult { get; set; }
        public EntitySC? SearchCriteria { get; set; }
    }
}
