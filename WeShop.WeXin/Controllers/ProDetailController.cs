using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeShop.IService;
using WeShop.WeXin.Models;

namespace WeShop.WeXin.Controllers
{
    public class ProDetailController : Controller
    {
        public IProductService ProductService { get; set; }

        public IPublishService PublishService { get; set; }
        public IStockService StockService { get; set; }
        public ITagService TagService { get; set; }

        public ActionResult Index(string prodetailCode)
        {
            PublishViewModel publishViewModel = new PublishViewModel();

            publishViewModel.Products = ProductService.GetEntities(n => n.Code == prodetailCode);
            publishViewModel.Stocks = StockService.GetEntities(b => b.ProCode == prodetailCode);
            publishViewModel.Tags=TagService.GetEntities(c => c.Products.First().Code== prodetailCode);
            return View(publishViewModel);
        
        }
    }
}