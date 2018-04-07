using System;

namespace TaskList.API.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public bool Done { get; set; }
        public string Description { get; set; }
    }
}
