using GoWithMe.Areas.Admin.Models;
using GoWithMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace GoWithMe.Controllers
{
    public class HomeController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();
        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();
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
        /*
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
        public JsonResult DeleteCartItem(int? id)
        {
            List<CartItem> listCartItem, listDeleteCartItem;
            listDeleteCartItem = new List<CartItem>();
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session 
                listCartItem = new List<CartItem>();
                Session["ShoppingCart"] = listCartItem;
                
            }
            else
            {
                listCartItem = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in listCartItem)
                {
                    if (item.productOrder.ID != id)
                    {
                        listDeleteCartItem.Add(item);
                    }
                }
            }
            Session["ShoppingCart"] = listDeleteCartItem;
            ViewData["count"] = listDeleteCartItem.Count;
            return Json(new { ItemAmount = listDeleteCartItem.Count });
        }

        public RedirectToRouteResult UpdateCartItem(int id, int number)
        {
            //Process Add To Cart
            List<CartItem> listCartItem;
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session 
                listCartItem = new List<CartItem>();
                listCartItem.Add(new CartItem { Quality = number, productOrder = db.Tours.Find(id) });
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
                        item.Quality = number;
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    listCartItem.Add(new CartItem { Quality = number, productOrder = db.Tours.Find(id) });
                Session["ShoppingCart"] = listCartItem;
            }

            //Count item in shopping cart 
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            foreach (CartItem item in ls)
            {
                cartcount += item.Quality;
            }

            return RedirectToAction("Index");
        }*/
        [HttpPost]
        public RedirectResult  AddSessionTour(int id, int number)
        {
            Session["ShoppingCart"] = new CartItem { Quality = number, productOrder = db.Tours.Find(id) };
            var userId = User.Identity.GetUserId();

            if (userId == null)  // trường hợp nếu chưa đăng nhập
            {
                return Redirect("/Customer/Create"); // tạo  thông tin user
            }
            else   // trường hợp đăng nhập rồi 
            {
                var customer = db.Customers.SingleOrDefault(c => c.AccountID == userId);
                if (customer == null)
                  {
                     return Redirect("/Customer/Create");
                  }
                else
                {
                    var ticket = db.Tickets.SingleOrDefault(t => t.CustomerID == customer.ID && t.TourID == id);
                    if (ticket != null)
                    {
                        Session["ThongBao"] = "Tour này đã được đặt!";
                        return Redirect("/Home/TourDetail/" + id);
                    }
                    return Redirect("/Customer/Edit/" + customer.ID);
                }
                
                
            }
        }
           
    }
}