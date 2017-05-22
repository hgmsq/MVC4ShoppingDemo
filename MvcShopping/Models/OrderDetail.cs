using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MvcShopping.Models
{
    [DisplayName("订单明細")]
    [DisplayColumn("Name")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("订单主档")]
        [Required]
        public virtual OrderHeader OrderHeader { get; set; }

        [DisplayName("订购商品")]
        [Required]
        public Product Product { get; set; }

        [DisplayName("商品售卖")]
        [Required(ErrorMessage = "请输入商品售卖")]
        [Range(99, 10000, ErrorMessage = "商品售卖必须介于 99 ~ 10,000 之间")]
        [Description("由于商品售卖可能会经常变动，因此必须保留购买当下的商品售卖")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [DisplayName("购买数量")]
        [Required]
        public int Amount { get; set; }
    }
}
