using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoWithMe.Areas.Admin.Models;

namespace GoWithMe.Areas.Admin.Controllers
{

    public class TicketsController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();

        // GET: Admin/Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Customer).Include(t => t.Tour);
            return View(tickets.ToList());
        }

        // GET: Admin/Tickets/Details/5
        public ActionResult Details(int? id, int? id1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Ticket ticket = db.Tickets.Find(id);
            Ticket ticket = db.Tickets.SingleOrDefault(t =>t.CustomerID == id && t.TourID == id1);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Admin/Tickets/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "AccountID");
            ViewBag.TourID = new SelectList(db.Tours, "ID", "Name");
            return View();
        }

        // POST: Admin/Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TourID,CustomerID,Date,Quantyti,Tatus")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "AccountID", ticket.CustomerID);
            ViewBag.TourID = new SelectList(db.Tours, "ID", "Name", ticket.TourID);
            return View(ticket);
        }

        // GET: Admin/Tickets/Edit/5
        public ActionResult Edit(int? id, int? id1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.SingleOrDefault(t => t.CustomerID == id && t.TourID == id1);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "AccountID", ticket.CustomerID);
            ViewBag.TourID = new SelectList(db.Tours, "ID", "Name", ticket.TourID);
            return View(ticket);
        }

        // POST: Admin/Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TourID,CustomerID,Date,Quantyti,Tatus")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "AccountID", ticket.CustomerID);
            ViewBag.TourID = new SelectList(db.Tours, "ID", "Name", ticket.TourID);
            return View(ticket);
        }

        // GET: Admin/Tickets/Delete/5
        public ActionResult Delete(int? id, int? id1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.SingleOrDefault(t => t.CustomerID == id && t.TourID == id1);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? id1)
        {
            Ticket ticket = db.Tickets.SingleOrDefault(t => t.CustomerID == id && t.TourID == id1);
            db.Tickets.Remove(ticket);
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
