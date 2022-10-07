using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using LitJson;
using MyFlightManager.Models;

namespace MyFlightManager.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        private Entities db = new Entities();
        // GET: FlightHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HeadBar()
        {
            return View();
        }
        public ActionResult showJatlag()
        {
            return View();
        }
        public ActionResult displayFlight()
        {
            return View();
        }

        /*
         * 获取当前出发、到达机场的covid等级
         * 输入：出发、到达机场名
         * 输出：Json，内容包括：出发城市cov、到达城市cov
         */
        public JsonResult getCovidLevel(string departAir, string arriveAir)
        {
            JsonData temp = new JsonData();
            string depart_ID = (from t in db.AIRPORT where t.AIRPORT_NAME == departAir select t.AIRPORT_NAME).ToList().FirstOrDefault();
            string arrive_ID = (from t in db.AIRPORT where t.AIRPORT_NAME == arriveAir select t.AIRPORT_NAME).ToList().FirstOrDefault();
            string depart_level = (from q in db.CITY where q.CITY_ID == depart_ID select q.COV19_RISK).ToList().FirstOrDefault();
            string arrive_level = (from q in db.CITY where q.CITY_ID == arrive_ID select q.COV19_RISK).ToList().FirstOrDefault();
            temp["depart_cov"] = depart_level;
            temp["arrive_cov"] = arrive_level;
            return Json(temp.ToJson());
        }

        /*不带参数的获取所有航班列表
         * 输出：航班号、出发时间、到达时间、出发机场、
         * 到达机场、航班公司名称、头等舱价格、经济舱价格、航班状态
        */
        public JsonResult getSomeFlight()
        {
            JsonData jsondata = new JsonData();
            jsondata["data"] = new JsonData();
            List<Models.FLIGHT> list1 = (from p in db.FLIGHT select p).ToList();
            int count = 0;
            foreach (FLIGHT item in list1)
            {
                if (count > 5)
                    break;//最多显示6条
                JsonData temp = new JsonData();
                temp["flightNo"] = item.FLIGHT_NUMBER;
                temp["departTime"] = Convert.ToDateTime(item.DEPART_DATE).ToString("f");
                temp["arrivalTime"] = Convert.ToDateTime(item.ARRIVE_DATE).ToString("f");

                int airline_ID = Convert.ToInt32(item.AIRLINE_ID);
                string depart_airport = (from u in db.AIRPORT join p1 in db.DEPARTS on u.AIRPORT_ID equals p1.AIRPORT_ID where p1.AIRLINE_ID == airline_ID select u.AIRPORT_NAME).ToList().FirstOrDefault();
                string arrive_airport = (from u in db.AIRPORT join p1 in db.ARRIVES on u.AIRPORT_ID equals p1.AIRPORT_ID where p1.AIRLINE_ID == airline_ID select u.AIRPORT_NAME).ToList().FirstOrDefault();
                temp["departportName"] = depart_airport;
                temp["arrivalportName"] = arrive_airport;

                temp["companyName"] = item.COMPANY_NAME;
                temp["firstClassPrice"] = item.FIRST_CLASS_PRICE;
                temp["economyClassPrice"] = item.ECONOMY_CLASS_PRICE;
                jsondata["data"].Add(temp);
                count++;
            }
            return Json(jsondata.ToJson());
        }

        /*
         * 带参数的航班查询，获取航班具体信息
         */
        /*       public JsonResult getFlightDetail(string flight_no, DateTime depart_time)
               {
                   JsonData jsondata = new JsonData();
                   JsonData temp = new JsonData();
                   List<Models.FLIGHT> list1 = (from p in db.FLIGHT select p).ToList();
                   foreach (FLIGHT item in list1)
                   {
                       if (item.FLIGHT_NUMBER != flight_no || item.DEPART_DATE != depart_time)
                           continue;
                       else
                       {
                           temp["flight_no"] = item.FLIGHT_NUMBER;
                           temp["depart_time"] = Convert.ToDateTime(item.DEPART_DATE).ToString("yyyy-MM-dd"); 
                           int airline_ID = Convert.ToInt32(item.AIRLINE_ID);
                           string depart_airport = (from u in db.AIRPORT join p1 in db.DEPARTS on u.AIRPORT_ID equals p1.AIRPORT_ID where p1.AIRLINE_ID == airline_ID select u.AIRPORT_NAME).ToList().First();
                           string arrive_airport = (from u in db.AIRPORT join p1 in db.ARRIVES on u.AIRPORT_ID equals p1.AIRPORT_ID where p1.AIRLINE_ID == airline_ID select u.AIRPORT_NAME).ToList().First();
                           temp["depart_airport"] = depart_airport;
                           temp["arrive_airport"] = arrive_airport;

                           temp["company_name"] = item.COMPANY_NAME;
                           temp["first_class_price"] = item.FIRST_CLASS_PRICE;
                           temp["economy_class_price"] = item.ECONOMY_CLASS_PRICE;
                           //temp["state"] = item.FLIGHT_STATE;
                           jsondata["data"].Add(temp);
                       }
                   }
                   return Json(jsondata.ToJson());
               }*/

        /*
        * 带参数的航班查询，获取航班具体信息
        */
        [HttpPost]  //post方法
        public JsonResult getFlight(string depart_code, string arrive_code, string dp_time)
        {
            JsonData jsondata = new JsonData();
            //返回出发机场ID
            List<string> depart_name = (from p in db.AIRPORT where p.AIRPORT_ID == depart_code select p.AIRPORT_NAME).ToList();
            //返回到达机场ID
            List<string> arrive_name = (from p in db.AIRPORT where p.AIRPORT_ID == arrive_code select p.AIRPORT_NAME).ToList();
            string target_name1 = depart_name.FirstOrDefault();
            string target_name2 = arrive_name.FirstOrDefault();
            //返回出发航线ID
            List<int> depart_list = (from p in db.DEPARTS where p.AIRPORT_ID == depart_code select p.AIRLINE_ID).ToList();
            //返回到达航线ID
            List<int> arrive_list = (from p in db.ARRIVES where p.AIRPORT_ID == arrive_code select p.AIRLINE_ID).ToList();
            //获得正确航线
            List<int> airline_ID = depart_list.Intersect(arrive_list).ToList();
            int target_airline = airline_ID.FirstOrDefault();
            //获取航班列表，日期要相同
            List<Models.FLIGHT> flight_list = (from p in db.FLIGHT where p.AIRLINE_ID == target_airline select p).ToList();
            if (flight_list.Count == 0)
                jsondata["error_code"] = 1;
            else
                jsondata["error_code"] = 0;
            jsondata["data"] = new JsonData();
            /*
            获取航班号、出发机场名称、到达机场名称、起飞时间、到达时、经济舱价格、头等舱价格、航空公司
            */
            foreach (FLIGHT item in flight_list)
            {
                if (Convert.ToDateTime(item.DEPART_DATE).ToString("yyyy-MM-dd").Equals(dp_time))
                {
                    JsonData temp = new JsonData();
                    temp["flightNo"] = item.FLIGHT_NUMBER;
                    temp["departportName"] = target_name1;
                    temp["arrivalportName"] = target_name2;
                    temp["departTime"] = Convert.ToDateTime(item.DEPART_DATE).ToString("f");
                    temp["arrivalTime"] = Convert.ToDateTime(item.ARRIVE_DATE).ToString("f");
                    temp["economyClassPrice"] = item.ECONOMY_CLASS_PRICE;
                    temp["firstClassPrice"] = item.FIRST_CLASS_PRICE;
                    temp["companyName"] = item.COMPANY_NAME;
                    jsondata["data"].Add(temp);
                }
            }
            ViewData["myFlight"] = jsondata["data"];

            return Json(Regex.Unescape(jsondata.ToJson()));
        }

        //获取所有时差
        public JsonResult getAllTimeDifference()
        {
            JsonData jsondata = new JsonData();
            jsondata["data"] = new JsonData();
            List<Models.TIME_DIFFERENCE> list1 = (from p in db.TIME_DIFFERENCE select p).ToList();
            foreach (TIME_DIFFERENCE item in list1)
            {

                JsonData temp = new JsonData();
                temp["country"] = item.COUNTRY;
                temp["cityName"] = item.CITY_NAME;
                temp["timeDifference"] = item.TIME_DIFFERENCE1;
                jsondata["data"].Add(temp);
            }
            return Json(jsondata.ToJson());
        }

        //获取单个国家时差
        public JsonResult getSomeTimeDifference(string name)
        {
            JsonData jsondata = new JsonData();
            jsondata["data"] = new JsonData();
            List<Models.TIME_DIFFERENCE> list1 = (from p in db.TIME_DIFFERENCE select p).ToList();
            foreach (TIME_DIFFERENCE item in list1)
            {

                JsonData temp = new JsonData();
                if (item.CITY_NAME == name || item.COUNTRY == name)
                {
                    temp["country"] = item.COUNTRY;
                    temp["cityName"] = item.CITY_NAME;
                    temp["timeDifference"] = item.TIME_DIFFERENCE1;
                    jsondata["data"].Add(temp);
                }
            }
            return Json(jsondata.ToJson());
        }
    }
}