using System;
using System.Linq;
using System.Web.Mvc;
using WeShop.EFModel;
using WeShop.IService;
using WeShop.WeXin.Models;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace WeShop.WeXin.Controllers
{
    public class ShoppingCartController : Controller
    {
        public ICustomerService CustomerService { get; set; }
        public IShoppingCartService ShoppingCartService { get; set; }
        public IProductService ProductService { get; set; }
        // GET: ShopingCart
        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            int cid = Convert.ToInt32(Session["Id"]);
            homeViewModel.ShoppingCarts = ShoppingCartService.GetEntities(c=>c.CusId==cid);
            return View(homeViewModel);
        }

        public ActionResult Insert(string date)
        {
            ShoppingCart cart = new ShoppingCart();
            string openid = Session["openid"].ToString();
            int num = Convert.ToInt32(Request["num"]);
            string code = Request["codes"].ToString();
            cart.ProCode = code;
            cart.Qty = num;
            cart.CreateTime = DateTime.Now;
            cart.CusId=CustomerService.GetEntities(c=>c.OpenId==openid).First().Id;
            if (ShoppingCartService.GetCount(s=>s.ProCode==code)<1)
            {
                if (ShoppingCartService.Add(cart))
                {
                    return Json(new {code = 200});
                }
                else
                {
                    return Json(new {code = 400});
                }
            }
            else
            {
                int Qty = ShoppingCartService.GetEntities(c => c.ProCode == code).First().Qty;
                cart.Qty = Qty + num;
                if (ShoppingCartService.Add(cart))
                {
                    return Json(new { code = 200 });
                }
                else
                {
                     return Json(new { code = 400 });
                }
               
            }
        }

        public ActionResult Delete()
        {
            string pcode = Request["cod"].ToString();
            int id = Convert.ToInt32(Session["Id"]);
            var cart = ShoppingCartService.GetEntity(p => p.ProCode == pcode && p.CusId == id);
            if (ShoppingCartService.Remove(cart))
            {
                return Json(new {code = 200});
            }
            else
            {
                return Json(new {code = 400});
            }
        }
    }
}