using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using LitJson;
using Newtonsoft.Json;
using MyFlightManager.Models;

namespace MyFlightManager.Controllers
{
    public class UserInfoController : DefaultController
    {
        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult perpagestru()
        {
            return View();
        }
        public ActionResult changeInfo()
        {
            return View();
        }
        public ActionResult changepsw()
        {
            return View();
        }

        public ActionResult feedback()
        {
            return View();
        }

        private Entities db = new Entities();


        public ActionResult addFeedBack( string fd, string ho)
        {
            string user_id = Session["UserID"].ToString();
            DateTime now = DateTime.Now;
            var feedback_id = System.Guid.NewGuid().ToString("N");
            var feedback = new FEEDBACK();
            feedback.USER_ID = user_id;
            feedback.FEEDBACK_ID = feedback_id;
            feedback.FEEDBACK_TIME = now;
            feedback.FEEDBACK_CONTENT = fd + ho;
            JsonData response = new JsonData();
            try
            {
                if (feedback.FEEDBACK_CONTENT != "")
                {
                    db.FEEDBACK.Add(feedback);
                    db.SaveChanges();
                }
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }



        [HttpPost]
        public ActionResult userinfo()
        {
            JsonData response = new JsonData();
            USER_ACCOUNT user = db.USER_ACCOUNT.Find(Session["UserID"]);
            response["userinfo"] = new JsonData();
            JsonData userinfo = new JsonData();
            if (user != null)
            {
                userinfo["userID"] = user.USER_ID;
                userinfo["userName"] = user.USER_NAME;
                userinfo["userPhoneNumber"] = user.PHONE_NUMBER.ToString();
                userinfo["userEmail"] = user.USER_EMAIL;
                userinfo["userIDcard"] = user.USER_IDCARD;
                response["error_node"] = 0;
                response["userinfo"] = userinfo;
            }
            return Json(response.ToJson());
        }

        [HttpPost]
        public ActionResult userinfoModify(string userName, string userPhoneNumber, string userEmail, string userIDcard)
        {
            JsonData response = new JsonData();
            Decimal userPhoneNumber_Decimal;
            bool userPhoneNumber_Decimal_check = Decimal.TryParse(userPhoneNumber, out userPhoneNumber_Decimal);
            if (!userPhoneNumber_Decimal_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            try
            {
                USER_ACCOUNT user = db.USER_ACCOUNT.Find(Session["UserID"]);
                user.USER_NAME = userName;
                user.PHONE_NUMBER = userPhoneNumber_Decimal;
                user.USER_EMAIL = userEmail;
                user.USER_IDCARD = userIDcard;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }

        [HttpPost]
        public ActionResult passwordModify(string oldpassword, string newpassword)
        {
            JsonData response = new JsonData();
            USER_ACCOUNT user = db.USER_ACCOUNT.Find(Session["UserID"]);
            if (user.USER_PASSWORD != oldpassword)
            {
                response["error_code"] = 2;
                return Json(response.ToJson());
            }
            try
            {
                user.USER_PASSWORD = newpassword;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }
       
    }
}