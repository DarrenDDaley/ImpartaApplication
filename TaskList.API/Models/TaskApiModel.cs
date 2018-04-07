using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskList.API.Models
{
    public class TaskApiModel : IValidatableObject
    {
        public bool Done { get; set; }
        public string Task { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Task)) {
                yield return new ValidationResult("Task can't be empty or null.");
            }
        }
    }
}
