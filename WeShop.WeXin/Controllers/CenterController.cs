using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WeShop.IService;
using WeShop.WeXin.Models;
using WeShop.WeXin.Filters;

namespace WeShop.WeXin.Controllers
{
    [OAuthFilter]
    public class CenterController : Controller
    {
        public IUserService UserService { get; set; }
        public IProductService ProductService { get; set; }
        // GET: Center
        public ActionResult Index()
        {
            var userinfo = Session["userinfo"] as OAuthUserInfo;
            return View(userinfo);
        }

        public ActionResult Center_Order()
        {
            return View();
        }

        public ActionResult Center_Order_Dfk()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Products = ProductService.GetEntities(n => true);
            return View(homeViewModel);
        }

        public ActionResult Center_Order_Dfh()
        {
            return View();
        }

        public ActionResult Center_Order_Dsh()
        {
            return View();
        }

        public ActionResult Center_Order_Dpj()
        {
            return View();
        }

        public ActionResult Center_Order_Tk()
        {
            return View();
        }
    }
}