using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToyStore.Areas.Access.Models;

namespace ToyStore.Areas.Access.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                TBuyer user = null;
                using (ToyStoreAccessContext db = new ToyStoreAccessContext())
                {
                    user = db.TBuyers.FirstOrDefault(u => u.Firstname == model.Name && u.Password == model.Password);


                    if (user != null)
                        HttpContext.Response.Cookies["idBuyer"].Value = user.idBuyer.ToString();
                   
                    HttpContext.Response.Cookies["idBuyer"].Expires = DateTime.Now.AddYears(1);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return  Redirect("/Home/index"); 
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                TBuyer user = null;
                using (ToyStoreAccessContext db = new ToyStoreAccessContext())
                {
                    user = db.TBuyers.FirstOrDefault(u => u.Firstname == model.Name);
                    
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (ToyStoreAccessContext db = new ToyStoreAccessContext())
                    {
                        db.TBuyers.Add(new TBuyer { Firstname = model.Name, Password = model.Password, Email = model.Email, C_TRole = 4 });
                        db.SaveChanges();

                        user = db.TBuyers.Where(u => u.Firstname == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        HttpContext.Response.Cookies["idBuyer"].Value = user.idBuyer.ToString();
                        HttpContext.Response.Cookies["idBuyer"].Expires = DateTime.Now.AddYears(1);
                        
                        return Redirect("/Home/index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            HttpContext.Response.Cookies["idBuyer"].Expires = DateTime.Now.AddDays(-1d);

            FormsAuthentication.SignOut();
            return Redirect("/Home/index");
        }
    }
}