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
using System.Net.Mail;

namespace FastFly.BeckEnd.Controllers
{
    public class ApplyDocumentsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/ApplyDocuments
        public IQueryable<ApplyDocument> GetApplyDocuments()
        {
            return db.ApplyDocuments;
        }

        // GET: api/ApplyDocuments/5
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult GetApplyDocument(int id)
        {
            ApplyDocument applyDocument = db.ApplyDocuments.Find(id);
            if (applyDocument == null)
            {
                return NotFound();
            }

            return Ok(applyDocument);
        }

        [Route("api/ApplyDocuments/{id}/docs")]
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult GetApplyDocument(string id)
        {
            List<ApplyDocument> userDocuments = db.ApplyDocuments.Where(db => db.UserId == id).ToList();
            if (userDocuments.Count > 0)
                return Ok(userDocuments);
            else
                return NotFound();
        }

        // GET: api/ApplyDocuments/5/userid
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult Get(int docId,string userId)
        {
            ApplyDocument applyDocument = db.ApplyDocuments.Where(db => db.DocId == docId && db.UserId == userId).FirstOrDefault();
            if (applyDocument == null)
            {
                return NotFound();
            }

            return Ok(applyDocument);
        }

        // PUT: api/ApplyDocuments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplyDocument(int id, ApplyDocument applyDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applyDocument.DocId)
            {
                return BadRequest();
            }

            db.Entry(applyDocument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                //if headofdepartemt sign the document and approve the trip send mail to all signers.
                if(applyDocument.DepartmentHeadSign != null)
                {
                    var ApproveUsers = db.Users.Where(b => b.Role == "TripApprove");
                    List<string> approveUsers = new List<string>();
                    foreach (User user in ApproveUsers)
                    {
                        approveUsers.Add(user.EmailAddress);
                    }
                    //ApproveUsers.ToArray<string>();
                    SendMail(approveUsers);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplyDocumentExists(id))
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

        // POST: api/ApplyDocuments
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult PostApplyDocument(ApplyDocument applyDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplyDocuments.Add(applyDocument);
            db.SaveChanges();
            //send auto mail to head of department
            //user who send the doc
            var applyUser = db.Users
                    .Where(b => b.Id == applyDocument.UserId)
                    .FirstOrDefault();
            //headofdepartement who send the doc
            var headOfDepUser = db.Users
                    .Where(b => b.Role == "HeadOfDepartment" && b.DepartmentId == applyUser.DepartmentId)
                    .FirstOrDefault();
            List<string> emailAddress = new List<string>() { headOfDepUser.EmailAddress };
            bool messageSucced = SendMail(emailAddress);

            //User temp = db.Users.FirstOrDefault(user => user.Role == "HeadOfDepartment" && user.DepartmentId == applyDocument.User.DepartmentId);
            //====================================
            return CreatedAtRoute("DefaultApi", new { id = applyDocument.DocId }, applyDocument);
        }

        // DELETE: api/ApplyDocuments/5
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult DeleteApplyDocument(int id)
        {
            ApplyDocument applyDocument = db.ApplyDocuments.Find(id);
            if (applyDocument == null)
            {
                return NotFound();
            }

            db.ApplyDocuments.Remove(applyDocument);
            db.SaveChanges();

            return Ok(applyDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplyDocumentExists(int id)
        {
            return db.ApplyDocuments.Count(e => e.DocId == id) > 0;
        }

        private bool SendMail(List<string> emailAddresses)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                foreach(string emailAddress in emailAddresses)
                {
                    mail.From = new MailAddress("shenkarfastfly@gmail.com");
                    mail.To.Add(emailAddress);
                    mail.Subject = "Test Mail";
                    mail.Body = "This is for testing sending mail for head of department";

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("shenkarfastfly", "fastfly123");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                //add logger and write the except ion message to the log
                return false;
            }
        }
    }
}