using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeShop.IService;
using WeShop.WeXin.Models;

namespace WeShop.WeXin.Controllers
{
    public class ProductsController : Controller
    {
        public IProductService ProductService { get; set; }
        

        public ActionResult Index(string proCode = "1001")
        {
            PublishViewModel pro = new PublishViewModel();

            pro.Products = ProductService.GetEntities(n => n.Sorts.First().Code == proCode);
            
            return View(pro);
        }
    }
}