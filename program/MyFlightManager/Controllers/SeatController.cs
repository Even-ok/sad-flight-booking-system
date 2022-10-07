using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitJson;
using MyFlightManager.Models;

namespace MyFlightManager.Controllers
{
    public class SeatController : Controller
    {
        // GET: Seat
        private Entities db = new Entities();


        // GET: Seat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult chooseSeat()
        {
            return View();
        }

        //获取用户部分个人信息，显示在选座左处
        public ActionResult initMessage(string ticket_ID)
        {
            var ticket = (from t in db.PLANE_TICKET
                          where t.TICKET_ID == ticket_ID
                          select t).ToList().FirstOrDefault();
            var airline = from a in db.AIRLINE
                          from f in db.FLIGHT
                          where ticket.FLIGHT_NUMBER == f.FLIGHT_NUMBER && f.AIRLINE_ID == a.AIRLINE_ID
                          select a.AIRLINE_NAME;
            JsonData response = new JsonData();
            response["ticket_ID"] = ticket.TICKET_ID;
            response["passenger_name"] = ticket.PASSENGER_NAME;
            response["airline"] = airline.ToList().FirstOrDefault();
            response["flight_class"] = ticket.FLIGHT_CLASS;
            return Json(response.ToJson());
        }

        //获取该机票指定舱位的座位情况
        public JsonResult freeSeatQuery(string ticket_ID)
        {
            var flight_info = (from t in db.PLANE_TICKET
                               where t.TICKET_ID == ticket_ID
                               select t).ToList().FirstOrDefault();

            var airplane_ID = (from a in db.AIRPLANE
                               from f in db.FLIGHT
                               where a.AIRPLANE_ID == f.AIRPLANE_ID && f.FLIGHT_NUMBER == flight_info.FLIGHT_NUMBER
                               select a.AIRPLANE_ID).ToList().FirstOrDefault();

            var total_seats = (from s in db.SEAT
                               where s.AIRPLANE_ID == airplane_ID && s.FLIGHT_CLASS == flight_info.FLIGHT_CLASS
                               select s).ToList().FirstOrDefault();


            var take_seats = from us in db.USER_SEAT
                             from t in db.PLANE_TICKET
                             where t.FLIGHT_NUMBER == flight_info.FLIGHT_NUMBER &&
                             us.TICKET_ID == t.TICKET_ID && t.FLIGHT_CLASS == flight_info.FLIGHT_CLASS
                             select us;

            int row_sum = Convert.ToInt32(total_seats.ROW_SUM);
            int col_sum = Convert.ToInt32(total_seats.COLUMN_SUM);
            int[,] seats = new int[row_sum, col_sum];
            for (int i = 0; i < row_sum; i++)
                for (int j = 0; j < col_sum; j++)
                    seats[i, j] = 0;

            foreach (var item in take_seats)
            {
                seats[Convert.ToInt32(item.SEAT_ROW) - 1, item.SEAT_COL[0] - 'A'] = 2;
            }
            JsonData response = new JsonData();
            response["seats"] = new JsonData();
            response["seats"].SetJsonType(JsonType.Array);
            response["seats"] = JsonMapper.ToObject(JsonMapper.ToJson(seats));
            response["row"] = row_sum;
            response["col"] = col_sum;
            return Json(response.ToJson());

        }
        //用户选座
        public JsonResult pickSeat(string ticket_ID, int Row, int Col)
        {
            var seats = new USER_SEAT();
            seats.TICKET_ID = ticket_ID;
            seats.SEAT_ROW = Convert.ToByte(Row + 1);
            switch (Col)
            {
                case 0:
                    {
                        seats.SEAT_COL = "A";
                        break;
                    }
                case 1:
                    {
                        seats.SEAT_COL = "B";
                        break;
                    }
                case 2:
                    {
                        seats.SEAT_COL = "C";
                        break;
                    }
                case 3:
                    {
                        seats.SEAT_COL = "D";
                        break;
                    }
                case 4:
                    {
                        seats.SEAT_COL = "E";
                        break;
                    }
                case 5:
                    {
                        seats.SEAT_COL = "F";
                        break;
                    }
            }
            try
            {
                db.USER_SEAT.Add(seats);
                db.SaveChanges();
                return Json("选座成功");
            }
            catch
            {
                return Json("选座失败");
            }
        }
    }
}