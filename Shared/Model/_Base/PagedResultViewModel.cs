namespace Shared.Model._Base
{
    public class PagedResultViewModel<T>
    {
        public IEnumerable<T> List { get; set; }
        public int Total { get; set; }
    }
}
