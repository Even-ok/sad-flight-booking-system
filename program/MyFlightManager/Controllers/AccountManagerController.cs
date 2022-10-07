using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using LitJson;
using System.Text.RegularExpressions;
using MyFlightManager.Models;

namespace MyFlightManager.Controllers
{
    public class AccountManagerController : Controller
    {
        // GET: AccountManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]  //post方法
        public ActionResult UserLogin(string name, string password, bool state)
        {
            var data = fun_user_login(name, password, state);

            return data;
        }


        /*注册*/
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegister(string UserID, string UserName, string password, long PhoneNumber, string userEmail, string userIdcard)
        {
            var data = fun_register(UserID, UserName, password, PhoneNumber, userEmail, userIdcard);

            return data;
        }
        private Entities db = new Entities();
        JsonData jsondata = new JsonData();

        //用户登录
        public JsonResult fun_user_login(string userID, string password, bool state)
        {
            USER_ACCOUNT USER = new USER_ACCOUNT();
            int flag = 0;
            var userid =
                        (from c in db.USER_ACCOUNT
                         where c.USER_ID == userID
                         select c.USER_ID).Distinct();
            if (userid.FirstOrDefault() != null)    //取序列中满足条件的第一个元素，如果没有元素满足条件，则返回默认值null
            {
                var id = userid.FirstOrDefault();  //第一个元素
                USER = db.USER_ACCOUNT.Find(id);
                if (USER.USER_PASSWORD != password)
                {
                    flag = 2;//密码错误
                }
                else
                {
                    //前端还是接收  LoginFlag好了
                    jsondata["userID"] = id;
                    flag = 0;//成功
                    Session["UserID"] = id; //保存用户信息
                                            //判断是否记住密码
                    if (state)
                    {
                        HttpCookie hc = new HttpCookie("Example");

                        //在cookie对象中保存用户名和密码
                        hc["UserID"] = userID;
                        hc["UserPwd"] = password;
                        //设置过期时间
                        hc.Expires = DateTime.Now.AddDays(2);
                        //保存到客户端
                        Response.Cookies.Add(hc);
                    }
                    else
                    {
                        HttpCookie hc = new HttpCookie("Example");
                        //判断hc是否空值
                        if (hc != null)
                        {
                            //设置过期时间
                            hc.Expires = DateTime.Now.AddDays(-1);
                            //保存到客户端
                            Response.Cookies.Add(hc);
                        }

                    }
                }
            }
            else
            {
                flag = 1;//用户名不存在
            }
            jsondata["LoginFlag"] = flag.ToString();
            return Json(Regex.Unescape(jsondata.ToJson()));
            //返回之后还要解析
        }
        //ActionResult是控制器方法执行后返回的结果类型，控制器方法可以返回一个直接或间接从
        //ActionResult抽象类继承的类型，如果返回的是非ActionResult类型，控制器将会将结果转
        //换为一个ContentResult类型。默认的ControllerActionInvoker调用

        public JsonResult fun_register(string UserID, string UserName, string password, long PhoneNumber, string userEmail, string userIdcard)
        {
            USER_ACCOUNT uSERS = new USER_ACCOUNT();
            int flag = 0;
            var userid =
                    (from c in db.USER_ACCOUNT
                     where c.USER_ID == UserID
                     select c.USER_ID).Distinct();
            if (userid.FirstOrDefault() != null)
            {
                flag = 0;  //用户已存在
            }
            else
            {
                uSERS.USER_ID = UserID;
                uSERS.USER_NAME = UserName;
                uSERS.USER_PASSWORD = password;
                uSERS.USER_EMAIL = userEmail;
                uSERS.USER_IDCARD = userIdcard;
                uSERS.PHONE_NUMBER = PhoneNumber;
                db.USER_ACCOUNT.Add(uSERS);
                db.SaveChanges();
                flag = 1; //注册成功
            }
            //JsonData jsondata = new JsonData();
            //jsondata["LoginFlag"] = flag;
            //jsondata.ToJson();
            Dictionary<string, string> jsondata = new Dictionary<string, string>();
            jsondata.Add("RegisterFlag", flag.ToString());
            jsondata.Add("UserID", UserID);
            return Json(JsonConvert.SerializeObject(jsondata, Formatting.Indented));
        }
        //退出当前账号
        [HttpPost]
        public JsonResult UserLogOff()
        {
            Session["UserID"] = null;
            JsonData jsondata = new JsonData();
            jsondata["state"] = 1;
            return Json(jsondata.ToJson());
        }

        public JsonResult CheckCookie()
        {
            //获取cookie中的数据
            HttpCookie cookie = Request.Cookies.Get("Example");
            JsonData jsondata = new JsonData();
            //判断cookie是否空值
            if (cookie != null)
            {
                //把保存的用户名和密码赋值给对应的文本框
                //用户名
                var name = cookie.Values["UserID"].ToString();
                jsondata["UserID"] = name;
                //密码
                var pwd = cookie.Values["UserPwd"].ToString();
                jsondata["password"] = pwd;
            }
            else
            {
                jsondata["UserID"] = null;
                jsondata["password"] = null;
            }
            return Json(jsondata.ToJson());
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult AdminLogin1(string name, string password)
        {
            var data = fun_admin_login(name, password);

            return data;
        }
        public JsonResult fun_admin_login(string userID, string password)
        {
            JsonData jsondata = new JsonData();
            ADMIN_ACCOUNT USER = new ADMIN_ACCOUNT();
            int flag = 0;
            var userid =
                        (from c in db.ADMIN_ACCOUNT
                         where c.ADMIN_ID == userID
                         select c.ADMIN_ID).Distinct();
            if (userid.FirstOrDefault() != null)    //取序列中满足条件的第一个元素，如果没有元素满足条件，则返回默认值null
            {
                var id = userid.FirstOrDefault();  //第一个元素
                USER = db.ADMIN_ACCOUNT.Find(id);
                if (USER.ADMIN_PASSWORD != password)
                {
                    flag = 2;//密码错误
                }
                else
                {
                    if (USER.ADMIN_STATE == true)
                        flag = 3;
                    else
                    {
                        flag = 0;//成功
                        USER.ADMIN_STATE = true;
                    }
                    Session["AdminID"] = id; //保存管理员信息
                }
            }
            else
            {
                flag = 1;//用户名不存在
            }
            jsondata["LoginFlag"] = flag.ToString();
            return Json(Regex.Unescape(jsondata.ToJson()));
            //返回之后还要解析
        }
        //ActionResult是控制器方法执行后返回的结果类型，控制器方法可以返回一个直接或间接从
        //ActionResult抽象类继承的类型，如果返回的是非ActionResult类型，控制器将会将结果转
        //换为一个ContentResult类型。默认的ControllerActionInvoker调用
        public ActionResult AdminLogOff()
        {
            string adminID = Session["AdminID"].ToString();
            ADMIN_ACCOUNT am = (from c in db.ADMIN_ACCOUNT
                                where c.ADMIN_ID == adminID
                                select c).FirstOrDefault();
            am.ADMIN_STATE = false;
            Session["AdminID"] = null;
            JsonData jsondata = new JsonData();
            jsondata["state"] = 1;
            return Json(jsondata.ToJson());
        }

    }
}