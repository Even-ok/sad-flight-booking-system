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
    
    public partial class CITY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CITY()
        {
            this.AIRPORT = new HashSet<AIRPORT>();
        }
    
        public string CITY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public string COUNTRY { get; set; }
        public string COV19_RISK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AIRPORT> AIRPORT { get; set; }
    }
}
