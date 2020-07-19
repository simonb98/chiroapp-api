using chiroapp_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace chiroapp_api.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Post> _posts;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _posts = dbContext.Posts;
        }

        IEnumerable<Post> IPostRepository.GetAll()
        {
            return _posts.Include(p => p.Comments).ToList();
        }

        Post IPostRepository.GetBy(int id)
        {
            return _posts.Include(p => p.Comments).SingleOrDefault(p => p.Id == id);
        }

        bool IPostRepository.TryGetPost(int id, out Post post)
        {
            post = _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            return post != null;
        }

        IEnumerable<Post> IPostRepository.GetBy(string title = null, string group = null)
        {
            var posts = _posts.Include(p => p.Comments).AsQueryable();
            if (!string.IsNullOrEmpty(title))
                posts = posts.Where(p => p.Title.IndexOf(title) >= 0);
            if (!string.IsNullOrEmpty(group))
                posts = posts.Where(p => p.Group.IndexOf(group) >= 0);
            return posts.OrderBy(p => p.Title).ToList();
        }

        void IPostRepository.Add(Post post)
        {
            _posts.Add(post);
        }

        void IPostRepository.Delete(Post post)
        {
            _posts.Remove(post);
        }

        void IPostRepository.Update(Post post)
        {
            _context.Update(post);
        }

        void IPostRepository.SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
