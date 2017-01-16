using System;
using System.Collections.Generic;

namespace NewsPortal.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}