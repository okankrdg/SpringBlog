using Microsoft.AspNet.Identity;
using SpringBlog.Areas.Admin.ViewModels;
using SpringBlog.Helpers;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }
        public ActionResult New()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult New(NewPostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = vm.Title,
                    Content = vm.Content,
                    CategoryId = vm.CategoryId,
                    AuthorId = User.Identity.GetUserId(),
                    CreationTime = DateTime.Now,
                    ModifacationTime = DateTime.Now,
                    Slug = UrlService.URLFriendly(vm.Title),
                    PhotoPath = ""
                };
                db.Posts.Add(post);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Post has been deleted successgully";
                //todo : Post/ındexe Yonlendir
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var model = db.Posts.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Posts.Remove(model);
            db.SaveChanges();
            TempData["SuccessMessage"] = "The Post has been deleted successgully";
            return RedirectToAction("Index");
        }
    }
}