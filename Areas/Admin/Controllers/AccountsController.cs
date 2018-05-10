using GoWithMe.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoWithMe.Areas.Admin.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AccountsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();

        public AccountsController()
        {
        }

        public AccountsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Accounts
        public async Task<ActionResult> Index()
        {
            var users = await applicationDbContext.Users.Include(u => u.Roles).ToArrayAsync();
            return View(users);
        }

        // GET: Admin/Accounts/AddToRole/5
        public async Task<ActionResult> AddToRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = new SelectList(applicationDbContext.Roles, "Name", "Name");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToRole(string id, string Role)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var result = await UserManager.AddToRoleAsync(user.Id, Role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Accounts");
            }
            ViewBag.ThongBao = "Không thể thêm quyền này";
            ViewBag.Role = new SelectList(applicationDbContext.Roles, "Name", "Name");
            return View(user);
        }

        // GET: Admin/Accounts/AddToRole/5
        public async Task<ActionResult> RemoveFromRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = new SelectList(applicationDbContext.Roles, "Name", "Name");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveFromRole(string id, string Role)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var result = await UserManager.RemoveFromRoleAsync(user.Id, Role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Accounts");
            }
            ViewBag.ThongBao = "Không thể xóa quyền này";
            ViewBag.Role = new SelectList(applicationDbContext.Roles, "Name", "Name");
            return View(user);
        }

        // GET: Admin/Products/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
    }
}