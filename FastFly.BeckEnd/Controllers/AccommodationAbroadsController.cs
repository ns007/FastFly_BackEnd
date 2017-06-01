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

        // GET: api/AccommodationAbroads/5 the parameter is the reckoningDocument/ApplyDocument id 
        [ResponseType(typeof(AccommodationAbroad))]
        public IHttpActionResult GetAccommodationAbroad(int id)
        {
            List<AccommodationAbroad> accommodationAbroad = db.AccommodationAbroads.Where(d => d.DocId == id).ToList();
            if (accommodationAbroad.Count == 0 || accommodationAbroad == null)
            {
                return NotFound();
            }

            return Ok(accommodationAbroad);
        }

        // PUT: api/AccommodationAbroads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccommodationAbroad(int docId, string fromDate, string toDate, AccommodationAbroad accommodationAbroad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime AccommodationAbroadFromDate = Utilities.stringToDateTime(fromDate);
            DateTime AccommodationAbroadToDate = Utilities.stringToDateTime(toDate);
            if (docId != accommodationAbroad.DocId || (AccommodationAbroadFromDate.Year != accommodationAbroad.FromDate.Year || AccommodationAbroadFromDate.Month != accommodationAbroad.FromDate.Month || AccommodationAbroadFromDate.Day != accommodationAbroad.FromDate.Day) ||
                (AccommodationAbroadToDate.Year != accommodationAbroad.ToDate.Year || AccommodationAbroadToDate.Month != accommodationAbroad.ToDate.Month || AccommodationAbroadToDate.Day != accommodationAbroad.ToDate.Day)
                )
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
                if (!AccommodationAbroadExists(docId, fromDate, toDate))
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
        public IHttpActionResult PostAccommodationAbroad(AccommodationAbroad[] accommodationsAbroad)
        {
            foreach(AccommodationAbroad accommodationAbroad in accommodationsAbroad)
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
                    string fromMonth = accommodationAbroad.FromDate.Month.ToString().Length == 1 ? "0" + accommodationAbroad.FromDate.Month.ToString() : accommodationAbroad.FromDate.Month.ToString();
                    string fromDay = accommodationAbroad.FromDate.Day.ToString().Length == 1 ? "0" + accommodationAbroad.FromDate.Day.ToString() : accommodationAbroad.FromDate.Day.ToString();
                    string fromDate = accommodationAbroad.FromDate.Year.ToString() + fromMonth + fromDay;
                    string toMonth = accommodationAbroad.ToDate.Month.ToString().Length == 1 ? "0" + accommodationAbroad.ToDate.Month.ToString() : accommodationAbroad.ToDate.Month.ToString();
                    string toDay = accommodationAbroad.ToDate.Day.ToString().Length == 1 ? "0" + accommodationAbroad.ToDate.Day.ToString() : accommodationAbroad.ToDate.Day.ToString();
                    string toDate = accommodationAbroad.FromDate.Year.ToString() + toMonth + toDay;
                    if (AccommodationAbroadExists(accommodationAbroad.DocId,fromDate,toDate))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(accommodationsAbroad);
        }

        // DELETE: api/AccommodationAbroads/5
        [ResponseType(typeof(AccommodationAbroad))]
        public IHttpActionResult DeleteAccommodationAbroad(int docId,string fromDate, string toDate)
        {
            DateTime AccommodationAbroadFromDate = Utilities.stringToDateTime(fromDate);
            DateTime AccommodationAbroadToDate = Utilities.stringToDateTime(toDate);
            List<AccommodationAbroad> accommodationAbroad = db.AccommodationAbroads.Where(e => e.DocId == docId && (e.FromDate.Year == AccommodationAbroadFromDate.Year && e.FromDate.Month == AccommodationAbroadFromDate.Month && e.FromDate.Day == AccommodationAbroadFromDate.Day) && (e.ToDate.Year == AccommodationAbroadToDate.Year && e.ToDate.Month == AccommodationAbroadToDate.Month && e.ToDate.Day == AccommodationAbroadToDate.Day)).ToList();
            if (accommodationAbroad.Count != 1 || accommodationAbroad == null)
            {
                return NotFound();
            }

            db.AccommodationAbroads.Remove(accommodationAbroad[0]);
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

        private bool AccommodationAbroadExists(int docId,string fromDate,string toDate)
        {
            DateTime AccommodationAbroadFromDate = Utilities.stringToDateTime(fromDate);
            DateTime AccommodationAbroadToDate = Utilities.stringToDateTime(toDate);
            return db.AccommodationAbroads.Count(e => e.DocId == docId && (AccommodationAbroadFromDate.Year == e.FromDate.Year && AccommodationAbroadFromDate.Month == e.FromDate.Month && AccommodationAbroadFromDate.Day == e.FromDate.Day) 
                                                && (AccommodationAbroadToDate.Year == e.FromDate.Year && AccommodationAbroadToDate.Month == e.FromDate.Month && AccommodationAbroadToDate.Day == e.FromDate.Day)
            ) > 0;
        }
    }
}