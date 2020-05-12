using SpringBlog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.OrderByDescending(x=>x.CreationTime).ToList());
        }
        [HttpPost]
        public ActionResult StateChange(int id,bool isPublished)
        {

            var comment = db.Comments.Find(id);
            if (comment == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            comment.State = isPublished ? CommentState.Approved : CommentState.Rejected;
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment= db.Comments.Find(id);
            db.Comments.RemoveRange(comment.Children);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}