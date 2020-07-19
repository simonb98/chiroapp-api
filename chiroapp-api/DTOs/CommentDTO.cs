using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chiroapp_api.DTOs
{
    public class CommentDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
