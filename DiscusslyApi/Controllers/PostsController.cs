using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Discussly.Models;
using DiscusslyApi.DAL;

namespace DiscusslyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostManeger _maneger;

        public PostsController(PostManeger maneger)
        {
            _maneger = maneger;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            var posts = await _maneger.GetAllPostsAsync();
            return Ok(posts);
        }


        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _maneger.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            var existing = await _maneger.GetPostByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _maneger.UpdatePostAsync(post);
            return NoContent();
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            await _maneger.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var existing = await _maneger.GetPostByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _maneger.DeletePostAsync(id);
            return NoContent();
        }
    }
}

