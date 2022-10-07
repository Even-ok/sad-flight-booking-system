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
using System.Text.RegularExpressions;

namespace MyFlightManager.Controllers
{
    public class AdminManagerController : Controller
    {
        // GET: AdminManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult airportMessage()
        {
            return View();
        }
        public ActionResult planeMessage()
        {
            return View();
        }

        public ActionResult flightMessage()
        {
            return View();
        }

        public ActionResult classMessage()
        {
            return View();
        }

        public ActionResult cityMessage()
        {
            return View();
        }

        public ActionResult airlineMessage()
        {
            return View();
        }


        public ActionResult companyMessage()
        {
            return View();
        }


        Regex flight_number_re = new Regex(@"^[a-zA-Z]{2}\d[0-9]{4}$");
        Regex airport_ID_re = new Regex(@"(^[a-zA-Z]{3}$)");
        Regex city_ID_re = new Regex(@"^\d[0-9]{4}$");
        Regex airline_ID_re = new Regex(@"(^\d{6}$)");



        /*********************************正则检验******************************************/
        public bool REcheck(Dictionary<string, string> re)
        {
            if (re.ContainsKey("flight_number"))
            {
                if (flight_number_re.IsMatch(re["flight_number"]))
                {
                    return false;
                }
            }
            if (re.ContainsKey("airplane_state"))
            {
                if (re["airplane_state"] != "0" && re["airplane_state"] != "1")
                {
                    return false;
                }
            }
            if (re.ContainsKey("COV19_risk"))
            {
                if (re["COV19_risk"] != "l" && re["COV19_risk"] != "h" && re["COV19_risk"] != "m")
                {
                    return false;
                }
            }
            if (re.ContainsKey("city_ID"))
            {
                if (city_ID_re.IsMatch(re["city_ID"]))
                {
                    return false;
                }
            }
            if (re.ContainsKey("airline_ID"))
            {
                if (airline_ID_re.IsMatch(re["airline_ID"]))
                {
                    return false;
                }
            }
            return true;
        }
        /*********************************正则检验******************************************/






        private Entities db = new Entities();





        /*********************************航线信息管理******************************************/

        [HttpPost]
        public JsonResult addAirline(string airline_ID, string mileage, string airline_name)
        {
            int airline_ID_int, mileage_int; ;
            bool airline_ID_check = int.TryParse(airline_ID, out airline_ID_int);
            bool mileage_check = int.TryParse(mileage, out mileage_int);
            JsonData response = new JsonData();
            if (!airline_ID_check || !mileage_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            AIRLINE airline = new AIRLINE();
            airline.AIRLINE_ID = airline_ID_int;
            airline.MILEAGE = mileage_int;
            airline.AIRLINE_NAME = airline_name;
            // 防止插入同样的主码
            try
            {
                db.AIRLINE.Add(airline);
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
        public JsonResult airlineQuery(string airline_ID)
        {
            JsonData response = new JsonData();
            if (airline_ID != "")
            {
                int airline_ID_int;
                bool airline_ID_check = int.TryParse(airline_ID, out airline_ID_int);
                if (!airline_ID_check)
                {
                    response["error_code"] = 1;
                    return Json(response.ToJson());
                }
                var airline = from a in db.AIRLINE
                              where a.AIRLINE_ID == airline_ID_int
                              select a;
                response["data"] = new JsonData();
                foreach (var i in airline)
                {
                    JsonData temp = new JsonData();

                    temp["airlineID"] = i.AIRLINE_ID;
                    temp["airlinename"] = i.AIRLINE_NAME;
                    temp["miles"] = i.MILEAGE;

                    response["data"].Add(temp);
                };
            }
            else
            {
                response["error_code"] = 0;
                var airline = from a in db.AIRLINE
                              select a;
                response["data"] = new JsonData();
                foreach (var i in airline)
                {
                    JsonData temp = new JsonData();

                    temp["airlineID"] = i.AIRLINE_ID;
                    temp["airlinename"] = i.AIRLINE_NAME;
                    temp["miles"] = i.MILEAGE;

                    response["data"].Add(temp);
                }
            }
            return Json(response.ToJson());
        }

        [HttpPost]
        public JsonResult deleteAirline(string airline_ID)
        {
            JsonData response = new JsonData();
            int airline_ID_int;
            bool airline_ID_check = int.TryParse(airline_ID, out airline_ID_int);
            if (!airline_ID_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            try
            {

                AIRLINE airline = (from a in db.AIRLINE
                                   where a.AIRLINE_ID == airline_ID_int
                                   select a).Single();
                db.AIRLINE.Remove(airline);
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
        public JsonResult modifyAirline(string airline_ID, string mileage, string airline_name)
        {
            JsonData response = new JsonData();
            int airline_ID_int, mileage_int; ;
            bool airline_ID_check = int.TryParse(airline_ID, out airline_ID_int);
            bool mileage_check = int.TryParse(mileage, out mileage_int);
            if (!airline_ID_check || !mileage_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            try
            {
                AIRLINE airline = db.AIRLINE.Find(airline_ID_int);
                airline.AIRLINE_ID = airline_ID_int;
                airline.MILEAGE = mileage_int;
                airline.AIRLINE_NAME = airline_name;
                db.Entry(airline).State = EntityState.Modified;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }



        /*********************************航线信息管理******************************************/


        /*********************************公司信息管理******************************************/

        [HttpPost]
        public JsonResult companyQuery(string company_ID)
        {
            var company = from c in db.AIRLINE_COMPANY
                          where c.COMPANY_ID == company_ID
                          select c;
            if (company_ID == "")
                company = from c in db.AIRLINE_COMPANY
                          select c;
            JsonData response = new JsonData();
            response["data"] = new JsonData();
            foreach (var item in company)
            {
                JsonData temp = new JsonData();
                temp["companyID"] = item.COMPANY_ID;
                temp["companyName"] = item.COMPANY_NAME;
                response["data"].Add(temp);
            }
            return Json(response.ToJson());
        }


        [HttpPost]
        public JsonResult addCompany(string company_ID, string company_name)
        {
            JsonData response = new JsonData();
            AIRLINE_COMPANY airline_company = new AIRLINE_COMPANY();
            airline_company.COMPANY_ID = company_ID;
            airline_company.COMPANY_NAME = company_name;
            // 防止插入同样的主码
            try
            {
                db.AIRLINE_COMPANY.Add(airline_company);
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
        public JsonResult deleteCompany(string company_ID)
        {
            JsonData response = new JsonData();

            try
            {
                var company = (from c in db.AIRLINE_COMPANY
                               where c.COMPANY_ID == company_ID
                               select c).Single();
                db.AIRLINE_COMPANY.Remove(company);
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
        public JsonResult modifyCompany(string company_id, string company_name)
        {
            JsonData response = new JsonData();
            try
            {
                AIRLINE_COMPANY company = db.AIRLINE_COMPANY.Find(company_id);
                company.COMPANY_NAME = company_name;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }

        /*********************************公司信息管理******************************************/


        /*********************************航班信息管理******************************************/


        /// <summary>
        /// 获取航班相关信息
        /// </summary>
        /// <param name="flight_number"></param>
        /// <param name="company_name"></param>
        /// <param name="airline_ID"></param>
        /// <param name="airplane_ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult flightQuery(string flight_number, string company_name, string airline_ID)
        {
            bool airline_ID_check;
            int airline_ID_int;
            // 返回数据
            JsonData response = new JsonData();
            // 检查是否能够转化成int类型
            airline_ID_check = int.TryParse(airline_ID, out airline_ID_int);
            // 允许空字符
            airline_ID_check = airline_ID_check || airline_ID == "";
            // 其中一个为假说明输入存在问题
            if (!airline_ID_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            var flights = from f in db.FLIGHT
                          select f;

            // 进行查询
            if (flight_number != "")
                flights = from f in db.FLIGHT
                          where f.FLIGHT_NUMBER == flight_number
                          select f;
            if (company_name != "")
                flights = from f in flights
                          where f.COMPANY_NAME == company_name
                          select f;
            if (airline_ID != "")
                flights = from f in flights
                          where f.AIRLINE_ID == airline_ID_int
                          select f;
            var cc = flights.Count();
            response["data"] = new JsonData();
            foreach (var ff in flights)
            {
                var airline = from a in db.AIRLINE
                              where a.AIRLINE_ID == ff.AIRLINE_ID
                              select a;
                JsonData temp = new JsonData();
                temp["aircompany"] = ff.COMPANY_NAME;
                temp["arrivetime"] = ff.ARRIVE_DATE.ToString();
                temp["departtime"] = ff.DEPART_DATE.ToString();
                if (ff.FLIGHT_STATE == "on-time")
                    temp["flightstate"] = "准时到达";
                else if (ff.FLIGHT_STATE == "delayed")
                    temp["flightstate"] = "航班延迟";
                else if (ff.FLIGHT_STATE == "arrived")
                    temp["flightstate"] = "已到达";
                else
                    temp["flightstate"] = "正在飞行";
                temp["airlineID"] = ff.AIRLINE_ID;
                temp["flightnumber"] = ff.FLIGHT_NUMBER;
                temp["airline"] = airline.FirstOrDefault().AIRLINE_NAME;
                temp["HP"] = ff.FIRST_CLASS_PRICE;
                temp["SP"] = ff.ECONOMY_CLASS_PRICE;
                temp["airplaneID"] = ff.AIRPLANE_ID;
                response["data"].Add(temp);
            }

            return Json(response.ToJson());
            //return base.Json(response);
        }

        [HttpPost]
        public JsonResult flightDelete(string flight_number, DateTime depart_time)
        {
            JsonData response = new JsonData();
            try
            {
                var flight = (from f in db.FLIGHT
                              where f.FLIGHT_NUMBER == flight_number && f.DEPART_DATE == depart_time
                              select f).Single();
                db.FLIGHT.Remove(flight);
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
        public JsonResult flightAdd(string flight_number, string airplane_id, DateTime depart_data, DateTime arrive_date,
      string flight_state, string company_name, string airline_ID, string first_class_price, string economy_class_price)
        {
            Dictionary<string, string> re = new Dictionary<string, string>();
            JsonData response = new JsonData();
            re["flight_number"] = flight_number;
            re["flight_state"] = flight_state;
            if (!REcheck(re))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }


            try
            {
                var flight = new FLIGHT();
                flight.FLIGHT_NUMBER = flight_number;
                flight.AIRPLANE_ID = Convert.ToInt32(airplane_id);
                flight.DEPART_DATE = depart_data;
                flight.ARRIVE_DATE = arrive_date;
                flight.FLIGHT_STATE = flight_state;
                flight.COMPANY_NAME = company_name;
                flight.AIRLINE_ID = Convert.ToInt32(airline_ID);
                flight.FIRST_CLASS_PRICE = Convert.ToInt32(first_class_price);
                flight.ECONOMY_CLASS_PRICE = Convert.ToInt32(economy_class_price);
                db.FLIGHT.Add(flight);
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
        public JsonResult flightModify(string flight_number, string airplane_id, DateTime depart_data, DateTime arrive_date,
            string flight_state, string company_name, string airline_ID, string first_class_price, string economy_class_price)
        {
            JsonData response = new JsonData();
            Dictionary<string, string> re = new Dictionary<string, string>();
            re["flight_number"] = flight_number;
            if (!REcheck(re))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            try
            {

                var flight = (from f in db.FLIGHT
                              where f.FLIGHT_NUMBER == flight_number
                              select f).Single();
                flight.AIRPLANE_ID = Convert.ToInt32(airplane_id);
                flight.ARRIVE_DATE = arrive_date;
                if (flight_state == "准时到达")
                    flight.FLIGHT_STATE = "on-time";
                else if (flight_state == "航班延迟")
                    flight.FLIGHT_STATE = "delayed";
                else if (flight_state == "已到达")
                    flight.FLIGHT_STATE = "arrived";
                else if (flight_state == "正在飞行")
                    flight.FLIGHT_STATE = "flying";
                flight.COMPANY_NAME = company_name;
                flight.AIRLINE_ID = Convert.ToInt32(airline_ID);
                flight.FIRST_CLASS_PRICE = Convert.ToInt32(first_class_price);
                flight.ECONOMY_CLASS_PRICE = Convert.ToInt32(economy_class_price);
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }


        /*********************************航班信息管理******************************************/


        /*********************************飞机信息管理******************************************/

        [HttpPost]
        public ActionResult airPlaneModify(string airplane_ID, string airplane_state, string company_ID, string model_no)
        {
            JsonData response = new JsonData();
            int airplane_ID_int;
            int airplane_state_int;
            bool airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            bool airplane_state_check = int.TryParse(airplane_state, out airplane_state_int);
            // 允许空字符
            if (!airplane_ID_check || !airplane_state_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            try
            {
                var airplane = (from a in db.AIRPLANE
                                where a.AIRPLANE_ID == airplane_ID_int
                                select a).Single();

                airplane.AIRPLANE_STATE = airplane_state_int;
                airplane.COMPANY_ID = company_ID;
                airplane.MODEL_NO = model_no;
                //db.AIRPLANE.Add(airplane);
                db.Entry(airplane).State = EntityState.Modified;
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
        public JsonResult addAirplane(string airplane_ID, string airplane_state, string company_ID, string model_no)
        {
            JsonData response = new JsonData();
            AIRPLANE airplane = new AIRPLANE();
            int airplane_ID_int;
            int airplane_state_int;
            bool airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            bool airplane_state_check = int.TryParse(airplane_state, out airplane_state_int);
            // 允许空字符
            airplane_ID_check = airplane_ID_check || airplane_ID == "";
            airplane_state_check = airplane_state_check || airplane_state == "";
            if (!(airplane_ID_check && airplane_state_check))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            airplane.AIRPLANE_ID = airplane_ID_int;
            airplane.AIRPLANE_STATE = airplane_state_int;
            airplane.COMPANY_ID = company_ID;
            airplane.MODEL_NO = model_no;
            // 防止插入同样的主码
            try
            {
                db.AIRPLANE.Add(airplane);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }

        /// <summary>
        ///  飞机信息查询
        /// </summary>
        /// <param name="airplane_ID">飞机ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult airplaneQuery(string airplane_ID)
        {
            bool airplane_ID_check;
            int airplane_ID_int;
            // 返回数据
            JsonData response = new JsonData();
            response["data"] = new JsonData();
            // 检查是否能够转化成int类型
            airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            // 允许空字符
            airplane_ID_check = airplane_ID_check || airplane_ID == "";
            // 其中一个为假说明输入存在问题
            if (!airplane_ID_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            var airplane = from a in db.AIRPLANE
                           select a;
            // airplane_ID 不能为空
            if (airplane_ID != "")
                airplane = from s in airplane
                           where s.AIRPLANE_ID == airplane_ID_int
                           select s;

            foreach (var item in airplane)
            {
                JsonData temp = new JsonData();
                temp["airplaneID"] = item.AIRPLANE_ID;
                temp["airplanestate"] = item.AIRPLANE_STATE;
                temp["companyID"] = item.COMPANY_ID;
                if (Convert.ToInt32(item.AIRPLANE_STATE) == 1)
                    temp["airplanestate"] = "待维修";
                else
                    temp["airplanestate"] = "正常";
                temp["model"] = item.MODEL_NO;
                response["data"].Add(temp);
            }
            return Json(response.ToJson());
        }




        public ActionResult airplaneDelete(string airplane_ID)
        {

            bool airplane_ID_check;
            int airplane_ID_int;
            // 返回数据
            JsonData response = new JsonData();
            // 检查是否能够转化成int类型
            airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            // 允许空字符
            airplane_ID_check = airplane_ID_check || airplane_ID == "";
            // 其中一个为假说明输入存在问题
            if (!airplane_ID_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            try
            {
                var airplane = (from a in db.AIRPLANE
                                where a.AIRPLANE_ID == airplane_ID_int
                                select a).Single();
                db.AIRPLANE.Remove(airplane);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }



        /*********************************飞机信息管理******************************************/


        /*********************************城市信息管理******************************************/
        [HttpPost]
        public JsonResult cityQuery(string city_id)
        {
            JsonData response = new JsonData();
            if (city_id != "")
            {
                CITY city = db.CITY.Find(city_id);
                if (city != null)
                {
                    response["cityinfo"] = new JsonData();
                    response["cityinfo"].SetJsonType(JsonType.Array);
                    var cinfo = new
                    {
                        cityID = city.CITY_ID,
                        cityname = city.CITY_NAME,
                        country = city.COUNTRY,
                        COV19risk = city.COV19_RISK
                    };
                    response["cityinfo"].Add(JsonMapper.ToObject(JsonMapper.ToJson(cinfo)));
                    response["error_code"] = 0;
                }
                else
                    response["error_code"] = 1;
            }
            else
            {
                response["error_code"] = 0;
                var city = from c in db.CITY
                           select c;
                response["cityinfo"] = new JsonData();
                response["cityinfo"].SetJsonType(JsonType.Array);
                foreach (var i in city)
                {
                    string risk;
                    if (i.COV19_RISK == "h")
                        risk = "高风险";
                    else if (i.COV19_RISK == "m")
                        risk = "中风险";
                    else
                        risk = "低风险";
                    var cinfo = new
                    {
                        cityID = i.CITY_ID,
                        cityname = i.CITY_NAME,
                        country = i.COUNTRY,
                        COV19risk = risk
                    };
                    response["cityinfo"].Add(JsonMapper.ToObject(JsonMapper.ToJson(cinfo)));
                }
            }
            return Json(response.ToJson());
        }


        [HttpPost]
        public JsonResult cityDelete(string city_id)
        {
            JsonData response = new JsonData();
            CITY city = db.CITY.Find(city_id);
            try
            {
                db.CITY.Remove(city);
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
        public JsonResult cityModify(string city_id, string city_name, string country, string COV19risk)
        {
            JsonData response = new JsonData();
            Dictionary<string, string> re = new Dictionary<string, string>();
            re["COV19_risk"] = COV19risk;
            if (!REcheck(re))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            CITY city = db.CITY.Find(city_id);
            city.CITY_NAME = city_name;
            city.COUNTRY = country;
            string risk;
            if (city.COV19_RISK == "高风险")
                risk = "h";
            else if (city.COV19_RISK == "中风险")
                risk = "m";
            else
                risk = "l";
            city.COV19_RISK = risk;
            try
            {
                db.Entry(city).State = EntityState.Modified;
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
        public JsonResult cityAdd(string city_id, string city_name, string country, string COV19risk)
        {
            JsonData response = new JsonData();
            Dictionary<string, string> re = new Dictionary<string, string>();
            re["city_ID"] = city_id;
            re["COV19_risk"] = COV19risk;
            if (!REcheck(re))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            CITY city = new CITY(); ;
            city.CITY_ID = city_id;
            city.CITY_NAME = city_name;
            city.COUNTRY = country;
            city.COV19_RISK = COV19risk;
            try
            {
                db.CITY.Add(city);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }
        /*********************************城市信息管理******************************************/


        /*********************************座位信息管理******************************************/
        public JsonResult airplaneSeatQuery(string airplane_ID, string flight_class)
        {

            bool airplane_ID_check;
            int airplane_ID_int;
            // 返回数据
            JsonData response = new JsonData();
            response["data"] = new JsonData();
            // 检查是否能够转化成int类型
            airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            // 允许空字符
            airplane_ID_check = airplane_ID_check || airplane_ID == "";
            // 其中一个为假说明输入存在问题
            if (!airplane_ID_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            var seat = from s in db.SEAT
                       select s;
            // airplane_ID 不能为空
            if (airplane_ID != "")
                seat = from s in seat
                       where s.AIRPLANE_ID == airplane_ID_int
                       select s;


            if (flight_class != "")
                seat = from s in seat
                       where s.FLIGHT_CLASS == flight_class
                       select s;
            foreach (var item in seat)
            {
                JsonData temp = new JsonData();
                temp["airplaneID"] = item.AIRPLANE_ID;
                temp["flightclass"] = item.FLIGHT_CLASS;
                temp["rowsum"] = item.ROW_SUM;
                temp["columnsum"] = item.COLUMN_SUM;
                response["data"].Add(temp);
            }
            return Json(response.ToJson());
        }



        public ActionResult seatDelete(string airplane_ID, string flight_class)
        {

            bool airplane_ID_check;
            int airplane_ID_int;
            // 返回数据
            JsonData response = new JsonData();
            // 检查是否能够转化成int类型
            airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            // 允许空字符
            airplane_ID_check = airplane_ID_check || airplane_ID == "";
            // 其中一个为假说明输入存在问题
            if (!airplane_ID_check)
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            try
            {
                var seat = (from s in db.SEAT
                            where s.AIRPLANE_ID == airplane_ID_int && s.FLIGHT_CLASS == flight_class
                            select s).Single();
                db.SEAT.Remove(seat);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }


        public ActionResult seatAdd(string airplane_ID, string flight_class, string row_sum, string col_sum)
        {

            bool airplane_ID_check, row_sum_check, col_sum_check;
            int airplane_ID_int, row_sum_int, col_sum_int;
            // 返回数据
            JsonData response = new JsonData();
            // 检查是否能够转化成int类型
            airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            row_sum_check = int.TryParse(row_sum, out row_sum_int);
            col_sum_check = int.TryParse(col_sum, out col_sum_int);
            // 允许空字符
            airplane_ID_check = airplane_ID_check || airplane_ID == "";
            row_sum_check = row_sum_check || row_sum == "";
            col_sum_check = col_sum_check || col_sum == "";

            // 其中一个为假说明输入存在问题
            if (!(airplane_ID_check && row_sum_check && col_sum_check))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            try
            {
                var seat = new SEAT();
                seat.AIRPLANE_ID = airplane_ID_int;
                seat.COLUMN_SUM = Convert.ToByte(col_sum_int);
                seat.ROW_SUM = Convert.ToByte(row_sum_int);
                seat.FLIGHT_CLASS = flight_class;
                db.SEAT.Add(seat);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }

        public ActionResult seatModify(string airplane_ID, string flight_class, string row_sum, string col_sum)
        {

            bool airplane_ID_check, row_sum_check, col_sum_check;
            int airplane_ID_int, row_sum_int, col_sum_int;
            // 返回数据
            JsonData response = new JsonData();
            // 检查是否能够转化成int类型
            airplane_ID_check = int.TryParse(airplane_ID, out airplane_ID_int);
            row_sum_check = int.TryParse(row_sum, out row_sum_int);
            col_sum_check = int.TryParse(col_sum, out col_sum_int);
            // 其中一个为假说明输入存在问题
            if (!(row_sum_check && col_sum_check && airplane_ID_check))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }

            try
            {
                var seat = (from s in db.SEAT
                            where s.AIRPLANE_ID == airplane_ID_int && s.FLIGHT_CLASS == flight_class
                            select s).Single();

                seat.COLUMN_SUM = Convert.ToByte(col_sum_int);
                seat.ROW_SUM = Convert.ToByte(row_sum_int);
                seat.FLIGHT_CLASS = flight_class;
                db.Entry(seat).State = EntityState.Modified; ;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }



        /*********************************座位信息管理******************************************/


        /*********************************公司信息管理******************************************/

        public JsonResult companyQuery()
        {
            var arr_airport = from aa in db.AIRPORT
                              where aa.AIRPORT_ID == "123"
                              select aa;
            foreach (var arr in arr_airport)
                return base.Json(arr.AIRPORT_NAME.ToString());
            return base.Json(arr_airport.Count());
        }

        /*********************************机场信息管理******************************************/

        public ActionResult airportQuery(string airport_name)
        {
            var airport = from a in db.AIRPORT
                          where a.AIRPORT_NAME == airport_name
                          select a;
            if (airport_name == "")
                airport = from a in db.AIRPORT
                          select a;
            JsonData response = new JsonData();
            response["data"] = new JsonData();
            foreach (var item in airport)
            {
                JsonData temp = new JsonData();
                temp["airportID"] = item.AIRPORT_ID;
                temp["cityID"] = item.CITY_ID;
                temp["airportname"] = item.AIRPORT_NAME;
                response["data"].Add(temp);
            }
            return Json(response.ToJson());
        }


        public ActionResult airportDelete(string airport_ID)
        {
            JsonData response = new JsonData();
            try
            {
                var airport = (from a in db.AIRPORT
                               where a.AIRPORT_ID == airport_ID
                               select a).Single();
                db.AIRPORT.Remove(airport);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }

        public ActionResult airportAdd(string airport_ID, string city_ID, string airport_name)
        {
            JsonData response = new JsonData();
            Dictionary<string, string> re = new Dictionary<string, string>();
            re["airport_ID"] = airport_ID;
            if (!REcheck(re))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            AIRPORT airport = new AIRPORT();
            airport.AIRPORT_ID = airport_ID;
            airport.CITY_ID = city_ID;
            airport.AIRPORT_NAME = airport_name;
            try
            {
                db.AIRPORT.Add(airport);
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());
        }

        public ActionResult airportUpdate(string airport_ID, string city_ID, string airport_name)
        {
            JsonData response = new JsonData();
            Dictionary<string, string> re = new Dictionary<string, string>();
            re["airport_ID"] = airport_ID;
            if (!REcheck(re))
            {
                response["error_code"] = 1;
                return Json(response.ToJson());
            }
            try
            {
                var airport = (from a in db.AIRPORT
                               where a.AIRPORT_ID == airport_ID
                               select a).Single();
                airport.AIRPORT_ID = airport_ID;
                airport.CITY_ID = city_ID;
                airport.AIRPORT_NAME = airport_name;
                db.Entry(airport).State = EntityState.Modified;
                db.SaveChanges();
                response["error_code"] = 0;
            }
            catch
            {
                response["error_code"] = 1;
            }
            return Json(response.ToJson());

        }


        /*********************************机场信息管理******************************************/
    }
}