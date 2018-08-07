using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestManagementAPI.Models
{
    public class TableMasterModel
    {
        public int Id { get; set; }
        public string TableNo { get; set; }
        public string Description { get; set; }
        public int CreatedUserId { get; set; }
      
    }
}