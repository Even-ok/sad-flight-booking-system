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
    
    public partial class AIRPLANE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AIRPLANE()
        {
            this.FLIGHT = new HashSet<FLIGHT>();
            this.SEAT = new HashSet<SEAT>();
        }
    
        public int AIRPLANE_ID { get; set; }
        public string COMPANY_ID { get; set; }
        public Nullable<int> AIRPLANE_STATE { get; set; }
        public string MODEL_NO { get; set; }
    
        public virtual AIRLINE_COMPANY AIRLINE_COMPANY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FLIGHT> FLIGHT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEAT> SEAT { get; set; }
    }
}
