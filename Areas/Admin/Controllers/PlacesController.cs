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
    public class PlacesController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();

        // GET: Admin/Places
        public ActionResult Index()
        {
            return View(db.Places.ToList());
        }

        // GET: Admin/Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Admin/Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,Discription,Image")] Place place, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(fileUpload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Image/Places"), _FileName);
                    //Kiểm tra file đã tồn tại
                    /*if (System.IO.File.Exists(_path))
                    {
                        ViewBag.ThongBao = "Hình ảnh này đã tồn tại";
                    }
                    else
                    {*/
                    fileUpload.SaveAs(_path);
                    ViewBag.ThongBao = "Đã lưu hình vào thư mục!!";
                    place.Image = fileUpload.FileName;
                    db.Places.Add(place);
                    db.SaveChanges();
                    //}

                }
            }

            catch
            {
                ViewBag.ThongBao = "File upload failed!!";

            }
            return View(place);
        }

        // GET: Admin/Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Admin/Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Discription,Image")] Place place, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(fileUpload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Image/Places"), _FileName);
                    //Kiểm tra file đã tồn tại
                    fileUpload.SaveAs(_path);
                    ViewBag.ThongBao = "Đã lưu hình vào thư mục!!";
                    place.Image = fileUpload.FileName;
                    db.Entry(place).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            catch
            {
                ViewBag.ThongBao = "File upload failed!!";

            }
            return View(place);
        }

        // GET: Admin/Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Admin/Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
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
