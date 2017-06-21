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
using FastFly.BeckEnd.enums;

namespace FastFly.BeckEnd.Controllers
{
    public class ApplyDocumentsController : ApiController
    {
        private FastFlyModelContainer db = new FastFlyModelContainer();

        // GET: api/ApplyDocuments
        public IQueryable<ApplyDocument> GetApplyDocuments()
        {
            db.Configuration.LazyLoadingEnabled = false;
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
            db.Configuration.LazyLoadingEnabled = false;
            List<ApplyDocument> userDocuments = db.ApplyDocuments.Where(db => db.UserId == id).ToList();
            if (userDocuments.Count > 0)
                return Ok(userDocuments);
            else
                return NotFound();
        }

        [Route("api/ApplyDocuments/docs/open")]
        [ResponseType(typeof(ApplyDocument))]
        public IHttpActionResult GetOpenApplyDocument()
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<ApplyDocument> openDocuments = db.ApplyDocuments.Where(db => db.DocStatus == 0).ToList();
            if (openDocuments.Count > 0)
                return Ok(openDocuments);
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
                if(applyDocument.DepartmentHeadSign != null) //need to add check if no one of the signer has any data so its first time)
                {
                    var ApproveUsers = db.Users.Where(b => b.ApplicationRoleId == (int)ApplicationRoles.Signer);
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
            //creating empty ReckoningDocuments with same doc_id and userId
            ReckoningDocument reckoningDocument = new ReckoningDocument() { DocId = applyDocument.DocId, UserId = applyDocument.UserId };
            var result = new ReckoningDocumentsController().PostReckoningDocument(reckoningDocument);

            sendMailToHeadOfDepartment(applyDocument);

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
            //need to delete ReckoningDocuments with same docId as well
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

        private void sendMailToHeadOfDepartment(ApplyDocument applyDocument)
        {
            //send auto mail to head of department of the user who sent the doc
            var applyUser = db.Users
                    .Where(b => b.Id == applyDocument.UserId)
                    .FirstOrDefault();
            var headOfDepUser = db.Users
                    .Where(b => b.Role == "HeadOfDepartment" && b.DepartmentId == applyUser.DepartmentId)
                    .FirstOrDefault();
            if (headOfDepUser != null)
            {
                List<string> emailAddress = new List<string>();
                emailAddress.Add(headOfDepUser.EmailAddress);
                bool messageSucced = SendMail(emailAddress);
                return;
            }
            else
                return;
        }

        private bool SendMail(List<string> emailAddresses)
        {
            try
            {
                
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                foreach(string emailAddress in emailAddresses)
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("shenkarfastfly@gmail.com","Shenkar - FastFly");
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