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
    
    public partial class CANCELS
    {
        public Nullable<System.DateTime> CANCELS_TIME { get; set; }
        public string TICKET_ID { get; set; }
        public string USER_ID { get; set; }
        public Nullable<int> CANCELS_SURCHARGE { get; set; }
    
        public virtual PLANE_TICKET PLANE_TICKET { get; set; }
        public virtual USER_ACCOUNT USER_ACCOUNT { get; set; }
    }
}