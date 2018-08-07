using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestManagementAPI.Models
{
    public class GroupMasterModel
    {
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}