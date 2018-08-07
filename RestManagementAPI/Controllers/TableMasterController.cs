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
    public class TableMasterController : ApiController
    {
        [HttpGet]
        public IEnumerable<TableMaster> GetList()
        {
            using (var ctx = new Database.RestMgmtEntities())
            {
               return ctx.TableMasters.ToList();
            }
        }

        [HttpPost]
        public IHttpActionResult Insert(TableMasterModel model)
        {
            using (var ctx = new Database.RestMgmtEntities())
            {
                ctx.TableMasters.Add(new TableMaster()
                {
                    TableNo = model.TableNo,
                    Description = model.Description,
                    CreatedUserId = model.CreatedUserId,
                    CreatedDateTime = DateTime.Now
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetTableMasterDetail(int id, TableMaster model)
        {
            using (var ctx = new Database.RestMgmtEntities())
            {
                TableMaster tableMaster = ctx.TableMasters.Where(t => t.Id == id).FirstOrDefault(); 
                return Ok(tableMaster);
            }           
        }

        [HttpPut]
        public IHttpActionResult UpdateTableMasterDetail(int id, TableMasterModel model)
        {
            using (var ctx = new Database.RestMgmtEntities())
            {
                TableMaster tableMaster = ctx.TableMasters.Where(t => t.Id == id).FirstOrDefault();
                tableMaster.TableNo = model.TableNo;
                tableMaster.Description = model.Description;
                tableMaster.UpdatedUserId = model.CreatedUserId;
                tableMaster.UpdateDateTime = DateTime.Now;
                ctx.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (var ctx = new Database.RestMgmtEntities())
            {
                TableMaster tableMaster = ctx.TableMasters.Where(t => t.Id == id).FirstOrDefault();
                ctx.TableMasters.Remove(tableMaster);
                ctx.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult RecordExist(int? id, string tableNo)
        {
            using (var ctx = new Database.RestMgmtEntities())
            {
                if(id==null)
                {
                    int cnt = ctx.TableMasters.Where(t => t.TableNo == tableNo).Count();
                    if(cnt==0)
                    {
                        return Ok("false");
                    }
                    else
                    {
                        return Ok("true");
                    }                   
                }
                else if(id>0)
                {
                    int cnt = ctx.TableMasters.Where(t => t.TableNo == tableNo && t.Id!=id).Count();
                    if (cnt == 0)
                    {
                        return Ok("false");
                    }
                    else
                    {
                        return Ok("true");
                    }
                }
                return Ok(false);
            }
        }
    }
}
