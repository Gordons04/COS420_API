//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResourceServer.Api.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class vote
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public int User_Profile_uid { get; set; }
    
        public virtual User_Profile User_Profile { get; set; }
    }
}
