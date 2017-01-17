using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(20)]
        [RegularExpression("[\\w\\D]+", ErrorMessage = "Only letters can be used for category name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}