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

        // PUT: api/TestReplacements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestReplacement(string id, TestReplacement testReplacement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testReplacement.CourseName)
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
                if (!TestReplacementExists(id))
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
        public IHttpActionResult PostTestReplacement(TestReplacement testReplacement)
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
                if (TestReplacementExists(testReplacement.CourseName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = testReplacement.CourseName }, testReplacement);
        }

        // DELETE: api/TestReplacements/5
        [ResponseType(typeof(TestReplacement))]
        public IHttpActionResult DeleteTestReplacement(string id)
        {
            TestReplacement testReplacement = db.TestReplacements.Find(id);
            if (testReplacement == null)
            {
                return NotFound();
            }

            db.TestReplacements.Remove(testReplacement);
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

        private bool TestReplacementExists(string id)
        {
            return db.TestReplacements.Count(e => e.CourseName == id) > 0;
        }
    }
}