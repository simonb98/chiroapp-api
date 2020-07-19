using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace chiroapp_api.Models
{
    public class Post
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Group { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Comment> Comments { get; private set; }
        #endregion

        public Post()
        {
            Comments = new List<Comment>();
            Created = DateTime.Now;
        }

        public Post(string title, string content, string group) : this()
        {
            Title = title;
            Content = content;
            Group = group;
        }

        public void AddComment(Comment comment) => Comments.Add(comment);

        public Comment GetComment(int id) => Comments.SingleOrDefault(c => c.Id == id);
    }
}
