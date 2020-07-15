using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chiroapp_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chiroapp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostsController(IPostRepository context)
        {
            _postRepository = context;
        }
    }
}