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
    public class DestinationPeriodsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/DestinationPeriods
        public IQueryable<DestinationPeriods> GetDestinationPeriods()
        {
            return db.DestinationPeriods;
        }

        // GET: api/DestinationPeriods/5 the parameter is the reckoningDocument/ApplyDocument id 
        [ResponseType(typeof(DestinationPeriods))]
        public IHttpActionResult GetDestinationPeriods(int id)
        {
            List<DestinationPeriods> destinationPeriods = db.DestinationPeriods.Where(d=> d.DocId == id).ToList();
            if (destinationPeriods.Count == 0 || destinationPeriods == null)
            {
                return NotFound();
            }

            return Ok(destinationPeriods);
        }

        // PUT: api/DestinationPeriods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDestinationPeriods(string id, DestinationPeriods destinationPeriods)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destinationPeriods.CountryCode)
            {
                return BadRequest();
            }

            db.Entry(destinationPeriods).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationPeriodsExists(id))
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

        // POST: api/DestinationPeriods
        [ResponseType(typeof(DestinationPeriods))]
        public IHttpActionResult PostDestinationPeriods(DestinationPeriods destinationPeriods)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DestinationPeriods.Add(destinationPeriods);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DestinationPeriodsExists(destinationPeriods.CountryCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = destinationPeriods.CountryCode }, destinationPeriods);
        }

        // DELETE: api/DestinationPeriods/5
        [ResponseType(typeof(DestinationPeriods))]
        public IHttpActionResult DeleteDestinationPeriods(string id)
        {
            DestinationPeriods destinationPeriods = db.DestinationPeriods.Find(id);
            if (destinationPeriods == null)
            {
                return NotFound();
            }

            db.DestinationPeriods.Remove(destinationPeriods);
            db.SaveChanges();

            return Ok(destinationPeriods);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DestinationPeriodsExists(string id)
        {
            return db.DestinationPeriods.Count(e => e.CountryCode == id) > 0;
        }
    }
}