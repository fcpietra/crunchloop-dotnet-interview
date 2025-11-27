namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public required string ItemDescription { get; set; }
        public long IdList { get; set; }
        public bool Done { get; set; }
    }
}
