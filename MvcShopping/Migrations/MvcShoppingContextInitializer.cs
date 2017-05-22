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
            var c1 = context.ProductCategories.Add(new ProductCategory() { Id = 1, Name = "���", Products = new List<Product>() });
            var c2 = context.ProductCategories.Add(new ProductCategory() { Id = 2, Name = "§�~", Products = new List<Product>() });
            var c3 = context.ProductCategories.Add(new ProductCategory() { Id = 3, Name = "���y", Products = new List<Product>() });
            var c4 = context.ProductCategories.Add(new ProductCategory() { Id = 4, Name = "���ҥΨ�", Products = new List<Product>() });
            
            context.SaveChanges();

            c1.Products.Add(new Product() { Name = c1.Name + "���O�U���ӫ~1", Color = Color.Red, Description = "N/A", Price = 99, PublishOn = DateTime.Now});
            c1.Products.Add(new Product() { Name = c1.Name + "���O�U���ӫ~2", Color = Color.Blue, Description = "N/A", Price = 150, PublishOn = DateTime.Now });

            c2.Products.Add(new Product() { Name = c2.Name + "���O�U���ӫ~1", Color = Color.Red, Description = "N/A", Price = 99, PublishOn = DateTime.Now });
            c2.Products.Add(new Product() { Name = c2.Name + "���O�U���ӫ~2", Color = Color.Blue, Description = "N/A", Price = 150, PublishOn = DateTime.Now });

            c3.Products.Add(new Product()
            {
                Name = "ASP.NET MVC 4 �}�o���",
                Price = 680,
                Description = "�ĤJ�@�̦h�~��ȶ}�o�u�@������A�q�[�����޳N���ѡ��}�o��ҡ��}�o�ޥ����w�˳��p�A�[�H��Ҹԭz�A�èϥγ̷s��Visual Studio 2012�}�o�u��A�������ظm�u�@��w���B��ֳt�B��e�����@�I",
                Color = Color.Red,
                PublishOn = DateTime.Parse("2012/12/20")
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
