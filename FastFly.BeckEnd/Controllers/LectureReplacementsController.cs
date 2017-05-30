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
    public class LectureReplacementsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/LectureReplacements
        public IQueryable<LectureReplacement> GetLectureReplacements()
        {
            return db.LectureReplacements;
        }

        // GET: api/LectureReplacements/5
        [ResponseType(typeof(LectureReplacement))]
        public IHttpActionResult GetLectureReplacement(int id)
        {
            List<LectureReplacement> lectureReplacement = db.LectureReplacements.Where(d => d.DocId == id).ToList();
            if (lectureReplacement.Count == 0 ||lectureReplacement == null)
            {
                return NotFound();
            }

            return Ok(lectureReplacement);
        }

        //PUT: api/LectureReplacements/5/CourseName/DateAsString/TimeAsString
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLectureReplacement(int docId,string courseName, string date, string fromHour,  LectureReplacement lectureReplacement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime lectureDate = Utilities.stringToDateTime(date);
            TimeSpan lectureTime = Utilities.stringToTimeSpan(fromHour);

            if (docId != lectureReplacement.DocId || !courseName.Equals(lectureReplacement.CourseName) || (lectureDate.Year != lectureReplacement.Date.Year || lectureDate.Month != lectureReplacement.Date.Month || lectureDate.Day != lectureReplacement.Date.Day) || lectureTime != lectureReplacement.FromHour)
            {
                return BadRequest();
            }

            db.Entry(lectureReplacement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectureReplacementExists(docId,courseName, date, fromHour))
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

        // POST: api/LectureReplacements
        [ResponseType(typeof(LectureReplacement))]
        public IHttpActionResult PostLectureReplacement(LectureReplacement[] lecturesReplacement)
        {
            foreach(LectureReplacement lectureReplacement in lecturesReplacement)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.LectureReplacements.Add(lectureReplacement);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    string month = lectureReplacement.Date.Month.ToString().Length == 1 ? "0" + lectureReplacement.Date.Month.ToString() : lectureReplacement.Date.Month.ToString();
                    string day = lectureReplacement.Date.Day.ToString().Length == 1 ? "0" + lectureReplacement.Date.Day.ToString() : lectureReplacement.Date.Day.ToString();
                    string hours = lectureReplacement.FromHour.Hours.ToString().Length == 1 ? "0" + lectureReplacement.FromHour.Hours.ToString() : lectureReplacement.FromHour.Hours.ToString();
                    string minutes = lectureReplacement.FromHour.Minutes.ToString().Length == 1 ? "0" + lectureReplacement.FromHour.Minutes.ToString() : lectureReplacement.FromHour.Minutes.ToString();
                    string date = lectureReplacement.Date.Year.ToString() + month + day;
                    string time = hours + minutes;
                    if (LectureReplacementExists(lectureReplacement.DocId,lectureReplacement.CourseName,date, time))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return Ok(lecturesReplacement);
        }

        // DELETE: api/LectureReplacements/5/CourseName/DateAsString/TimeAsString
        [ResponseType(typeof(LectureReplacement))]
        public IHttpActionResult DeleteLectureReplacement(int docId, string courseName, string date, string fromHour)
        {
            DateTime lectureDate = Utilities.stringToDateTime(date);
            TimeSpan lectureTime = Utilities.stringToTimeSpan(fromHour);
            List<LectureReplacement> lectureReplacement = db.LectureReplacements.Where(e => e.DocId == docId && e.CourseName.Equals(courseName) && (e.Date.Year == lectureDate.Year && e.Date.Month == lectureDate.Month && e.Date.Day == lectureDate.Day) && e.FromHour == lectureTime).ToList();
            if (lectureReplacement.Count != 1 || lectureReplacement == null)
            {
                return NotFound();
            }

            db.LectureReplacements.Remove(lectureReplacement[0]);
            db.SaveChanges();

            return Ok(lectureReplacement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LectureReplacementExists(int docId, string courseName, string date, string fromHour)
        {
            DateTime lectureDate = Utilities.stringToDateTime(date);
            TimeSpan lectureTime = Utilities.stringToTimeSpan(fromHour);
            return db.LectureReplacements.Count(e => e.DocId == docId && e.CourseName.Equals(courseName) && (e.Date.Year == lectureDate.Year && e.Date.Month == lectureDate.Month && e.Date.Day == lectureDate.Day) && e.FromHour == lectureTime) > 0;
        }
    }
}