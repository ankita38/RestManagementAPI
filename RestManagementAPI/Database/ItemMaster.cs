//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestManagementAPI.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemMaster
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdateUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual GroupMaster GroupMaster { get; set; }
    }
}
