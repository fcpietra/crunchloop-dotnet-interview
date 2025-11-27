namespace TodoApi.Dtos
{
    public class UpdateTodoItem
    {
        public required string ItemDescription { get; set; }
        public bool Done { get; set; }

    }
}
