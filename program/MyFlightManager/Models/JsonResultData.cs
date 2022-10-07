using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFlightManager.Models
{
    public class JsonResultData<T>
    {
        public int Code { get; set; }
        public List<T> list { get; set; }

    }
}