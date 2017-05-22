namespace MvcShopping.Migrations
{
    using MvcShopping.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.Linq;

    internal sealed class MvcShoppingContextInitializer : DropCreateDatabaseIfModelChanges<MvcShoppingContext>
    {
        public MvcShoppingContextInitializer()
        {
        }

        protected override void Seed(MvcShoppingContext context)
        {
            var c1 = context.ProductCategories.Add(new ProductCategory() { Id = 1, Name = "文具", Products = new List<Product>() });
            var c2 = context.ProductCategories.Add(new ProductCategory() { Id = 2, Name = "禮品", Products = new List<Product>() });
            var c3 = context.ProductCategories.Add(new ProductCategory() { Id = 3, Name = "書籍", Products = new List<Product>() });
            var c4 = context.ProductCategories.Add(new ProductCategory() { Id = 4, Name = "美勞用具", Products = new List<Product>() });
            
            context.SaveChanges();

            c1.Products.Add(new Product() { Name = c1.Name + "類別下的商品1", Color = Color.Red, Description = "N/A", Price = 99, PublishOn = DateTime.Now});
            c1.Products.Add(new Product() { Name = c1.Name + "類別下的商品2", Color = Color.Blue, Description = "N/A", Price = 150, PublishOn = DateTime.Now });

            c2.Products.Add(new Product() { Name = c2.Name + "類別下的商品1", Color = Color.Red, Description = "N/A", Price = 99, PublishOn = DateTime.Now });
            c2.Products.Add(new Product() { Name = c2.Name + "類別下的商品2", Color = Color.Blue, Description = "N/A", Price = 150, PublishOn = DateTime.Now });

            c3.Products.Add(new Product()
            {
                Name = "ASP.NET MVC 4 開發實戰",
                Price = 680,
                Description = "融入作者多年實務開發工作之精髓，從觀念→技術講解→開發實例→開發技巧→安裝部署，加以實例詳述，並使用最新的Visual Studio 2012開發工具，讓網站建置工作更安全、更快速、更容易維護！",
                Color = Color.Red,
                PublishOn = DateTime.Parse("2012/12/20")
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
