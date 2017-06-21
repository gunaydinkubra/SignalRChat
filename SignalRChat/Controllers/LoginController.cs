using SignalRChat.Db;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChat.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel data)
        {
            var db = new DbSet();
            var user = db.GetUser(data.UserName);
            if(user != null && user.Pass == data.Pass)
            {
                return Redirect("/Chat/Chat");
            }
            else
            {
                ViewData["Warning"] = $"{data.UserName} is not found.";
                return View();
            }
           

        }
    }
}