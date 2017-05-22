using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{
    [Authorize] // 必须登入会员才能使用订单结账功能
    public class OrderController : BaseController
    {
        // 显示完成订单的表单页面
        public ActionResult Complete()
        {
            return View();
        }

        // 将订单资料与购物车资料写入资料库
        [HttpPost]
        public ActionResult Complete(OrderHeader form)
        {
            var member = db.Members.Where(p => p.Email == User.Identity.Name).FirstOrDefault();
            if(member == null) return RedirectToAction("Index", "Home");

            if (this.Carts.Count == 0) return RedirectToAction("Index", "Cart");

            // 将订单资料与购物车资料写入资料库
            OrderHeader oh = new OrderHeader()
            {
                Member = member,
                ContactName = form.ContactName,
                ContactAddress = form.ContactAddress,
                ContactPhoneNo = form.ContactPhoneNo,
                BuyOn = DateTime.Now,
                Memo = form.Memo,
                OrderDetailItems = new List<OrderDetail>()
            };

            int total_price = 0;
            foreach (var item in this.Carts)
            {
                var product = db.Products.Find(item.Product.Id);
                if (product == null) return RedirectToAction("Index", "Cart");

                total_price += item.Product.Price * item.Amount;
                oh.OrderDetailItems.Add(new OrderDetail() { Product = product, Price = product.Price, Amount = item.Amount });
            }

            oh.TotalPrice = total_price;
            
            db.Orders.Add(oh);
            db.SaveChanges();

            // 订单完成后必须清空現有购物车资料
            this.Carts.Clear();

            // 订单完成后回到网站首页
            return RedirectToAction("Index", "Home");
        }

    }
}
