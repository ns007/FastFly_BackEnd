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
    public class CarRentsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/CarRents
        public IQueryable<CarRent> GetCarRents()
        {
            return db.CarRents;
        }

        // GET: api/CarRents/5 the parameter is the reckoningDocument/ApplyDocument id 
        [ResponseType(typeof(CarRent))]
        public IHttpActionResult GetCarRent(int id)
        {
            List<CarRent> carRent = db.CarRents.Where(d => d.DocId == id).ToList();
            if (carRent.Count == 0 || carRent == null)
            {
                return NotFound();
            }

            return Ok(carRent);
        }

        // PUT: api/CarRents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarRent(int id, CarRent carRent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carRent.DocId)
            {
                return BadRequest();
            }

            db.Entry(carRent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarRentExists(id))
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

        // POST: api/CarRents
        [ResponseType(typeof(CarRent))]
        public IHttpActionResult PostCarRent(CarRent carRent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarRents.Add(carRent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CarRentExists(carRent.DocId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = carRent.DocId }, carRent);
        }

        // DELETE: api/CarRents/5
        [ResponseType(typeof(CarRent))]
        public IHttpActionResult DeleteCarRent(int id)
        {
            CarRent carRent = db.CarRents.Find(id);
            if (carRent == null)
            {
                return NotFound();
            }

            db.CarRents.Remove(carRent);
            db.SaveChanges();

            return Ok(carRent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarRentExists(int id)
        {
            return db.CarRents.Count(e => e.DocId == id) > 0;
        }
    }
}