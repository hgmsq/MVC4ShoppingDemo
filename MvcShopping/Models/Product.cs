using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MvcShopping.Models
{
    [DisplayName("商品资讯")]
    [DisplayColumn("Name")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("商品類別")]
        [Required]
        public virtual ProductCategory ProductCategory { get; set; }

        [DisplayName("商品名稱")]
        [Required(ErrorMessage = "请输入商品名稱")]
        [MaxLength(60, ErrorMessage = "商品名稱不可超过60个字")]
        public string Name { get; set; }

        [DisplayName("商品簡介")]
        [Required(ErrorMessage = "请输入商品簡介")]
        [MaxLength(250, ErrorMessage = "商品簡介请勿输入超过250个字")]
        public string Description { get; set; }

        [DisplayName("商品顏色")]
        [Required(ErrorMessage = "请選擇商品顏色")]
        public Color Color { get; set; }

        [DisplayName("商品售卖")]
        [Required(ErrorMessage = "请输入商品售卖")]
        [Range(99, 10000, ErrorMessage = "商品售卖必须介于 99 ~ 10,000 之间")]
        public int Price { get; set; }

        [DisplayName("上架时间")]
        [Description("如果不設定上架时间，代表此商品永不上架")]
        public DateTime? PublishOn { get; set; }
    }
}
