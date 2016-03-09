using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jobApp.Context;
using jobApp.Models;
using PagedList;
using jobApp.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace jobApp.Controllers
{
    [Authorize]
    public class JobsController : BaseController
    {
        private JobContext db = new JobContext();
        //private UserManager<ApplicationUser> manager;


        // GET: Jobs
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string category, string Position, string City, int? pageNumber)
        {
            


            var jobs = from s in db.Jobs1
                       select s;


            ViewBag.IT = String.IsNullOrEmpty(category) ? "IT" : "";
            ViewBag.Sales = String.IsNullOrEmpty(category) ? "Sales" : "";
            ViewBag.Banking = String.IsNullOrEmpty(category) ? "Banking" : "";
            ViewBag.Construction = String.IsNullOrEmpty(category) ? "Construction" : "";
            ViewBag.Retail = String.IsNullOrEmpty(category) ? "Retail" : "";
            ViewBag.Marketing = String.IsNullOrEmpty(category) ? "Marketing" : "";

            switch (category)
            {
                case "IT":
                    jobs = jobs.Where(p => p.Category == JobCategory.IT);
                    break;
                case "Sales":
                    jobs = jobs.Where(p => p.Category == JobCategory.Sales);
                    break;
                case "Banking":
                    jobs = jobs.Where(p => p.Category == JobCategory.Banking);
                    break;
                case "Retail":
                    jobs = jobs.Where(p => p.Category == JobCategory.Retail);
                    break;
                case "Construction":
                    jobs = jobs.Where(p => p.Category == JobCategory.Construction);
                    break;
                case "Marketing":
                    jobs = jobs.Where(p => p.Category == JobCategory.Marketing);
                    break;
                default:
                    jobs = jobs.OrderBy(s => s.JobCompanyName);
                    break;
            }


            if (!string.IsNullOrWhiteSpace(Position))
            {
                jobs = jobs.Where(p => p.JobPosition.Contains(Position));
            }
            if (!string.IsNullOrWhiteSpace(City))
            {
                jobs = jobs.Where(p => p.JobCity.Contains(City));
            }
            

            ViewBag.Count = jobs.Count();
            return View(jobs.ToList().ToPagedList(pageNumber ?? 1, 5));
        }
        // GET: Jobs/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs1.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,JobPosition,JobCompanyName,JobCompany,JobDescription,JobRequirements,JobCity,JobSalary,Category")] Job job)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            //var currentUser = manager.FindById(User.Identity.GetUserId());
            
            if (ModelState.IsValid)
            {
                //job.ApplicationUserId = currentUser.Id;
                job.ApplicationUserId = user.Id;
                db.Jobs1.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index","MyJobs");
            }
            return View(job);

        }

        // GET: Jobs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Job job = db.Jobs1.Find(id);
        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(job);
        //}

        //// POST: Jobs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "JobId,JobPosition,JobCompanyName,JobCompany,JobDescription,JobRequirements,JobCity,JobSalary")] Job job)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(job).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(job);
        //}

        //// GET: Jobs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Job job = db.Jobs1.Find(id);
        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(job);
        //}

        //// POST: Jobs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Job job = db.Jobs1.Find(id);
        //    db.Jobs1.Remove(job);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AllowAnonymous]
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");

        }

    }
}
