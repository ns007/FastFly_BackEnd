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
    public class ReckoningDocumentsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/ReckoningDocuments
        public IQueryable<ReckoningDocument> GetReckoningDocuments()
        {
            return db.ReckoningDocuments;
        }

        // GET: api/ReckoningDocuments/5
        [ResponseType(typeof(ReckoningDocument))]
        public IHttpActionResult GetReckoningDocument(int id)
        {
            ReckoningDocument reckoningDocument = db.ReckoningDocuments.Find(id);
            if (reckoningDocument == null)
            {
                return NotFound();
            }

            return Ok(reckoningDocument);
        }

        // PUT: api/ReckoningDocuments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReckoningDocument(int id, ReckoningDocument reckoningDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reckoningDocument.DocId)
            {
                return BadRequest();
            }

            db.Entry(reckoningDocument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReckoningDocumentExists(id))
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

        // POST: api/ReckoningDocuments
        [ResponseType(typeof(ReckoningDocument))]
        public IHttpActionResult PostReckoningDocument(ReckoningDocument reckoningDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReckoningDocuments.Add(reckoningDocument);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReckoningDocumentExists(reckoningDocument.DocId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reckoningDocument.DocId }, reckoningDocument);
        }

        // DELETE: api/ReckoningDocuments/5
        [ResponseType(typeof(ReckoningDocument))]
        public IHttpActionResult DeleteReckoningDocument(int id)
        {
            ReckoningDocument reckoningDocument = db.ReckoningDocuments.Find(id);
            if (reckoningDocument == null)
            {
                return NotFound();
            }

            db.ReckoningDocuments.Remove(reckoningDocument);
            db.SaveChanges();

            return Ok(reckoningDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReckoningDocumentExists(int id)
        {
            return db.ReckoningDocuments.Count(e => e.DocId == id) > 0;
        }
    }
}