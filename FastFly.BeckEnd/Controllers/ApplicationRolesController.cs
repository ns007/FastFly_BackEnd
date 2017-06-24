using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FastFly.BeckEnd;

namespace FastFly.BeckEnd.Controllers
{
    public class ApplicationRolesController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/ApplicationRoles
        public IQueryable<ApplicationRole> GetApplicationRoles()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.ApplicationRoles;
        }

        // GET: api/ApplicationRoles/5
        [ResponseType(typeof(ApplicationRole))]
        public IHttpActionResult GetApplicationRole(int id)
        {
            ApplicationRole applicationRole = db.ApplicationRoles.Find(id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return Ok(applicationRole);
        }

        // PUT: api/ApplicationRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationRole(int id, ApplicationRole applicationRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationRole.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleExists(id))
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

        // POST: api/ApplicationRoles
        [ResponseType(typeof(ApplicationRole))]
        public IHttpActionResult PostApplicationRole(ApplicationRole applicationRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationRoles.Add(applicationRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = applicationRole.Id }, applicationRole);
        }

        // DELETE: api/ApplicationRoles/5
        [ResponseType(typeof(ApplicationRole))]
        public IHttpActionResult DeleteApplicationRole(int id)
        {
            ApplicationRole applicationRole = db.ApplicationRoles.Find(id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            db.ApplicationRoles.Remove(applicationRole);
            db.SaveChanges();

            return Ok(applicationRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationRoleExists(int id)
        {
            return db.ApplicationRoles.Count(e => e.Id == id) > 0;
        }
    }
}