namespace Domain.Dto._Base
{
    public class PagedQueryDto
    {
        public string OrderField { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
