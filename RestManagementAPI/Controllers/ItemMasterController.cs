using RestManagementAPI.Database;
using RestManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestManagementAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemMasterController : ApiController
    {
        [HttpGet]
        public IEnumerable<ItemMasterModel> GetList()
        {
            using (var ctx = new RestMgmtEntities())
            {
                IEnumerable<ItemMasterModel> itemList = (from it in ctx.ItemMasters join gr in ctx.GroupMasters on it.GroupId equals gr.Id select new ItemMasterModel { Id = it.Id, GroupName = gr.GroupName, ItemName = it.ItemName, Price = it.Price }).ToList();
               return itemList;
            }
        }

        [HttpPost]
        public IHttpActionResult Insert(ItemMasterModel model)
        {
            using (var ctx = new RestMgmtEntities())
            {
                ctx.ItemMasters.Add(new ItemMaster()
                {
                    ItemName = model.ItemName,
                    GroupId = model.GroupId,
                    Price = model.Price,
                    CreatedUserId = model.CreatedUserId,
                    CreatedDate = DateTime.Now
                });
                ctx.SaveChanges();
            }
            return Ok();
        }


        [HttpPut]
        public IHttpActionResult Update(int id, ItemMasterModel model)
        {
            using (var ctx = new RestMgmtEntities())
            {
                ItemMaster itemMaster = ctx.ItemMasters.Where(t => t.Id == id).FirstOrDefault();

                if(itemMaster!=null)
                {
                    itemMaster.ItemName = model.ItemName;
                    itemMaster.GroupId = model.GroupId;
                    itemMaster.Price = model.Price;
                    itemMaster.UpdatedDate = DateTime.Now;
                    itemMaster.UpdateUserId = model.UpdateUserId;
                    ctx.SaveChanges();
                }
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            using (var ctx = new RestMgmtEntities())
            {
                ItemMasterModel itemMasterModel = ctx.ItemMasters.Where(t => t.Id == id).Select(t=>new ItemMasterModel {
                    Id = t.Id,
                    ItemName = t.ItemName,
                    GroupId =t.GroupId,
                    Price=t.Price
                }).FirstOrDefault();


                
                return Ok(itemMasterModel);
            }           
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (var ctx = new RestMgmtEntities())
            {
                ItemMaster itemMaster = ctx.ItemMasters.Where(t => t.Id == id).FirstOrDefault();
                ctx.ItemMasters.Remove(itemMaster);
                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult CheckRecordExist(int id, int groupId, string name)
        {
            using (var ctx = new RestMgmtEntities())
            {
                if(id>0)
                {
                    int cnt = ctx.ItemMasters.Where(t => t.Id != id && t.GroupId == groupId && t.ItemName == name).Count();
                    if(cnt>0)
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
                    int cnt = ctx.ItemMasters.Where(t => t.GroupId == groupId && t.ItemName == name).Count();
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

    }
}
