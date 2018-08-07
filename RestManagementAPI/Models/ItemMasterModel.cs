using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestManagementAPI.Models
{
    public class ItemMasterModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string ItemName { get; set; }
        public double? Price { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string GroupName { get; set; }
    }
}