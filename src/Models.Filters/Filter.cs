namespace Models.Filters
{
    public abstract class Filter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
        public string SortBy { get; set; } = "Id";
        public bool SortAscending { get; set; } = true;
    }
}
