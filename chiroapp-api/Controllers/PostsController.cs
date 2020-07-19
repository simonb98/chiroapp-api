using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chiroapp_api.DTOs;
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

        [HttpGet]
        public IEnumerable<Post> GetPosts(string title = null, string group = null)
        {
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(group))
                return _postRepository.GetAll();
            return _postRepository.GetBy(title, group);
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            Post post = _postRepository.GetBy(id);
            if (post == null) return NotFound();
            return post;
        }

        [HttpPost]
        public ActionResult<Post> PostPost(PostDTO post)
        {
            Post postToCreate = new Post() { Title = post.Title, Content = post.Content, Group = post.Group };
            foreach (var c in post.Comments)
                postToCreate.AddComment(new Comment(c.Content));
            _postRepository.Add(postToCreate);
            _postRepository.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { id = postToCreate.Id }, postToCreate);
        }

        [HttpPut("{id}")]
        public IActionResult PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _postRepository.Update(post);
            _postRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            Post post = _postRepository.GetBy(id);
            if (post == null)
            {
                return NotFound();
            }

            _postRepository.Delete(post);
            _postRepository.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}/comments/{commentId}")]
        public ActionResult<Comment> GetComment(int id, int commentId)
        {
            if(!_postRepository.TryGetPost(id, out var post))
            {
                return NotFound();
            }
            Comment comment = post.GetComment(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        [HttpPost ("{id}/comments")]
        public ActionResult<Comment> PostComment(int id, CommentDTO comment)
        {
            if(!_postRepository.TryGetPost(id, out var post))
            {
                return NotFound();
            }
            var commentToCreate = new Comment(comment.Content);
            post.AddComment(commentToCreate);
            _postRepository.SaveChanges();
            return CreatedAtAction("GetComment", new { id = post.Id, commentId = commentToCreate.Id }, commentToCreate);
        }
    }
}