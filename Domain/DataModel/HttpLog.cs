namespace Domain.DataModel
{
    public class HttpLog
    {
        public int Id { get; set; }
        public string? IP { get; set; }
        public string Method { get; set; }
        public string URL { get; set; }
        public IDictionary<string, string[]> RequestHeader { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Request { get; set; }
        public IDictionary<string, string[]> ResponseHeader { get; set; }
        public DateTime ResponsedAt { get; set; }
        public string Response { get; set; }
        public long Duration { get; set; }
    }
}
