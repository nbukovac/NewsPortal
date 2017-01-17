using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Models.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(maximumLength: 20)]
        [RegularExpression("[\\w]+", ErrorMessage = "Only letters can be used for category name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
