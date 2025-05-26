using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Discussly.Models;
using DiscusslyApi.Data;
using DiscusslyApi.Interface;

namespace DiscusslyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentManeger _maneger;

        public CommentsController(ICommentManeger maneger)
        {
            _maneger = maneger;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var comments = await _maneger.GetAllCommentsAsync();
            return Ok(comments);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _maneger.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }
            var existing = await _maneger.GetCommentByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            await _maneger.UpdateCommentAsync(comment);
            return NoContent();
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment(Comment comment)
        {
            await _maneger.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }
        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _maneger.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _maneger.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
