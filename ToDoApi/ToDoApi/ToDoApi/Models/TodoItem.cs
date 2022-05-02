using Microsoft.EntityFrameworkCore;
namespace ToDoApi.Models
{
    public class TodoItem
    {
        public long ItemID { get; set; }
        public string? ItemName { get; set; }
        public bool ItemIsComplete { get; set; }
        public string? Secret { get; set; }
    }

    public class TodoItemDTO
    {
        [Key]
        public long ItemID { get; set; }
        public string? ItemName { get; set; }
        public bool ItemIsComplete { get; set; }
    }
}
