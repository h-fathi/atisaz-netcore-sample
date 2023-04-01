using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AtisazBazar.Services.DataTransferObjects
{
    public record TodoVM
    {
        public TodoVM() 
        {
            Id= Guid.NewGuid();
        }

        [JsonIgnore]
        public Guid Id { get; init; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Use letters and space only please")]
        public string Title { get; init; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500)]
        public string Description { get; init; }
        [Required(ErrorMessage = "Priority is required")]
        public Priority Priority { get; init; }
    }
}
