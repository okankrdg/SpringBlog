using SpringBlog.Helpers;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult New()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Category has been created successgully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var model = db.Categories.Find(id);
            if (model==null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                TempData["SuccessMessage"] = "The Category has been updated successgully";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var model = db.Categories.Find(category.Id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var model = db.Categories.Find(id);
            if (model==null)
            {
                return HttpNotFound();
            }
            if (model.Posts.Count > 0)
            {
                TempData["ErrorMessage"] = "You cannot delete a category which contains posts.";
                return RedirectToAction("Index");
            }
            db.Categories.Remove(model);
            db.SaveChanges();
            TempData["SuccessMessage"] = "The Category has been deleted successgully";
            return RedirectToAction("Index");
        }
       
    }
}