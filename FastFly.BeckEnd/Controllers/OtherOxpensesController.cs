﻿using System;
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
    public class OtherOxpensesController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/OtherOxpenses
        public IQueryable<OtherOxpense> GetOtherOxpenses()
        {
            return db.OtherOxpenses;
        }

        // GET: api/OtherOxpenses/5 the parameter is the reckoning document id 
        [ResponseType(typeof(OtherOxpense))]
        public IHttpActionResult GetOtherOxpense(int id)
        {
            List<OtherOxpense> otherOxpense = db.OtherOxpenses.Where(d => d.DocId == id).ToList();
            if (otherOxpense.Count == 0 || otherOxpense == null)
            {
                return NotFound();
            }

            return Ok(otherOxpense);
        }

        // PUT: api/OtherOxpenses/5/expenseEssence
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOtherOxpense(int id, string expenseEssence, OtherOxpense otherOxpense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != otherOxpense.DocId || !expenseEssence.Equals(otherOxpense.ExpenseEssence))
            {
                return BadRequest();
            }

            db.Entry(otherOxpense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtherOxpenseExists(id,otherOxpense.ExpenseEssence))
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

        // POST: api/OtherOxpenses
        [ResponseType(typeof(OtherOxpense))]
        public IHttpActionResult PostOtherOxpense(OtherOxpense[] otherOxpenses)
        {
            foreach(OtherOxpense otherOxpense in otherOxpenses)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.OtherOxpenses.Add(otherOxpense);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (OtherOxpenseExists(otherOxpense.DocId,otherOxpense.ExpenseEssence))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(otherOxpenses);
        }

        // DELETE: api/OtherOxpenses/5
        [ResponseType(typeof(OtherOxpense))]
        public IHttpActionResult DeleteOtherOxpense(int id)
        {
            OtherOxpense otherOxpense = db.OtherOxpenses.Find(id);
            if (otherOxpense == null)
            {
                return NotFound();
            }

            db.OtherOxpenses.Remove(otherOxpense);
            db.SaveChanges();

            return Ok(otherOxpense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OtherOxpenseExists(int id,string expenseEssence)
        {
            return db.OtherOxpenses.Count(e => e.DocId == id && e.ExpenseEssence == expenseEssence) > 0;
        }
    }
}