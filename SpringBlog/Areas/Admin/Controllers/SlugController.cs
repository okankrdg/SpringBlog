using SpringBlog.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class SlugController : Controller
    {
        
        public JsonResult ConverToSlug(string title)
        {
            var data = UrlService.URLFriendly(title);
            return Json(data);
        }
    }
}