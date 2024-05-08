namespace TaskManagementSystem.Models.DTOs
{
    public class TaskGetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
