using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitJson;
using MyFlightManager.Models;

namespace MyFlightManager.Controllers
{
    public class FeedbackController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        // GET: feedback
        public ActionResult displayFeedback()
        {
            return View();
        }
        public ActionResult handleFeedback()
        {
            return View();
        }
        //展示未被处理的反馈
        public JsonResult showHandle(string feedbackID)
        {
            JsonData jsondata = new JsonData();
            FEEDBACK feedback = new FEEDBACK();
            feedback = db.FEEDBACK.Find(feedbackID);
            if (feedback == null)
                return Json(jsondata.ToJson());
            else
            {
                jsondata["feedbackID"] = feedbackID;
                jsondata["UserID"] = feedback.USER_ID;
                jsondata["feedbackTime"] = Convert.ToDateTime(feedback.FEEDBACK_TIME).ToString("f");
                jsondata["feedbackContent"] = feedback.FEEDBACK_CONTENT;
                return Json(jsondata.ToJson());
            }
        }
        [HttpPost]
        //管理员处理反馈
        public JsonResult handleResult(string feedbackID, string answer)
        {
            var admin_id = Session["AdminID"].ToString();
            DateTime now = DateTime.Now;
            var dealtID = System.Guid.NewGuid().ToString("N");
            var fb_answer = new FEEDBACK_ANSWER();
            fb_answer.ADMIN_ID = admin_id;
            fb_answer.DEALT_ID = dealtID;
            fb_answer.FEEDBACK_ID = feedbackID;
            fb_answer.ANSWER_TIME = now;
            fb_answer.ANSWER_CONTENT = answer;
            FEEDBACK feedback = new FEEDBACK();
            feedback = db.FEEDBACK.Find(feedbackID);

            JsonData response = new JsonData();
            try
            {
                if (fb_answer.ANSWER_CONTENT != "" && feedback != null)
                {
                    db.FEEDBACK_ANSWER.Add(fb_answer);
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

        //管理员界面显示反馈
        [HttpPost]
        public JsonResult showFeedback()
        {
            var admin_id = Session["AdminID"].ToString();
            List<FEEDBACK> answerList = (from u in db.FEEDBACK select u).ToList();
            JsonData jsondata = new JsonData();
            jsondata["data"] = new JsonData();
            List<FEEDBACK_ANSWER> feedbackList = (from u in db.FEEDBACK_ANSWER select u).ToList();
            foreach (FEEDBACK item in answerList)
            {

                if (feedbackList.Exists(dict => dict.FEEDBACK_ID.Equals(item.FEEDBACK_ID))) //该反馈出现在反馈处理表中，已经被处理
                    continue;
                JsonData temp = new JsonData();
                temp["feedback_ID"] = item.FEEDBACK_ID;
                temp["user_ID"] = item.USER_ID;
                temp["feedback_time"] = Convert.ToDateTime(item.FEEDBACK_TIME).ToString("f");
                temp["feedback_state"] = "未处理";
                jsondata["data"].Add(temp);
            }
            return Json(jsondata.ToJson());
        }

        //用户界面显示反馈结果(反馈处理的时间和内容）
        [HttpPost]
        public JsonResult showResult()
        {
            var userid = Session["UserID"].ToString();
            List<FEEDBACK_ANSWER> answerList = (from u in db.FEEDBACK_ANSWER select u).ToList();

            JsonData jsondata = new JsonData();
            jsondata["data"] = new JsonData();
            foreach (FEEDBACK_ANSWER item in answerList)
            {

                JsonData temp = new JsonData();
                //需要反馈的时间和内容
                temp["answerTime"] = Convert.ToDateTime(item.ANSWER_TIME).ToString("d");
                temp["content"] = item.ANSWER_CONTENT;

                jsondata["data"].Add(temp);
            }
            return Json(jsondata.ToJson());

        }
    }
}