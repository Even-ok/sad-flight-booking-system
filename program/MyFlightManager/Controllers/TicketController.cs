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
    public class TicketController : DefaultController
    {
        // GET: Ticket
        private Entities db = new Entities();

        // GET: Ticket
        public ActionResult buyTicket()
        {
            //本页面实现购买机票的功能
            return View();
        }
        public ActionResult changeTicket()
        {
            return View();
        }
        /*
         * 显示当前用户所有机票（订单）
         */
        [HttpPost]
        public JsonResult showOrder(string userID)
        {
            JsonData jsonData = new JsonData();
            jsonData["error_code"] = 0;
            jsonData["data"] = new JsonData();
            IQueryable<FLIGHT> result = db.FLIGHT;//Ctx表示EF上下文


            //从机票购买表中获取机票ID
            List<string> purchase_list = (from p in db.PURCHASES where p.USER_ID == userID select p.TICKET_ID).ToList();
            //从机票改签表中获取机票ID
            List<string> change_list = (from p in db.CHANGES where p.USER_ID == userID select p.TICKET_ID).ToList();
            //从机票取消表中获取机票ID
            List<string> cancel_list = (from p in db.CANCELS where p.USER_ID == userID select p.TICKET_ID).ToList();

            List<string> ticket_ID_list = purchase_list.Union(change_list).Union(cancel_list).ToList();
            //从机票表获取机票信息
            List<Models.PLANE_TICKET> all_flight = new List<Models.PLANE_TICKET>();
            foreach (string tk_item in ticket_ID_list)
            {
                List<Models.PLANE_TICKET> item = (from p in db.PLANE_TICKET where p.TICKET_ID == tk_item select p).ToList();
                all_flight.Union(item);
            }//获得一个数组，这个数组里有航班号、出发日期、航线、乘客名、机票状态
            List<FLIGHT> fl_list = new List<FLIGHT>();
            foreach (PLANE_TICKET p in all_flight)
            {
                JsonData temp = new JsonData();
                temp["flightNumber"] = p.FLIGHT_NUMBER;
                temp["flightDate"] = Convert.ToDateTime(p.FLIGHT_DATE).ToString("yyyy-MM-dd"); //返回2000/9/16
                result = result.Where(t => t.FLIGHT_NUMBER == p.FLIGHT_NUMBER && t.DEPART_DATE == p.FLIGHT_DATE);
                FLIGHT fl_item = result.FirstOrDefault();
                AIRLINE ar_item = (from t in db.AIRLINE where t.AIRLINE_ID == fl_item.AIRLINE_ID select t).ToList().FirstOrDefault();
                temp["Price"] = p.PRICE;
                temp["flightClass"] = p.FLIGHT_CLASS;
                temp["passengerName"] = p.PASSENGER_NAME;
                temp["ticketState"] = p.TICKET_STATE;
                temp["ticketID"] = p.TICKET_ID;

                temp["companyName"] = fl_item.COMPANY_NAME;
                temp["departAirportName"] = (from u in db.AIRPORT join p1 in db.DEPARTS on u.AIRPORT_ID equals p1.AIRPORT_ID where p1.AIRLINE_ID == fl_item.AIRLINE_ID select u.AIRPORT_NAME).ToList().FirstOrDefault();
                temp["arriveAirportName"] = (from u in db.AIRPORT join p1 in db.ARRIVES on u.AIRPORT_ID equals p1.AIRPORT_ID where p1.AIRLINE_ID == fl_item.AIRLINE_ID select u.AIRPORT_NAME).ToList().FirstOrDefault();
                jsonData["data"].Add(temp);

            }

            ViewData["all_ticket"] = all_flight;

            return Json(jsonData.ToJson());
        }

        /*
         * 购买机票，加入机票记录
         */
        [HttpPost]
        public ActionResult addTicket(string flightNumber, string departDate, int price, string flight_class)

        {
            string userid = Session["UserID"].ToString();
            USER_ACCOUNT user = db.USER_ACCOUNT.Find(userid);
            //机票号自动生成，添加购买记录
            var UserID = Session["UserID"].ToString();
            string ticketID = System.Guid.NewGuid().ToString("N");
            PLANE_TICKET tk_item = new PLANE_TICKET();
            //生成值机消息
            MESSAGE msg = new MESSAGE();
            PURCHASES pc_item = new PURCHASES();
            tk_item.TICKET_ID = ticketID;
            tk_item.FLIGHT_NUMBER = flightNumber;
            tk_item.FLIGHT_DATE = Convert.ToDateTime(departDate);
            tk_item.PRICE = price;
            tk_item.FLIGHT_CLASS = flight_class;
            tk_item.PASSENGER_NAME = user.USER_NAME;
            tk_item.TICKET_STATE = "common";
            pc_item.USER_ID = UserID;
            pc_item.PURCHASE_TIME = DateTime.Now;
            pc_item.TICKET_ID = ticketID;
            msg.USER_ID = UserID;
            msg.MESSAGE_ID = System.Guid.NewGuid().ToString("N");
            //提前2天提示选座
            msg.MESSAGE_TIME = Convert.ToDateTime(departDate).AddDays(-2);
            msg.MESSAGE_CONTENT = "请检查是否已为您的航班" + flightNumber + "值机，谢谢！";
            db.PLANE_TICKET.Add(tk_item);
            db.PURCHASES.Add(pc_item);
            db.MESSAGE.Add(msg);
            db.SaveChanges();
            JsonData jsondata = new JsonData();
            jsondata["status"] = 1;
            return Json(jsondata.ToJson());

        }

        //删除购买表中的这个ticket
        public ActionResult cancelTicket(string ticket_ID)
        {
            JsonData jsondata = new JsonData();
            string userID = Session["UserID"].ToString();
            PLANE_TICKET tk_item = new PLANE_TICKET();
            PURCHASES pur = new PURCHASES();
            CHANGES change = new CHANGES();
            tk_item = db.PLANE_TICKET.Find(ticket_ID);
            if(tk_item.TICKET_STATE=="canceled")//订单已经被取消了
            {

                jsondata["status"] = 0;
                return Json(jsondata.ToJson());
            }
            pur = db.PURCHASES.Find(ticket_ID);
            change = db.CHANGES.Find(ticket_ID);
            CANCELS cancel = new CANCELS();
            cancel.USER_ID = userID;
            cancel.TICKET_ID = ticket_ID;
            cancel.CANCELS_TIME = DateTime.Now;
            cancel.CANCELS_SURCHARGE = 100;  //假设扣除手续费100元
            db.CANCELS.Add(cancel);//取消表中加入实例
            tk_item.TICKET_STATE = "canceled"; //机票状态更改
            if (pur != null)
                db.PURCHASES.Remove(pur);//购买表中删除实例
            if (change != null)
                db.CHANGES.Remove(change);//若在改签表中，改签表删除实例
            db.SaveChanges();
            jsondata["status"] = 1;
            return Json(jsondata.ToJson());
        }

        //改签机票
        public ActionResult changeTicket1(string ticket_ID, string flightNo, string departDate, string flightClass)
        {
            string userID = Session["UserID"].ToString();
            PLANE_TICKET tk_item = new PLANE_TICKET();
            PURCHASES pur = new PURCHASES();
            //要更改的航班
            List<FLIGHT> changeFlightList = (from u in db.FLIGHT
                                             where u.FLIGHT_NUMBER == flightNo
                                             select u).ToList();
            FLIGHT changeFlight = new FLIGHT();
            foreach (FLIGHT item in changeFlightList)
            {
                if (Convert.ToDateTime(item.DEPART_DATE).ToString("f") == departDate)
                    changeFlight = item;
            }
            //查找ticket实例（原来存在的）
            tk_item = db.PLANE_TICKET.Find(ticket_ID);
            pur = db.PURCHASES.Find(ticket_ID);
            CHANGES change = new CHANGES();
            change.USER_ID = userID;
            change.TICKET_ID = ticket_ID;
            change.CHANGES_TIME = DateTime.Now;
            //计算改签价钱
            if (flightClass == "经济舱")
            {
                if (tk_item.PRICE <= changeFlight.ECONOMY_CLASS_PRICE)
                    change.CHANGES_SURCHARGE = 0;
                else
                    change.CHANGES_SURCHARGE = tk_item.PRICE - changeFlight.ECONOMY_CLASS_PRICE;
            }
            else
            {
                if (tk_item.PRICE <= changeFlight.FIRST_CLASS_PRICE)
                    change.CHANGES_SURCHARGE = 0;
                else
                    change.CHANGES_SURCHARGE = tk_item.PRICE - changeFlight.ECONOMY_CLASS_PRICE; //根据票价差计算改签费
            }
            tk_item.FLIGHT_NUMBER = flightNo;
            tk_item.FLIGHT_DATE = Convert.ToDateTime(departDate);
            db.CHANGES.Add(change);//改签表中加入实例
            tk_item.TICKET_STATE = "changed"; //机票状态更改
            if (pur != null)
                db.PURCHASES.Remove(pur);//购买表中删除实例
            db.SaveChanges();
            JsonData jsondata = new JsonData();
            jsondata["state"] = 1;
            jsondata["charge"] = change.CHANGES_SURCHARGE;
            return Json(jsondata.ToJson());
        }

        //获取购买机票时的身份认证信息
        public JsonResult getIdentification()
        {
            JsonData jsonData = new JsonData();
            jsonData["data"] = new JsonData();

            if (Session["UserID"] == null)
                return Json(jsonData.ToJson());
            else
            {
                JsonData temp = new JsonData();
                string UserID = Session["UserID"].ToString();
                //获取用户
                USER_ACCOUNT myUser = (from u in db.USER_ACCOUNT where u.USER_ID == UserID select u).ToList().FirstOrDefault();
                temp["userPhoneNumber"] = Convert.ToString(myUser.PHONE_NUMBER);
                temp["userName"] = myUser.USER_NAME;
                temp["userIDcard"] = myUser.USER_IDCARD;
                jsonData["data"].Add(temp);

                return Json(Regex.Unescape(jsonData.ToJson()));

            }

        }

        //获取出发、到达地
        public JsonResult getLocation(string departAirport, string arriveAirport)
        {
            JsonData jsonData = new JsonData();
            jsonData["data"] = new JsonData();
            List<AIRPORT> dp = (from u in db.AIRPORT where u.AIRPORT_NAME == departAirport select u).ToList();
            List<AIRPORT> ar = (from u in db.AIRPORT where u.AIRPORT_NAME == arriveAirport select u).ToList();
            if (dp.Count != 0 && ar.Count != 0)
            {
                JsonData temp = new JsonData();
                AIRPORT dpcity_1 = dp.FirstOrDefault();
                AIRPORT arcity_1 = ar.FirstOrDefault();
                CITY dpcity = (from u in db.CITY where u.CITY_ID == dpcity_1.CITY_ID select u).ToList().FirstOrDefault();
                CITY arcity = (from u in db.CITY where u.CITY_ID == arcity_1.CITY_ID select u).ToList().FirstOrDefault();
                temp["departLocation"] = dpcity.CITY_NAME;
                temp["arriveLocation"] = arcity.CITY_NAME;
                temp["departLevel"] = dpcity.COV19_RISK;
                temp["arriveLevel"] = arcity.COV19_RISK;
                jsonData["data"].Add(temp);
            }
            return Json(Regex.Unescape(jsonData.ToJson()));
        }

        public ActionResult books()
        {
            return View();
        }

        public ActionResult schedule()
        {
            return View();
        }

        //获取用户个人行程
        public JsonResult scheduleQuery( DateTime startDate, DateTime endDate)
        {
            string user_ID = Session["UserID"].ToString();
            endDate = endDate.AddDays(1);
            JsonData response = new JsonData();
            response["data"] = new JsonData();
            // 获取user_ID 所乘航班的表
            var flights = from p in db.PURCHASES
                          from t in db.PLANE_TICKET
                          from f in db.FLIGHT
                          where p.USER_ID == user_ID && p.TICKET_ID == t.TICKET_ID &&
                          t.FLIGHT_NUMBER == f.FLIGHT_NUMBER
                          && f.DEPART_DATE >= startDate
                          && f.ARRIVE_DATE <= endDate
                          select new
                          {
                              f.AIRLINE_ID,
                              f.FLIGHT_NUMBER,
                              t.TICKET_ID,
                              t.TICKET_STATE,
                          };

            int tt = flights.Count();
            foreach (var f in flights)
            {
                JsonData temp = new JsonData();
                var arriveCity = from a in db.ARRIVES
                                 from p in db.AIRPORT
                                 from c in db.CITY
                                 where a.AIRLINE_ID == f.AIRLINE_ID && a.AIRPORT_ID == p.AIRPORT_ID
                                 && p.CITY_ID == c.CITY_ID
                                 select c.CITY_NAME;
                var departCity = from a in db.DEPARTS
                                 from p in db.AIRPORT
                                 from c in db.CITY
                                 where a.AIRLINE_ID == f.AIRLINE_ID && a.AIRPORT_ID == p.AIRPORT_ID
                                 && p.CITY_ID == c.CITY_ID
                                 select c.CITY_NAME;

                var arriveDate = from ff in db.FLIGHT
                                 where ff.AIRLINE_ID == f.AIRLINE_ID && f.FLIGHT_NUMBER == ff.FLIGHT_NUMBER
                                 select ff.ARRIVE_DATE;
                var departDate = from ff in db.FLIGHT
                                 where ff.AIRLINE_ID == f.AIRLINE_ID && f.FLIGHT_NUMBER == ff.FLIGHT_NUMBER
                                 select ff.DEPART_DATE;
                string state = f.TICKET_STATE;
                if (state == "common" && departDate.ToList().FirstOrDefault() > DateTime.Now)
                    temp["state"] = "未出行";
                else if (state == "common")
                    temp["state"] = "已出行";
                else if (state == "canceled")
                    //temp["state"] = "退订";
                    continue;
                else
                    //temp["state"] = "改签";
                    continue;

                temp["arriveCity"] = arriveCity.ToList().FirstOrDefault().ToString();
                temp["departCity"] = departCity.ToList().FirstOrDefault().ToString();
                temp["arriveDate"] = arriveDate.ToList().FirstOrDefault().ToString();
                temp["departDate"] = departDate.ToList().FirstOrDefault().ToString();
                response["data"].Add(temp);
            }

            response["error_code"] = 0;
            return Json(response.ToJson());
        }

        //获取用户个人机票详情
        [HttpPost]
        public ActionResult booksQuery(DateTime startDate, DateTime endDate)
        {
            string user_ID = Session["UserID"].ToString();
            endDate = endDate.AddDays(1);
            JsonData response = new JsonData();
            response["data"] = new JsonData();
            // 获取user_ID 所乘航班的表
            var pur_flight = from p in db.PURCHASES
                             from t in db.PLANE_TICKET
                             from f in db.FLIGHT
                             where p.USER_ID == user_ID && p.TICKET_ID == t.TICKET_ID &&
                             t.FLIGHT_NUMBER == f.FLIGHT_NUMBER && f.DEPART_DATE >= startDate
                             && f.DEPART_DATE <= endDate
                             select new
                             {
                                 f.AIRLINE_ID,
                                 f.FLIGHT_NUMBER,
                                 t.TICKET_ID,
                                 t.PASSENGER_NAME,
                                 t.TICKET_STATE,
                                 t.PRICE,
                                 t.FLIGHT_CLASS,
                                 f.COMPANY_NAME
                             };
            //获取改签的机票
            var cg_flight = from p in db.CHANGES
                            from t in db.PLANE_TICKET
                            from f in db.FLIGHT
                            where p.USER_ID == user_ID && p.TICKET_ID == t.TICKET_ID &&
                            t.FLIGHT_NUMBER == f.FLIGHT_NUMBER && f.DEPART_DATE >= startDate
                            && f.DEPART_DATE <= endDate
                            select new
                            {
                                f.AIRLINE_ID,
                                f.FLIGHT_NUMBER,
                                t.TICKET_ID,
                                t.PASSENGER_NAME,
                                t.TICKET_STATE,
                                t.PRICE,
                                t.FLIGHT_CLASS,
                                f.COMPANY_NAME
                            };
            //获取退订机票
            var can_flight = from p in db.CANCELS
                             from t in db.PLANE_TICKET
                             from f in db.FLIGHT
                             where p.USER_ID == user_ID && p.TICKET_ID == t.TICKET_ID &&
                             t.FLIGHT_NUMBER == f.FLIGHT_NUMBER && f.DEPART_DATE >= startDate
                             && f.DEPART_DATE <= endDate
                             select new
                             {
                                 f.AIRLINE_ID,
                                 f.FLIGHT_NUMBER,
                                 t.TICKET_ID,
                                 t.PASSENGER_NAME,
                                 t.TICKET_STATE,
                                 t.PRICE,
                                 t.FLIGHT_CLASS,
                                 f.COMPANY_NAME
                             };
            var all_flight = pur_flight.Union(cg_flight).Union(can_flight);
            foreach (var f in all_flight)
            {
                JsonData temp = new JsonData();
                var arriveAirport = from a in db.ARRIVES
                                    from p in db.AIRPORT
                                    where a.AIRLINE_ID == f.AIRLINE_ID && a.AIRPORT_ID == p.AIRPORT_ID
                                    select p;

                var arriveCity = from ap in arriveAirport
                                 from c in db.CITY
                                 where ap.CITY_ID == c.CITY_ID
                                 select c.CITY_NAME;

                var departAirport = from a in db.DEPARTS
                                    from p in db.AIRPORT
                                    where a.AIRLINE_ID == f.AIRLINE_ID && a.AIRPORT_ID == p.AIRPORT_ID
                                    select p;

                var departCity = from dp in departAirport
                                 from c in db.CITY
                                 where dp.CITY_ID == c.CITY_ID
                                 select c.CITY_NAME;


                var departDate = from ff in db.FLIGHT
                                 where ff.AIRLINE_ID == f.AIRLINE_ID && f.FLIGHT_NUMBER == ff.FLIGHT_NUMBER
                                 select ff.DEPART_DATE;

                var ticket = f.TICKET_ID;
                var user_seat_col = from us in db.USER_SEAT
                                    where us.TICKET_ID == f.TICKET_ID
                                    select us.SEAT_COL;

                var user_seat_row = from us in db.USER_SEAT
                                    where us.TICKET_ID == f.TICKET_ID
                                    select us.SEAT_ROW;
                var airline = from a in db.AIRLINE
                              where a.AIRLINE_ID == f.AIRLINE_ID
                              select a.AIRLINE_NAME;
                temp["airline"] = airline.ToList().FirstOrDefault().ToString();
                if (arriveCity.ToList().FirstOrDefault() == null || departCity.ToList().FirstOrDefault() == null || departDate.ToList().FirstOrDefault() == null)
                    continue;
                temp["arriveCity"] = arriveCity.ToList().FirstOrDefault().ToString();
                temp["departCity"] = departCity.ToList().FirstOrDefault().ToString();
                temp["departDate"] = departDate.ToList().FirstOrDefault().ToString();
                temp["flight_number"] = f.FLIGHT_NUMBER;
                temp["passenger_name"] = f.PASSENGER_NAME;
                temp["ticket_ID"] = f.TICKET_ID;
                string state = f.TICKET_STATE;
                if (state == "common" && departDate.ToList().FirstOrDefault() > DateTime.Now)
                    temp["state"] = "未出行";
                else if (state == "common")
                    temp["state"] = "已出行";
                else if (state == "canceled")
                    temp["state"] = "退订";
                else
                    temp["state"] = "改签";


                temp["price"] = f.PRICE;
                temp["seat_class"] = f.FLIGHT_CLASS;
                var row = user_seat_row.ToList().FirstOrDefault().ToString();
                var col = user_seat_col.ToList().FirstOrDefault();
                if (col == null)
                    temp["seat"] = "";
                else
                    temp["seat"] = row + col;
                temp["company_name"] = f.COMPANY_NAME;
                if (arriveAirport.ToList().FirstOrDefault() == null || departAirport.ToList().FirstOrDefault() == null)
                    temp["arriveAirport"] = arriveAirport.ToList().FirstOrDefault().AIRPORT_NAME;
                temp["departAirport"] = departAirport.ToList().FirstOrDefault().AIRPORT_NAME;
                response["data"].Add(temp);
            }

            return Json(response.ToJson());
        }

    }
}
