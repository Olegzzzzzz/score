using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStore.Areas.Access.Providers;
using ToyStore.Models;

namespace ToyStore.Controllers
{
    public class HeaderController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();
        // GET: Header
        public ActionResult HeaderList()
        {
            string[] role;

            CustomRoleProvider userRole = new CustomRoleProvider();
            role = userRole.GetRolesForUser(User.Identity.Name);

            if (role.Length != 0 && "admin" == role[0])
                ViewBag.onAdmin = true;
            else
                ViewBag.onAdmin = false;

            var list = db.TCategories.ToList();

            return PartialView("_HeaderList", list);
        }


        public ActionResult ShowUser()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            ViewBag.ActionResult = result;
            return PartialView("_ShowUser");
        }

        public ActionResult LogOut()
        {
            if (User.Identity.IsAuthenticated)

                ViewBag.LogOut = true;
            else
                ViewBag.LogOut = false;

            return PartialView("_LogOut");
        }
    }
}