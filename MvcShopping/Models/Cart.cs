using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcShopping.Models
{
    public class Cart
    {
        [DisplayName("選購商品")]
        [Required]
        public Product Product { get; set; }

        [DisplayName("選購数量")]
        [Required]
        public int Amount { get; set; }
    }
}
