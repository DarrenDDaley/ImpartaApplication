using System;

namespace TaskList.API.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public bool Done { get; set; }
        public string Task { get; set; }
    }
}
