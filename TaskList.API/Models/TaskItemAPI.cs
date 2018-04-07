using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskList.API.Models
{
    public class TaskItemAPI : IValidatableObject
    {
        public bool Done { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Description)) {
                yield return new ValidationResult("Task can't be empty or null.");
            }
        }
    }
}
