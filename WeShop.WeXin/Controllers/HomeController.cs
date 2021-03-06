﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeShop.IService;
using WeShop.WeXin.Models;

namespace WeShop.WeXin.Controllers
{
    public class HomeController : Controller
    {
        public IBannerService BannerService { get; set; }
        public INoticeService NoticeService { get; set; }
        public IProductService ProductService { get; set; }

        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.NoticeNum = NoticeService.GetCount(n => true);
            homeViewModel.Banners = BannerService.GetEntities(b => true);
            homeViewModel.Notices = NoticeService.GetEntitiesByPage(3, 1, false,n=>true, n => n.ModiTime);
            homeViewModel.Products = ProductService.GetEntitiesByPage(3, 1, false, p => p.Type == 1, p => p.ModiTime);
            Session["Id"] = "2";
            //homeViewModel.Notices = NoticeService.GetEntities(b => b.Id == gonggaoId);
            return View(homeViewModel);
        }

        public ActionResult GongGao()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.Notices = NoticeService.GetEntities(/*3, 1, false,*/ n => true/*, n => n.ModiTime*/);
            return View(homeViewModel);
        }

        public ActionResult GongGao_xq(int gonggaoId)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Notices = NoticeService.GetEntities(b => b.Id ==gonggaoId);
            return View(homeViewModel);
        }
    }
}