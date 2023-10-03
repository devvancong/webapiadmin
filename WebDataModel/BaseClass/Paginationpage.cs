using System.Linq.Expressions;

namespace WebDataModel.BaseClass
{
    public class Paginationpage<TEnty>
    {
        public int PerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int Count { get; set; } = 1;
        public List<Expression<Func<TEnty, bool>>> Expression { get; set; } = new List<Expression<Func<TEnty, bool>>>();
    }
}