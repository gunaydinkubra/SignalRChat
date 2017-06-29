
using SignalRChat.Db;
using SignalRChat.Models;
using System.Web.Mvc;

namespace SignalRChat.Controllers
{
    public class UserController : Controller
    {
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(UsersModel data)
        {
            if (!string.IsNullOrEmpty(data.Name))
            {
                if (!string.IsNullOrEmpty(data.Surname))
                {
                    if (!string.IsNullOrEmpty(data.UserName))
                    {
                        if (!string.IsNullOrEmpty(data.Pass))
                        {
                            var db = new DbSet();
                            var userModel = db.GetUser(data.UserName);
                            if (userModel != null)
                            {
                                ViewData["Warning"] = $"{data.UserName} is available in our system.";
                            }
                            else
                            {
                                db.Add(data);
                                ModelState.Clear();
                                ViewData["Success"] = $"Successfull! Your information is saved. ";

                                return View();
                            }

                        }
                        else
                        {
                            ViewData["Warning"] = "You must enter your password";
                        }
                        
                    }
                    else
                    {
                        ViewData["Warning"] = "You must enter your username";
                    }
                    
                }
                else
                {
                    ViewData["Warning"] = "You must enter your surname";
                }
                
            }
            else
            {
                ViewData["Warning"] = "You must enter your name";
            }
            

            return View(data);

        }
    }
}