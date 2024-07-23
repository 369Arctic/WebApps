using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHaven_Models.ViewModels
{
    public class ShoppingCartVM // Strongly Typed Views - строгая типизация модели для конкретной View. 
    {
        public IEnumerable<ShoppingCart> ShoppingCartsList { get; set; }

        public Order Order { get; set; }

        // добавить стоимость товара
    }
}
