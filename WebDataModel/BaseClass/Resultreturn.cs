namespace WebDataModel.BaseClass
{
    public class Resultreturn<TModel>
        where TModel : class
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public bool Status { get; set; }
        public IEnumerable<TModel>? Results { get; set; }
        public int TotalCount { get => 0; set => Results.Count(); }
    }
}