namespace Future_Generation.Common
{
    public abstract class EntitySC
    {
        public EntitySC()
        {
            PageSize = 10;
            Page = 1;
        }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
