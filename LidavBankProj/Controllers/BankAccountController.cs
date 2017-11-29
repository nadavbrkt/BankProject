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
    public class BankAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /BankAccount/
        public ActionResult Index()
        {
            return View(db.AccountModel.ToList());
        }

        // GET: /BankAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountmodel = db.AccountModel.Find(id);
            if (accountmodel == null)
            {
                return HttpNotFound();
            }
            return View(accountmodel);
        }

        // GET: /BankAccount/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: /BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AccountId,AccountHolder,CurrentAmount")] AccountModel accountmodel)
        {
            if (ModelState.IsValid)
            {
                db.AccountModel.Add(accountmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountmodel);
        }

        // GET: /BankAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountmodel = db.AccountModel.Find(id);
            if (accountmodel == null)
            {
                return HttpNotFound();
            }
            return View(accountmodel);
        }

        // POST: /BankAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AccountId,AccountHolder,CurrentAmount")] AccountModel accountmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountmodel);
        }

        // GET: /BankAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountmodel = db.AccountModel.Find(id);
            if (accountmodel == null)
            {
                return HttpNotFound();
            }
            return View(accountmodel);
        }

        // POST: /BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountModel accountmodel = db.AccountModel.Find(id);
            db.TransactionModel.RemoveRange(db.TransactionModel.Where(x => x.SrcAccountID == id || x.DstAccountID == id));
            db.AccountModel.Remove(accountmodel);
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
