using chiroapp_api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chiroapp_api.Data
{
    public class ApplicationDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationDataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                var posts = new List<Post>
                {
                    new Post("Zee","Zoals jullie wel weten gaan we 27 juni naar de zee.","Aspiranten"),
                    new Post("Plopsaland","Joepie de speelclub gaat naar plopsaland.","Speelclub"),
                    new Post("Paintballen","23 mei gaan we paintballen, neem €15 mee.","Toppers")
                };
                _dbContext.Posts.AddRange(posts);
                _dbContext.SaveChanges();
            }
        }
    }
}
