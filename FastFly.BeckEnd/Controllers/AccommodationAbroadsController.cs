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
    public class AccommodationAbroadsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/AccommodationAbroads
        public IQueryable<AccommodationAbroad> GetAccommodationAbroads()
        {
            return db.AccommodationAbroads;
        }

        // GET: api/AccommodationAbroads/5
        [ResponseType(typeof(AccommodationAbroad))]
        public IHttpActionResult GetAccommodationAbroad(int id)
        {
            AccommodationAbroad accommodationAbroad = db.AccommodationAbroads.Find(id);
            if (accommodationAbroad == null)
            {
                return NotFound();
            }

            return Ok(accommodationAbroad);
        }

        // PUT: api/AccommodationAbroads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccommodationAbroad(int id, AccommodationAbroad accommodationAbroad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accommodationAbroad.DocId)
            {
                return BadRequest();
            }

            db.Entry(accommodationAbroad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccommodationAbroadExists(id))
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

        // POST: api/AccommodationAbroads
        [ResponseType(typeof(AccommodationAbroad))]
        public IHttpActionResult PostAccommodationAbroad(AccommodationAbroad accommodationAbroad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccommodationAbroads.Add(accommodationAbroad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccommodationAbroadExists(accommodationAbroad.DocId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accommodationAbroad.DocId }, accommodationAbroad);
        }

        // DELETE: api/AccommodationAbroads/5
        [ResponseType(typeof(AccommodationAbroad))]
        public IHttpActionResult DeleteAccommodationAbroad(int id)
        {
            AccommodationAbroad accommodationAbroad = db.AccommodationAbroads.Find(id);
            if (accommodationAbroad == null)
            {
                return NotFound();
            }

            db.AccommodationAbroads.Remove(accommodationAbroad);
            db.SaveChanges();

            return Ok(accommodationAbroad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccommodationAbroadExists(int id)
        {
            return db.AccommodationAbroads.Count(e => e.DocId == id) > 0;
        }
    }
}