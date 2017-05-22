using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcShopping.Controllers
{
    public class HomeController : BaseController
    {
        // 首页
        public ActionResult Index()
        {
            var data = db.ProductCategories.ToList();

            return View(data);
        }

        // 商品列表
        public ActionResult ProductList(int id, int p = 1)
        {
            var productCategory = db.ProductCategories.Find(id);

            if (productCategory != null)
            {
                var data = productCategory.Products.ToList();

                var pagedData = data.ToPagedList(pageNumber: p, pageSize: 10);

                return View(pagedData);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // 商品明细
        public ActionResult ProductDetail(int id)
        {
            var data = db.Products.Find(id);

            return View(data);
        }
    }
}
