using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeShop.IService;
using WeShop.WeXin.Models;

namespace WeShop.WeXin.Controllers
{
    public class PublishController : Controller
    {   
        public IProductService ProductService { get; set; }

        public IPublishService PublishService { get; set; }

        public ActionResult Index(string laoban="1000")
        {
            PublishViewModel publishViewModel = new PublishViewModel();

            publishViewModel.Sort1 = PublishService.GetEntities(n =>n.UpCode=="A");
            publishViewModel.Sort2 = PublishService.GetEntities(b =>b.UpCode==laoban);

            return View(publishViewModel);
        }
    }
}