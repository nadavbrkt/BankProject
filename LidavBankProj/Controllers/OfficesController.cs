using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LidavBankProj.Models;
using LidavBankProj.DAL;

namespace LidavBankProj.Controllers
{
    public class OfficesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Offices/
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View("AdminIndex", db.OfficeModel.ToList());
            }
            else
            {
                return View(db.OfficeModel.ToList());
            }
            
        }

        // GET: /Offices/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeModel officemodel = db.OfficeModel.Find(id);
            if (officemodel == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Admin"))
            {
                return View("AdminDetails",officemodel);
            }
            else
            {
                return View(officemodel);
            }
        }

        // GET: /Offices/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                ViewBag.Message = "You Are Not Allowed Here!";
                return View("Error");
            }
        }

        // POST: /Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,City,Street,House,OpeningHours,ClosingHours,Phone")] OfficeModel officemodel)
        {
            if (ModelState.IsValid)
            {
                db.OfficeModel.Add(officemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(officemodel);
        }

        // GET: /Offices/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OfficeModel officemodel = db.OfficeModel.Find(id);
                if (officemodel == null)
                {
                    return HttpNotFound();
                }
                return View(officemodel);
            }
            else
            {
                ViewBag.Message = "You Are Not Allowed Here!";
                return View("Error");
            }
        }

        // POST: /Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,City,Street,House,OpeningHours,ClosingHours,Phone")] OfficeModel officemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(officemodel);
        }

        // GET: /Offices/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OfficeModel officemodel = db.OfficeModel.Find(id);
                if (officemodel == null)
                {
                    return HttpNotFound();
                }
                return View(officemodel);
            }
            else
            {
                ViewBag.Message = "You Are Not Allowed Here!";
                return View("Error");
            }
        }

        // POST: /Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfficeModel officemodel = db.OfficeModel.Find(id);
            db.OfficeModel.Remove(officemodel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
