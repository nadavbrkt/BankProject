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
using Microsoft.AspNet.Identity;

namespace LidavBankProj.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Transactions/
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var transactionmodel = db.TransactionModel.Include(t => t.DstAccount).Include(t => t.SrcAccount);
                return View("adminIndex", transactionmodel.ToList());
            }
            else
            {
                VInOutTransaction model = new VInOutTransaction();
                var UserId = User.Identity.GetUserId();
                model.outgoing = db.TransactionModel.Where(t => t.SrcAccount.AccountHolder == UserId)
                    .Include(t => t.DstAccount).Include(t => t.SrcAccount).ToList();
                model.incoming = db.TransactionModel.Where(t => t.DstAccount.AccountHolder == UserId)
                    .Include(t => t.DstAccount).Include(t => t.SrcAccount).ToList();
                return View("UserIndex", model);
            }
        }

        // GET: /Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TransactionModel transactionmodel = db.TransactionModel.Find(id);
            if (transactionmodel == null)
            {
                return HttpNotFound();
            }

            if (transactionmodel.DstAccount.ApplicationUser.Id.Equals(User.Identity.GetUserId()) ||
                transactionmodel.SrcAccount.ApplicationUser.Id.Equals(User.Identity.GetUserId()) ||
                User.IsInRole("Admin"))
            {
                return View(transactionmodel);
            }
            ViewBag.Message = "You are not allowed here";
            return View("Error");
        }

        // GET: /Transactions/Create
        public ActionResult Create()
        {
            ViewBag.DstAccountID = new SelectList(db.AccountModel, "AccountId", "AccountId");
            ViewBag.Time = DateTime.Now;
            if (User.IsInRole("Admin"))
            {
                ViewBag.SrcAccountID = new SelectList(db.AccountModel, "AccountId", "AccountId");
                return View("AdminCreate");
            }
            else
            {
                var UserId = User.Identity.GetUserId();
                ViewBag.SrcAccountID = new SelectList(db.AccountModel.Where(a => a.ApplicationUser.Id == UserId)
                    .Select(a => a.AccountId).ToList());
                
                return View();
            }

        }

        // POST: /Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TransactionId,SrcAccountID,DstAccountID,Amount,Time")] TransactionModel transactionmodel)
        {
            if (ModelState.IsValid)
            {
                db.TransactionModel.Add(transactionmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DstAccountID = new SelectList(db.AccountModel, "AccountId", "AccountHolder", transactionmodel.DstAccountID);
            ViewBag.SrcAccountID = new SelectList(db.AccountModel, "AccountId", "AccountHolder", transactionmodel.SrcAccountID);
            return View(transactionmodel);
        }

        // GET: /Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionModel transactionmodel = db.TransactionModel.Find(id);
            if (transactionmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.DstAccountID = new SelectList(db.AccountModel, "AccountId", "AccountHolder", transactionmodel.DstAccountID);
            ViewBag.SrcAccountID = new SelectList(db.AccountModel, "AccountId", "AccountHolder", transactionmodel.SrcAccountID);
            return View(transactionmodel);
        }

        // POST: /Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TransactionId,SrcAccountID,DstAccountID,Amount,Time")] TransactionModel transactionmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DstAccountID = new SelectList(db.AccountModel, "AccountId", "AccountHolder", transactionmodel.DstAccountID);
            ViewBag.SrcAccountID = new SelectList(db.AccountModel, "AccountId", "AccountHolder", transactionmodel.SrcAccountID);
            return View(transactionmodel);
        }

        // GET: /Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionModel transactionmodel = db.TransactionModel.Find(id);
            if (transactionmodel == null)
            {
                return HttpNotFound();
            }
            return View(transactionmodel);
        }

        // POST: /Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionModel transactionmodel = db.TransactionModel.Find(id);
            db.TransactionModel.Remove(transactionmodel);
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
