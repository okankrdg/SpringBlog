using SpringBlog.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.ViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        [Display(Name ="Short Url")]
        [StringLength(200)]
        public string Slug { get; set; }
        [PostImage]
        public HttpPostedFileBase FeaturedImage { get; set; }
        [Display(Name ="Created Time")]
        public DateTime CreationTime { get; set; }
        [Display(Name = "Last Update Time")]
        public DateTime ModifacationTime { get; set; }

        public string CurrentFeaturedImageName { get; set; }
        
    }
}