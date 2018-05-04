using GoWithMe.Areas.Admin.Models;
using GoWithMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace GoWithMe.Controllers
{
    public class CustomerController : Controller
    {
        private GoWithMeDbContext db = new GoWithMeDbContext();
        // trả về tour khách hàng vừa đặt
        public async Task<ActionResult> Index(int? id)
        {
            CartItem listCart = (CartItem)Session["ShoppingCart"];
            if (listCart == null)
            {
                return RedirectToAction("/Home/");
            }
            var ticket = await db.Tickets.Where(t => t.CustomerID == id && t.TourID == listCart.productOrder.ID).ToListAsync();
            return View(ticket); 
        }
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            if(userId != null)
            {
                Customer customer = new Customer { AccountID = userId};
                return View(customer);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccountID,Name,PhoneNumber,Address")] Customer customer)
        {
            CartItem listCart = (CartItem)Session["ShoppingCart"];
            if (listCart == null)
            {
                return RedirectToAction("/Home/");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    var addCustomer = db.Customers.Include(c => c.Tickets).SingleOrDefault(c => c.ID == customer.ID);
                    addCustomer.Tickets.Add(new Ticket { TourID = listCart.productOrder.ID, Date = DateTime.Now, Quantyti = listCart.Quality, Tatus = "Pending"});
                    db.SaveChanges();
                    return RedirectToAction("Index/"+addCustomer.ID);
                }
            }  
            return View(customer);
        }
        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountID,Name,PhoneNumber,Address")] Customer customer)
        {
            CartItem listCart = (CartItem)Session["ShoppingCart"];
            if (listCart == null)
            {
                return RedirectToAction("/Home/");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    var addCustomer = db.Customers.Include(c => c.Tickets).SingleOrDefault(c => c.ID == customer.ID);
                    addCustomer.Tickets.Add(new Ticket { TourID = listCart.productOrder.ID, Date = DateTime.Now, Quantyti = listCart.Quality, Tatus = "Pending" });
                    db.SaveChanges();
                    return RedirectToAction("Index/" + addCustomer.ID);
                }
            }
            return View(customer);
        }

    }
}