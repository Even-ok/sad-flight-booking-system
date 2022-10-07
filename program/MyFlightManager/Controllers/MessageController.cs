using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitJson;
using MyFlightManager.Models;

namespace MyFlightManager.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult message()
        {
            return View();
        }
        public ActionResult feedbackMessage()
        {
            return View();
        }

        private Entities db = new Entities();
        public ActionResult Feedback()
        {
            string userid = Session["UserID"].ToString();
            JsonData response = new JsonData();
            var answer = from a in db.FEEDBACK_ANSWER
                         from f in db.FEEDBACK
                         where f.USER_ID == userid && a.FEEDBACK_ID == f.FEEDBACK_ID
                         select a;
            if (answer != null)
            {
                response["info"] = new JsonData();
                response["info"].SetJsonType(JsonType.Array);
                foreach (var i in answer)
                {
                    var info = new
                    {
                        time = i.ANSWER_TIME.ToString(),
                        content = i.ANSWER_CONTENT
                    };
                    response["info"].Add(JsonMapper.ToObject(JsonMapper.ToJson(info)));
                }
                response["error_code"] = 0;
            }
            else
                response["error_code"] = 1;
            return Json(response.ToJson());
        }

        public ActionResult showMessage()
        {
            string userid = Session["UserID"].ToString();
            JsonData response = new JsonData();
            var message = from a in db.MESSAGE
                         where a.USER_ID == userid 
                         select a;
            if (message != null)
            {
                response["info"] = new JsonData();
                response["info"].SetJsonType(JsonType.Array);
                foreach (var i in message)

                {
                    var info = new
                    {
                        time = i.MESSAGE_TIME.ToString(),
                        content = i.MESSAGE_CONTENT
                    };
                    response["info"].Add(JsonMapper.ToObject(JsonMapper.ToJson(info)));
                }
                response["error_code"] = 0;
            }
            else
                response["error_code"] = 1;
            return Json(response.ToJson());
        }
    }
}