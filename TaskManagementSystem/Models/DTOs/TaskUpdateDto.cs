namespace TaskManagementSystem.Models.DTOs
{
    public class TaskUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
