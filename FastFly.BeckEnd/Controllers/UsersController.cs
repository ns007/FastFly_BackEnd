using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using FastFly.BeckEnd.enums;

namespace FastFly.BeckEnd.Controllers
{
    public class UsersController : ApiController
    {


        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            if (id.Equals("signers"))
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<User> signers =  GetSignersUsers();
                if (signers == null || signers.Count == 0)
                {
                    return NotFound();
                }
                return Ok(signers);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/signers
        [ResponseType(typeof(User))]
        public List<User> GetSignersUsers()
        {
            List<User> signerUsers = db.Users.Where(u => u.ApplicationRoleId == (int)ApplicationRoles.Signer && u.UserEnable == true).ToList();
            if (signerUsers == null || signerUsers.Count == 0)
            {
                return null;
            }

            return signerUsers;
        }

        [Route("api/Users/HeadOfDep/{id}")]
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult GetHeadOfDep(string id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            User applyUser = db.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();
            User HeadOfDepUser = db.Users.Where(b => b.ApplicationRoleId == (int)ApplicationRoles.HeadOfDepartment && b.DepartmentId == applyUser.DepartmentId).FirstOrDefault();
            if(HeadOfDepUser != null)
            {
                return Ok(HeadOfDepUser);
            }
            else
                return NotFound();
        }

        public IHttpActionResult Get(string id, string Password)
        {
            User user = db.Users.Find(id);
            if (user == null || !user.Password.Equals(Password) || user.UserEnable == false)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //user.Password = EncrpytPassword(user.Password);
            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        private string EncrpytPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            db.Entry(user).State = EntityState.Modified;
            user.UserEnable = false;
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

    }
}
