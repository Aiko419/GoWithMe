using GoWithMe.Areas.Admin.Models;
using GoWithMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GoWithMe.Controllers
{
    public class HomeController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();
        public ActionResult Index()
        {

            return View(db.Places.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult News(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var place = db.Places.Include(p => p.News).SingleOrDefault<Place>(p => p.ID == id);
            return View(place);
        }

        public ActionResult TourDetail(int? id)
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
        public ActionResult PlaceDetail(int? id)
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

        public ActionResult Tour()
        {
            return View(db.Tours.ToList());
        }

        //Tiến trình thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public JsonResult AddToCart(int id)
        {
            //Process Add To Cart
            List<CartItem> listCartItem;
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session 
                listCartItem = new List<CartItem>();
                listCartItem.Add(new CartItem { Quality = 1, productOrder = db.Tours.Find(id) });
                Session["ShoppingCart"] = listCartItem;
            }
            else
            {
                bool flag = false;
                listCartItem = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in listCartItem)
                {
                    if (item.productOrder.ID == id)
                    {
                        item.Quality++; flag = true;
                        break;
                    }
                }

                if (!flag)
                    listCartItem.Add(new CartItem { Quality = 1, productOrder = db.Tours.Find(id) });

                Session["ShoppingCart"] = listCartItem;
            }
            //Count item in shopping cart 
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            foreach (CartItem item in ls)
            {
                cartcount += item.Quality;
            }
            return Json(new { ItemAmount = cartcount });
        }

    }
}