using Ecommerce.Models;
using Ecommerce.Repositories.Collections;
using Ecommerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryCollection _categoryRepository;

        public CategoryController(ICategoryCollection categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryRepository.GetAllCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            return Ok(await _categoryRepository.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(category.Name))
            {
                ModelState.AddModelError("Name", "The Name Category shouldn't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryRepository.InsertCategory(category);

            string idString = category.Id.ToString();

            return Created("Created", new { Id = idString });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(category.Name))
            {
                ModelState.AddModelError("Name", "The Name Category shouldn't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            category.Id = new MongoDB.Bson.ObjectId(id);

            await _categoryRepository.UpdateCategory(category);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryRepository.DeleteCategory(id);

            return NoContent();
        }
    }
}
