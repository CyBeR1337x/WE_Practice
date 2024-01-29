using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace WebApp.Controllers {
    public class CookieController : Controller {
        // GET: Cookie
        public ActionResult AddCookie() {
            return View();
        }
        [HttpPost]
        public ActionResult AddCookie(CookiePref cf) {
            HttpCookie cookie = new HttpCookie("Preferences");
            cookie["Theme"] = cf.Theme;
            cookie["Language"] = cf.FavotireLanguage;
            cookie.Expires = DateTime.Now.AddYears(1); 
    
            Response.Cookies.Add(cookie);
            return RedirectToAction("CookieResult"); 
        }

        [HttpGet]
        public ActionResult CookieResult() {
            return View();
        }
    }
}