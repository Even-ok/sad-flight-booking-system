//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyFlightManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PLANE_TICKET
    {
        public string TICKET_ID { get; set; }
        public string FLIGHT_NUMBER { get; set; }
        public Nullable<System.DateTime> FLIGHT_DATE { get; set; }
        public Nullable<int> PRICE { get; set; }
        public string FLIGHT_CLASS { get; set; }
        public string PASSENGER_NAME { get; set; }
        public string TICKET_STATE { get; set; }
    
        public virtual CANCELS CANCELS { get; set; }
        public virtual CHANGES CHANGES { get; set; }
        public virtual FLIGHT FLIGHT { get; set; }
        public virtual PURCHASES PURCHASES { get; set; }
        public virtual USER_SEAT USER_SEAT { get; set; }
    }
}