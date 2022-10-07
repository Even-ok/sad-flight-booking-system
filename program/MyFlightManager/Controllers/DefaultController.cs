using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFlightManager.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //拦截未登录用户访问
            var userName = Session["UserID"] as String;
            if (String.IsNullOrEmpty(userName))
            {
                Response.Write("<script>alert('请先登录！')</script>");
                //重定向至登录页面
                filterContext.Result = Redirect("../mylogin.html");
                return;
            }

        }
    }
}