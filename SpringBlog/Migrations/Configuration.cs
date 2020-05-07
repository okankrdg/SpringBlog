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

            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "karadagokancan@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { 
                    UserName = "karadagokancan@gmail.com",
                    Email= "karadagokancan@gmail.com",
                    DisplayName="Okan Karadað",
                    EmailConfirmed=true
                };

                manager.Create(user, "Password1.");
                manager.AddToRole(user.Id, "admin");
                #region Seed Categories and Posts
                if (!context.Categories.Any())
                {
                    context.Categories.Add(new Category
                    {
                        CategoryName = "Sample Category 1",
                        Posts = new List<Post>
                        {
                            new Post
                            {
                                Title="Sample Post 1 Title",
                                AuthorId=user.Id,
                                Content="<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et tortor ac turpis lobortis tempor sit amet ut orci. Vivamus sed pretium quam, nec mollis libero. Fusce et blandit mauris. Sed congue enim sed odio suscipit, vitae malesuada eros porttitor. Aenean nisl ipsum, molestie porta dolor a, auctor congue libero. Sed mollis quis lectus non venenatis. Curabitur nulla sem, maximus et pellentesque et, interdum id mi. Nullam ut nulla nisi.</p>",
                                Slug="sample-post-1",
                                CreationTime=DateTime.Now,
                                ModifacationTime=DateTime.Now

                            },
                             new Post
                            {
                                Title="Sample Post 2 Title",
                                AuthorId=user.Id,
                                Content="<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus et tortor ac turpis lobortis tempor sit amet ut orci. Vivamus sed pretium quam, nec mollis libero. Fusce et blandit mauris. Sed congue enim sed odio suscipit, vitae malesuada eros porttitor. Aenean nisl ipsum, molestie porta dolor a, auctor congue libero. Sed mollis quis lectus non venenatis. Curabitur nulla sem, maximus et pellentesque et, interdum id mi. Nullam ut nulla nisi.</p>",
                                Slug="sample-post-1",
                                CreationTime=DateTime.Now,
                                ModifacationTime=DateTime.Now

                            }
                        }
                    });
                }
                #endregion

               
            }
            #region add100post
            if (!context.Categories.Any(x => x.CategoryName == "Generated Posts") && context.Users.Any())
            {
                var userId = context.Users.First().Id;
                context.Categories.Add(new Category { CategoryName = "Generated Posts", Posts = GeneratePosts(userId) });

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
