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
    public class StayExpensesController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/StayExpenses
        public IQueryable<StayExpense> GetStayExpenses()
        {
            return db.StayExpenses;
        }

        // GET: api/StayExpenses/5 - the parameter is the reckoningDocument id
        [ResponseType(typeof(StayExpense))]
        public IHttpActionResult GetStayExpense(int id)
        {
            List<StayExpense> stayExpense = db.StayExpenses.Where(d => d.DocId == id).ToList();
            if (stayExpense.Count == 0 || stayExpense == null)
            {
                return NotFound();
            }

            return Ok(stayExpense);
        }

        // PUT: api/StayExpenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStayExpense(int id, StayExpense stayExpense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stayExpense.DocId)
            {
                return BadRequest();
            }

            db.Entry(stayExpense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StayExpenseExists(id,stayExpense.FromDate,stayExpense.ToDate))
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

        // POST: api/StayExpenses
        [ResponseType(typeof(StayExpense))]
        public IHttpActionResult PostStayExpense(StayExpense[] stayExpenses)
        {
            foreach(StayExpense stayExpense in stayExpenses)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.StayExpenses.Add(stayExpense);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (StayExpenseExists(stayExpense.DocId,stayExpense.FromDate,stayExpense.ToDate))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(stayExpenses);
        }

        // DELETE: api/StayExpenses/5
        [ResponseType(typeof(StayExpense))]
        public IHttpActionResult DeleteStayExpense(int id)
        {
            StayExpense stayExpense = db.StayExpenses.Find(id);
            if (stayExpense == null)
            {
                return NotFound();
            }

            db.StayExpenses.Remove(stayExpense);
            db.SaveChanges();

            return Ok(stayExpense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StayExpenseExists(int id,DateTime fromdate,DateTime todate)
        {
            return db.StayExpenses.Count(e => e.DocId == id && e.FromDate == fromdate && e.ToDate == todate) > 0;
        }
    }
}