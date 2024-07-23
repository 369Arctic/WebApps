using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookHaven_Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }
        [Required]
        public int YearOfPublishing { get; set; }

        [Required]
        [Range(1, 15000)]
        [DisplayName("List Price")]
        public double ListPrice { get; set; } // диапазон цен

        [Required]
        [Range(1,15000)]
        [DisplayName("Price for 1-50")]
        public double Price { get; set; } // цена за 1шт

        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for 50+")]
        public double Price50 { get; set; } // цена при покупке более 50 штук

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]

        public Category Category { get; set; }
    }
}
