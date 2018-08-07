using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestManagementAPI.Database;
using RestManagementAPI.Models;
using System.Web.Http.Cors;

namespace RestManagementAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GroupMasterController : ApiController
    {
        [HttpGet]
        public IEnumerable<GroupMasterModel> GetList()
        {
            using (var ctx = new RestMgmtEntities())
            {

                return ctx.GroupMasters.ToList().Select(t => new GroupMasterModel()
                {
                    Id = t.Id,
                    GroupCode = t.GroupCode,
                    GroupName = t.GroupName
                }).ToList().OrderBy(t=>t.GroupName);
            }
        }

        [HttpPost]
        public IHttpActionResult Insert(GroupMasterModel model)
        {
            using (var ctx = new RestMgmtEntities())
            {
                ctx.GroupMasters.Add(new GroupMaster()
                {
                    GroupCode = model.GroupCode,
                    GroupName = model.GroupName,
                    CreatedUserId = model.CreatedUserId
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult CheckRecordExist(int id, string groupCode)
        {
            using (var ctx = new RestMgmtEntities())
            {
                if(id>0)
                {
                    int cnt = ctx.GroupMasters.Where(t => t.GroupCode == groupCode && t.Id != id).Count();
                    if (cnt > 0)
                    {
                        return Ok("true");
                    }
                    else
                    {
                        return Ok("false");
                    }
                }
                else
                {
                    int cnt = ctx.GroupMasters.Where(t => t.GroupCode == groupCode).Count();
                    if (cnt > 0)
                    {
                        return Ok("true");
                    }
                    else
                    {
                        return Ok("false");
                    }
                }                
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (var ctx = new RestMgmtEntities())
            {
                var res = ctx.GroupMasters.Where(t => t.Id == id).FirstOrDefault();
                ctx.GroupMasters.Remove(res);
                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            GroupMasterModel groupMaster = new GroupMasterModel();
            using (var ctx = new RestMgmtEntities())
            {
                groupMaster = ctx.GroupMasters.Where(t => t.Id == id).Select(t => new GroupMasterModel()
                {
                    Id = t.Id,
                    GroupCode = t.GroupCode,
                    GroupName = t.GroupName,
                    CreatedUserId = t.CreatedUserId??0
                }).FirstOrDefault();
            }
            return Ok(groupMaster);
        }

        [HttpPut]
        public IHttpActionResult Update(GroupMasterModel model, int id)
        {
            GroupMaster groupMaster = new GroupMaster();
            using (var ctx = new RestMgmtEntities())
            {
                groupMaster = ctx.GroupMasters.Where(t => t.Id == id).FirstOrDefault();
                groupMaster.GroupCode = model.GroupCode;
                groupMaster.GroupName = model.GroupName;
                groupMaster.UpdatedUserId = model.CreatedUserId;
                groupMaster.UpdatedDate = DateTime.Now;
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
