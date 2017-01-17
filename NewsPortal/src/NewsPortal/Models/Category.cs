﻿using System;
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
        [RegularExpression("[\\w]+")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual List<Article> Articles { get; set; }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Category()
        {
            
        }
    }
}