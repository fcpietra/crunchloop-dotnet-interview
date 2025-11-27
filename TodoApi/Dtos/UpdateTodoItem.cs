namespace TodoApi.Dtos
{
    public class UpdateTodoItem
    {
        public required string itemDescription { get; set; }
        public bool done { get; set; }

    }
}
