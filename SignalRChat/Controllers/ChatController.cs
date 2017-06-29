using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChat.Controllers
{
    [_SessionControl]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Chat()
        {
            ViewData["Name"] = Server.UrlDecode(Request.Cookies["NameCookie"].Value) ;
            return View();
           
        }
    }
}