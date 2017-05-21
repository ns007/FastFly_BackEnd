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
            //LectureReplacement lectureReplacement = db.LectureReplacements.Find(id);
            if (lectureReplacement.Count == 0 ||lectureReplacement == null)
            {
                return NotFound();
            }

            return Ok(lectureReplacement);
        }

        // PUT: api/LectureReplacements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLectureReplacement(string id, LectureReplacement lectureReplacement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lectureReplacement.CourseName)
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
                if (!LectureReplacementExists(id))
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
                    if (LectureReplacementExists(lectureReplacement.CourseName))
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

        // DELETE: api/LectureReplacements/5
        [ResponseType(typeof(LectureReplacement))]
        public IHttpActionResult DeleteLectureReplacement(string id)
        {
            LectureReplacement lectureReplacement = db.LectureReplacements.Find(id);
            if (lectureReplacement == null)
            {
                return NotFound();
            }

            db.LectureReplacements.Remove(lectureReplacement);
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

        private bool LectureReplacementExists(string id)
        {
            return db.LectureReplacements.Count(e => e.CourseName == id) > 0;
        }
    }
}