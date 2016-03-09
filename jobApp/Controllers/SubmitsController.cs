using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using jobApp.Context;
using jobApp.Models;
using System.IO;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;
using PagedList;

namespace jobApp.Controllers
{
    public class SubmitsController : BaseController
    {
        private JobContext db = new JobContext();

        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        // GET: Submits
        [Authorize]
        public ActionResult Index(string Position)
        {

            var jobs1 = db.Jobs1.Include(j => j.ApplicationUser);
            var submit = db.Submit.Include(s => s.Job);

            if (!string.IsNullOrWhiteSpace(Position))
            {
                submit = submit.Where(p => p.Job.JobPosition.Contains(Position));
            }
            ViewBag.Count = submit.Count();
            return PartialView(submit.ToList().Where(j => j.Job.ApplicationUserId == user.Id));
        }

        public ActionResult Downloads(Job job)
        {
            var dir = new DirectoryInfo(Server.MapPath("~/UploadedCV/"));
            FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();

            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }


            return View(items.ToList());
        }
        public FileResult Download(string ImageName)
        {
            //return File("~/UploadedCV/" +ImageName, "image/pdf");
            return File("~/UploadedCV/" + ImageName, "application/pdf");
            //return new FileStreamResult(new FileStream("/UploadedCV/" + ImageName, FileMode.Open), "image/pdf");
        }
        //public ActionResult Details(int id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //Submit submit = await db.Submit.FindAsync(id);
        //    //if (submit == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    var submit = new Submit() { jobid = id };
        //    return View(submit);
        //}

        // GET: Submits/Create

        public ActionResult Create(int id)
        {

            var submit = new Submit() { jobid = id };
            return View(submit);
        }



        // POST: Submits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubmitId,fullName,submitEmail,CV,jobid")] Submit submit, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedCV"), fileName));
                    //  using (JobContext dc = new JobContext())
                    // {

                    submit.CV = fileName;
                    db.Submit.Add(submit);
                    await db.SaveChangesAsync();
                    //}

                    ModelState.Clear();
                    submit = null;
                    ViewBag.Message = "Successfully Done";
                    return RedirectToAction("Index", "Jobs");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error! Please try again";
                    return View();
                }

            }

            //ViewBag.jobid = new SelectList(db.Jobs1, "JobId", "ApplicationUserId", submit.jobid);
            //ViewBag.jobid = new SelectList(db.Jobs1, "JobId", "JobId");
            return View(submit);
        }

        // GET: Submits/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //Submit submit = await db.Submit.FindAsync(id);
        //    //if (submit == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //ViewBag.jobid = new SelectList(db.Jobs1, "JobId", "ApplicationUserId", submit.jobid);
        //    var submit = new Submit() { jobid = id };
        //    return View(submit);
        //}

        // POST: Submits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "SubmitId,fullName,submitEmail,CV,jobid")] Submit submit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(submit).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.jobid = new SelectList(db.Jobs1, "JobId", "ApplicationUserId", submit.jobid);
        //    return View(submit);
        //}

        // GET: Submits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submit submit = db.Submit.Find(id);
            if (submit == null)
            {
                return HttpNotFound();
            }
            //var submit = new Submit() { jobid = id };
            return View(submit);
        }

        // POST: Submits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string CV)
        {
            Submit submit = await db.Submit.FindAsync(id);
            var CVName = "";
            CVName = submit.CV;
            string fullPath = Request.MapPath("~/UploadedCV/" + CVName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }


            db.Submit.Remove(submit);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "MyJobs");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
