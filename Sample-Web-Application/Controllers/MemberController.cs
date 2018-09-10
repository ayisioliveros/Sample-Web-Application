using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1DataAccess;

namespace WebApplication1.Controllers
{
    public class MemberController : ApiController
    {
        //Get All Members
        //api/member
        public IEnumerable<member> Get()
        {
            using (WebApplication1Entities entities = new WebApplication1Entities())
            {
                return entities.members.ToList();
            }
        }

        //Get Member by ID
        //api/member/{ID}
        public HttpResponseMessage Get(int id)
        {
            using (WebApplication1Entities entities = new WebApplication1Entities())
            {
                var entity = entities.members.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with Id = " + id.ToString() + " not found");
                }
            }
        }

        //Post Member
        //api/member
        public HttpResponseMessage Post([FromBody]member memb)
        {
            try
            {
                using (WebApplication1Entities entities = new WebApplication1Entities())
                {
                    entities.members.Add(memb);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, memb);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Put Member
        //api/member/{ID}
        public HttpResponseMessage Put(int id, [FromBody] member memb)
        {
            try
            {
                using (WebApplication1Entities entities = new WebApplication1Entities())
                {
                    var entity = entities.members.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with Id = " + id.ToString());
                    }
                    else
                    {

                        entity.userName = memb.userName;
                        entity.passWord = memb.passWord;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Delete Member
        //api/member/{ID}
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (WebApplication1Entities entities = new WebApplication1Entities())
                {
                    var entity = entities.members.Remove(entities.members.FirstOrDefault(e => e.ID == id));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with Id = " + id.ToString());
                    }
                    else
                    {
                        entities.members.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
