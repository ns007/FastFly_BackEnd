//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FastFly.BeckEnd
{
    using System;
    using System.Collections.Generic;
    
    public partial class StayExpense
    {
        public int DocId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public int TotalDays { get; set; }
        public decimal FeePerDay { get; set; }
    }
}
