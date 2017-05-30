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
    public class TestReplacementsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/TestReplacements
        public IQueryable<TestReplacement> GetTestReplacements()
        {
            return db.TestReplacements;
        }

        // GET: api/TestReplacements/5 - the parameter is the docId number.
        [ResponseType(typeof(TestReplacement))]
        public IHttpActionResult GetTestReplacement(int id)
        {
            List<TestReplacement> testReplacement = db.TestReplacements.Where(d => d.DocId == id).ToList();
            if (testReplacement.Count == 0 || testReplacement == null)
            {
                return NotFound();
            }

            return Ok(testReplacement);
        }

        // PUT: api/TestReplacements/5/computer since/20-20-2006 12:00:00/
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestReplacement(int docId,string courseName,string date, TestReplacement testReplacement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime testDate = Utilities.stringToDateTime(date);

            if (docId != testReplacement.DocId || !courseName.Equals(testReplacement.CourseName) || (testDate.Year != testReplacement.TestDate.Year || testDate.Month != testReplacement.TestDate.Month || testDate.Day != testReplacement.TestDate.Day))
            {
                return BadRequest();
            }

            db.Entry(testReplacement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestReplacementExists(docId, testReplacement.CourseName, date))
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

        // POST: api/TestReplacements
        [ResponseType(typeof(TestReplacement))]
        public IHttpActionResult PostTestReplacement(TestReplacement[] testsReplacement)
        {
            foreach(TestReplacement testReplacement in testsReplacement)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.TestReplacements.Add(testReplacement);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    string month = testReplacement.TestDate.Month.ToString().Length == 1 ? "0" + testReplacement.TestDate.Month.ToString() : testReplacement.TestDate.Month.ToString();
                    string day = testReplacement.TestDate.Day.ToString().Length == 1 ? "0" + testReplacement.TestDate.Day.ToString() : testReplacement.TestDate.Day.ToString();
                    string date = testReplacement.TestDate.Year + month + day;
                    if (TestReplacementExists(testReplacement.DocId, testReplacement.CourseName, date))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(testsReplacement);
        }

        // DELETE: api/TestReplacements/5
        [ResponseType(typeof(TestReplacement))]
        public IHttpActionResult DeleteTestReplacement(int docId, string courseName, string date)
        {
            DateTime testDate = Utilities.stringToDateTime(date);
            List<TestReplacement> testReplacement = db.TestReplacements.Where(e => e.DocId == docId && e.CourseName.Equals(courseName) && (e.TestDate.Year == testDate.Year && e.TestDate.Month == testDate.Month && e.TestDate.Day == testDate.Day)).ToList();
            if (testReplacement.Count != 1 || testReplacement == null)
            {
                return NotFound();
            }

            db.TestReplacements.Remove(testReplacement[0]);
            db.SaveChanges();

            return Ok(testReplacement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestReplacementExists(int docId, string id, string testdate)
        {
            DateTime testDate = Utilities.stringToDateTime(testdate);
            return db.TestReplacements.Count(e => e.CourseName == id && e.DocId == docId && e.TestDate == testDate) > 0;
        }
    }
}