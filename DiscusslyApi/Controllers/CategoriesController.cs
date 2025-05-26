using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Discussly.Models;
using DiscusslyApi.DAL;
using DiscusslyApi.Interface;

namespace DiscusslyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManeger _maneger;

        public CategoriesController(ICategoryManeger maneger)
        {
            _maneger = maneger;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var categories = await _maneger.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _maneger.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category Category)
        {
            if (id != Category.Id)
            {
                return BadRequest();
            }

            var existing = await _maneger.GetCategoryByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _maneger.UpdateCategoryAsync(Category);
            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category Category)
        {
            await _maneger.AddCategoryAsync(Category);
            return CreatedAtAction(nameof(GetCategory), new { id = Category.Id }, Category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existing = await _maneger.GetCategoryByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _maneger.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}