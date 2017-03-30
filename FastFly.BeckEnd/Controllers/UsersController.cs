using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using FastFly.BeckEnd.Models;
using System.Security.Cryptography;
using System.Data.Entity;

namespace FastFly.BeckEnd.Controllers
{
    public class UsersController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["FlyFastConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection();

        public IEnumerable<User> Get()
        {
            using (var context = new FastFlyModelContainer())
            {
                try
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    IEnumerable<User> allusers =  context.Users.Include("Department1").Include("Faculty1").
                                                  Include("ApplicationRole1").Include("ApplyDocuments").
                                                  Include("ReckoningDocuments").ToList();
                    return allusers;
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }

            }
            return null;
        }

        public IHttpActionResult Get(string Id, string Password)
        {
            using (var context = new FastFlyModelContainer())
            {
                try
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    IEnumerable<User> allusers = context.Users.Include("Department1").Include("Faculty1").
                                                  Include("ApplicationRole1").Include("ApplyDocuments").
                                                  Include("ReckoningDocuments").ToList();
                    User user = allusers.Where(u => u.Id == Id && u.Password == Password).FirstOrDefault();
                    if (user != null)
                        return Ok(user);
                    else
                        return NotFound();
                }
                catch (Exception ex)
                {
                    //return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }

            }
            return null;
        }

        // POST api/users - add user to db 
        public HttpResponseMessage Post(User user)
        {
            using (var context = new FastFlyModelContainer())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model");
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
                
            }
           
        }
    }
}
