using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class Category
    {
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        [RegularExpression("[\\w]+", ErrorMessage = "Only letters can be used for category name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual List<Article> Articles { get; set; }

        public Category(string name, string description)
        {
            CategoryId = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public Category()
        {
            
        }
    }
}