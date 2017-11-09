using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Helpers;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Entities;
using WeShop.IService;
using WeShop.EFModel;
using WeShop.WeXin.Models;

namespace WeShop.WeXin.Controllers
{
    public class OAuthController : Controller
    {
        public ICustomerService CustomerService { get; set; }
        private string _appId = "wx35566a416480faf7";
        private string _appsecret = "c8493cdbbb6579fb3b8de048528a3b4c";
        private string _domain = "http://wx.guiq.top";
        // GET: OAuth
        public ActionResult Login(string requestUrl)
        {
            //生成一个回调Url，供微信授权完成后，返回给我们信息的接收地址
            var redirectUrl = $"{_domain}{Url.Action("CallBack", new { requestUrl })}";

            //生成一个验证码、
            var state = "wx" + DateTime.Now.Millisecond;
            Session["state"] = state;
            //生成微信授权页面
            var url = OAuthApi.GetAuthorizeUrl(_appId, redirectUrl, state, OAuthScope.snsapi_userinfo);

            return Redirect(url);
        }
        /// <summary>
        /// 这个是微信授权完成，所执行的回调方法
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public ActionResult CallBack(string code, string state, string requestUrl)
        {
            //判断验证码
            if (state != (string)Session["state"])
            {
                return Content("非法访问");
            }
            //判断code是不是有数据，这里有时可能为空，空的就失败了
            if (string.IsNullOrEmpty(code))
            {
                return Content("授权失败");
            }
            //这是通过code获取AccessToken令牌
            var OAuthAccessToken = OAuthApi.GetAccessToken(_appId, _appsecret, code);
            //判断有没有获取成功
            if (OAuthAccessToken.errcode != ReturnCode.请求成功)
            {
                return Content("授权失败");
            }
            //获取令牌成功，就说明我们已登陆
            Session["AccessToken"] = OAuthAccessToken;
            //尝试获取用户信息，如果能获取到，就说明我们这个令牌是有权限的，如果没有获取到，令牌就没有权限。
            //但是不管令牌是否有权限，OpenId都是一样的
            //OAuthUserInfo userInfo;
            try
            {
                var userInfo = OAuthApi.GetUserInfo(OAuthAccessToken.access_token, OAuthAccessToken.openid);
                //如果不为空，就说明获取成功，同时也说明了，我们的令牌是有权限的
                AddCustomer(userInfo);
                Session["userinfo"] = userInfo;
                //重定向到用户一开始请求的页面地址
                return Redirect(requestUrl);
            }
            catch (Exception)
            {
                //如果获取失败，就会报出异常，说明这个令牌没权限
                var redirectUrl = $"{_domain}{Url.Action("CallBack", new { requestUrl })}";
                //生成微信用户授权页面Url
                var url = OAuthApi.GetAuthorizeUrl(_appId, redirectUrl, state, OAuthScope.snsapi_userinfo);
                return Redirect(url);
            }

        }
        public ActionResult JsSdkConfig()
        {
            //生成需要注册的完整的URl（包含前缀和域名）
            var url = _domain + Request.RawUrl;
            //获取JS SDK的配置信息
            var config = JSSDKHelper.GetJsSdkUiPackage(_appId, _appsecret, url);
            return PartialView(config);
        }

        public void AddCustomer(OAuthUserInfo customer)
        {
            Customer cust = new Customer();
            cust.OpenId = customer.openid;
            cust.Photo = customer.headimgurl;
            cust.Name = customer.nickname;
            cust.Address = customer.city;
            cust.CreateTime = DateTime.Now;
            cust.Role = "普通用户";
            if (CustomerService.GetCount(n => n.OpenId == customer.openid) < 1)
            {
                CustomerService.Add(cust);
            }
            else if (CustomerService.GetCount(n => n.Photo == customer.headimgurl) < 1)
            {
                cust.Id = CustomerService.GetEntities(n => n.OpenId == customer.openid).First().Id;
                CustomerService.Add(cust);
            }
            Session["openid"] = customer.openid;
        }
    }
}