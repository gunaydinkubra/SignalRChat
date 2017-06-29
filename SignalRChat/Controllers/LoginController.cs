using SignalRChat.Db;
using SignalRChat.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SignalRChat.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
                FormsAuthentication.SignOut();
                return View();
            
        }
        [HttpPost]
        public ActionResult Login(LoginModel data)
        {
            var db = new DbSet();
            var user = db.GetUser(data.UserName);
            if(user != null && user.Pass == data.Pass)
            {
                FormsAuthentication.SetAuthCookie(data.UserName, true);
                HttpCookie name = new HttpCookie("NameCookie");
                name.Value = Server.UrlEncode($"{ user.Name} {user.Surname}");
                Response.Cookies.Add(name);

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