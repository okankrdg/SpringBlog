using SpringBlog.Models;
using SpringBlog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace SpringBlog.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? cid,string q,int page=1)
        {
            int pageSize = 10;

            IQueryable<Post> posts = db.Posts;
            Category category = null;
            string searchResult = null;
            if (cid != null)
            {
                category = db.Categories.Find(cid);
                if (category==null)
                {
                    return HttpNotFound();
                }
                posts = posts.Where(x => x.CategoryId == cid);
                
            };
            if (q != null)
            {
                q = q.Trim();
                posts = posts.Where(x => x.Content.Contains(q) || x.Title.Contains(q));
                searchResult = q;
            }
            var vm = new HomeIndexViewModel
            {
                Posts = posts.OrderByDescending(x => x.CreationTime).ToPagedList(page,pageSize),
                SelectedCategory = category,
                SearchResult=searchResult,
                CategoryID=cid
            };
            
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CategoriesPartial()
        {
            var cats = db.Categories.OrderBy(x => x.CategoryName).ToList();
            return PartialView("_CategoriesPartial",cats);
        }
    }
}