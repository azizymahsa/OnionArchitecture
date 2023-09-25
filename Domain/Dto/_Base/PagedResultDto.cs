namespace Domain.Dto._Base
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> List { get; set; }
        public int Total { get; set; }
    }
}
