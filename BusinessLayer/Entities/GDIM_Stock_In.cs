//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLayer.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class GDIM_Stock_In
    {
        public string SI_ID { get; set; }
        public string SI_Me_ID { get; set; }
        public string SI_Me_Specification_ID { get; set; }
        public Nullable<decimal> SI_Me_Unit_Price { get; set; }
        public Nullable<decimal> SI_Me_Quantity { get; set; }
        public Nullable<System.DateTime> Si_Date { get; set; }
        public Nullable<int> SI_Flag { get; set; }
        public string SI_Audit_User_ID { get; set; }
        public string SI_Audit_User_Name { get; set; }
        public string SI_Audit_Advice { get; set; }
        public string SI_Remark { get; set; }
    }
}
