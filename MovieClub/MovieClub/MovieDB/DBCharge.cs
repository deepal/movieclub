//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieClub.MovieDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class DBCharge
    {
        public int ChargeId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> MovieId { get; set; }
        public Nullable<double> Charge { get; set; }
        public Nullable<int> Paid { get; set; }
    }
}
