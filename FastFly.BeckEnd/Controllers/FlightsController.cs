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
    public class FlightsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/Flights
        public IQueryable<Flight> GetFlights()
        {
            return db.Flights;
        }

        // GET: api/Flights/5 the parameter is the reckoningDocument/ApplyDocument id 
        [ResponseType(typeof(Flight))]
        public IHttpActionResult GetFlight(int id)
        {
            List<Flight> flight = db.Flights.Where(d => d.DocId == id).ToList();
            if (flight.Count == 0 || flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        // PUT: api/Flights/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlight(int DocId, Flight[] flights)
        {
            foreach(Flight flight in flights)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (DocId != flight.DocId)
                {
                    return BadRequest();
                }

                db.Entry(flight).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(DocId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Flights
        [ResponseType(typeof(Flight))]
        public IHttpActionResult PostFlight(Flight[] flights)
        {
            foreach (Flight flight in flights)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                db.Flights.Add(flight);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (FlightExists(flight.DocId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(flights);
            //return CreatedAtRoute("DefaultApi", new { id = flight.FlightDate }, flight);
        }

        // DELETE: api/Flights/5
        [ResponseType(typeof(Flight))]
        public IHttpActionResult DeleteFlight(DateTime id)
        {
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return NotFound();
            }

            db.Flights.Remove(flight);
            db.SaveChanges();

            return Ok(flight);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlightExists(int docId)
        {
            return db.Flights.Count(e => e.DocId == docId) > 0;
        }
    }
}