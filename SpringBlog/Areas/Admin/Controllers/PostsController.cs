using Microsoft.AspNet.Identity;
using SpringBlog.Areas.Admin.ViewModels;
using SpringBlog.Helpers;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    Slug = UrlService.URLFriendly(vm.Slug),
                    PhotoPath = this.SaveImage(vm.FeaturedImage)
                };
                db.Posts.Add(post);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Post has been deleted successgully";
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
            ImageUtiltes.DeleteImage(this,model.PhotoPath);
            db.Posts.Remove(model);
            db.SaveChanges();
            TempData["SuccessMessage"] = "The Post has been deleted successgully";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var article = db.Posts.Find(id);
            if (article==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditPostViewModel vm = new EditPostViewModel
            {
                CategoryId = article.CategoryId,
                Content = article.Content,
                CreationTime = article.CreationTime.Value,
                CurrentFeaturedImageName = article.PhotoPath,
                ModifacationTime = article.ModifacationTime.Value,
                Id = article.Id,
                Slug = article.Slug,
                Title = article.Title
            };
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View(vm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var post = db.Posts.Find(vm.Id);
                post.CategoryId = vm.CategoryId;
                post.Content = vm.Content;
                post.ModifacationTime = DateTime.Now;
                post.Slug = UrlService.URLFriendly(vm.Slug);
                post.Title = vm.Title;
                if (vm.FeaturedImage != null)
                {
                    this.DeleteImage(post.PhotoPath);
                    post.PhotoPath = this.SaveImage(vm.FeaturedImage);
                }
                TempData["SuccessMessage"] = "The Post has been updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View(vm);
        }
        [HttpPost]
        public JsonResult ConverToSlug(string title)
        {
            var data= UrlService.URLFriendly(title);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}