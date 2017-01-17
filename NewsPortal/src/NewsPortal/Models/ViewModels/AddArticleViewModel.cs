using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Models.ViewModels
{
    public class AddArticleViewModel
    {
        [Required]
        [StringLength(maximumLength: 70)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Summary { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
