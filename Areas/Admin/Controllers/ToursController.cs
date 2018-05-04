using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoWithMe.Areas.Admin.Models;

namespace GoWithMe.Areas.Admin.Controllers
{
    public class ToursController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();

        // GET: Admin/Tours
        public ActionResult Index()
        {
            return View(db.Tours.ToList());
        }

        // GET: Admin/Tours/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/Tours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Quantyti,Price,Discription,StartDay,Duration,Image")] Tour tour, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(fileUpload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Image/Tours"), _FileName);
                    fileUpload.SaveAs(_path);
                    ViewBag.ThongBao = "Đã lưu hình vào thư mục!!";
                    tour.Image = fileUpload.FileName;
                    db.Tours.Add(tour);
                    db.SaveChanges();
                }
            }
            catch
            {
                ViewBag.ThongBao = "File upload failed!!";
            }

            return View(tour);
        }

        // GET: Admin/Tours/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Quantyti,Price,Discription,StartDay,Duration,Image")] Tour tour, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(fileUpload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Image/News"), _FileName);
                    //Kiểm tra file đã tồn tại
                    fileUpload.SaveAs(_path);
                    ViewBag.ThongBao = "Đã lưu hình vào thư mục!!";
                    tour.Image = fileUpload.FileName;
                    db.Entry(tour).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            catch
            {
                ViewBag.ThongBao = "File upload failed!!";

            }
            return View(tour);
        }

        // GET: Admin/Tours/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
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
