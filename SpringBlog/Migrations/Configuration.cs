namespace SpringBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SpringBlog.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SpringBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SpringBlog.Models.ApplicationDbContext context)
        {
            //https://stackoverflow.com/questions/19280527/mvc-5-seed-users-and-roles

           
            #region add100post
            if (!context.Categories.Any(x => x.CategoryName == "Generated Posts") && context.Users.Any())
            {
                var userId = context.Users.First().Id;
                context.Categories.Add(new Category { CategoryName = "Generated Posts",Slug="generated-posts-1" ,Posts = GeneratePosts(userId) });

            }
            #endregion
        }
        private List<Post> GeneratePosts(string userId, int count = 100)
        {
            var posts = new List<Post>();

            for (int i = 0; i < count; i++)
            {
                posts.Add(new Post
                {
                    Title = "Generated Post " + (i + 1),
                    AuthorId = userId,
                    Content = "<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>",
                    Slug = "generated-post-" + (i + 1),
                    CreationTime = DateTime.Now,
                    ModifacationTime = DateTime.Now
                });
            }

            return posts;
        }
    }
}
