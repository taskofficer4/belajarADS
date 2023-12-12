namespace Actris.Abstraction.Model.Request
{
    public class PagedFilter
    {
        public int Size { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string Sort { get; set; } = "id asc";
    }
}
