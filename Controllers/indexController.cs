using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
namespace Chinchillet.Controllers
{
    public class indexController : Controller
    {
        [HttpAcceptedMethods(HttpVerbs.Get)]
        [ActionName("index")]
        public ActionResult get()
        {
            ViewBag.Model = new Item() { Name = "GET", Id = 0 };
            return View();
        }

        [HttpAcceptedMethods(HttpVerbs.Post)]
        [ActionName("index")]
        public ActionResult post(Item item)
        {
            ViewBag.Model = item;
            return View();
        }

        [HttpAcceptedMethods(HttpVerbs.Delete)]
        [ActionName("index")]
        public ActionResult delete(int Id)
        {
            ViewBag.Model = new Item() { Name = "DELETE", Id = Id };
            return View();
        }

        [HttpAcceptedMethods(HttpVerbs.Put)]
        [ActionName("index")]
        public ActionResult put(Item item)
        {
            ViewBag.Model = item;
            return View();
        }
    }
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
