﻿using System;
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
    [Authorize(Roles = "Administrator, Manager")]
    public class NewsController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Place);
            return View(news.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PlaceID,Name,Content,Image")] News news,HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(fileUpload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Image/News"), _FileName);
                    fileUpload.SaveAs(_path);
                    ViewBag.ThongBao = "Đã lưu hình vào thư mục!!";
                    news.Image = fileUpload.FileName;
                    db.News.Add(news);
                    db.SaveChanges();
                }
            }
            catch
            {
                ViewBag.ThongBao = "File upload failed!!";
            }
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", news.PlaceID);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", news.PlaceID);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PlaceID,Name,Content,Image")] News news, HttpPostedFileBase fileUpload)
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
                    news.Image = fileUpload.FileName;
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            catch
            {
                ViewBag.ThongBao = "File upload failed!!";

            }
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", news.PlaceID);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
