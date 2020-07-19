using chiroapp_api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chiroapp_api.DTOs
{
    public class PostDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Group { get; set; }

        public ICollection<Comment> Comments { get; private set; }
    }
}
