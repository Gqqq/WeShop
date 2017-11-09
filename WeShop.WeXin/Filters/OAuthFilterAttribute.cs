using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeShop.WeXin.Filters
{
    public class OAuthFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断是否登陆授权，AccessToken标识代表着用户已登陆，这里面存放的是用户的令牌
            if (filterContext.HttpContext.Session["AccessToken"] != null) return;

            //开始授权阶段

            //requestUrl代表用户访问的请求地址，需要保存，以便于授权完成后，跳到这个地址来。
            var requestUrl = filterContext.HttpContext.Request.RawUrl;
            //未登录，所以要跳到登录页
            var UrlHelper = new UrlHelper(filterContext.RequestContext);
            //
            filterContext.Result = new RedirectResult(UrlHelper.Action("Login", "OAuth", new { requestUrl }));
        }
    }
}