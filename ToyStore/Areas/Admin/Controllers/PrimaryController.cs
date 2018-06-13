using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToyStore.Areas.Admin.Controllers
{
    public class PrimaryController : Controller
    {
        // GET: Admin/Primary
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}