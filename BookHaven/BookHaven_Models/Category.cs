using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookHaven_Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Category")]
        [MaxLength(50)] // проверка валидации
        public string Name { get; set; }
        [DisplayName("Display Order")] // имя, отображаемое во View 
        [Range(1, 50, ErrorMessage = "The display order should not exceed 50")]
        public int DisplayOrder { get; set; }
    }
}
