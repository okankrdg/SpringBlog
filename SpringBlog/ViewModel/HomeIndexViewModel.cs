using X.PagedList;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpringBlog.ViewModel
{
    public class HomeIndexViewModel
    {
        public IPagedList<Post> Posts { get; set; }
        public Category SelectedCategory { get; set; }
        public string SearchResult { get; set; }
        public int? CategoryID { get; set; }
    }
}