using Ecommerce.Models;
using Ecommerce.Repositories.Collections;
using Ecommerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations; 

namespace Ecommerce.Controllers
{
    [Authorize]
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductCollection _productRepository;

        public ProductController(IProductCollection productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productRepository.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            return Ok(await _productRepository.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError("Name", "The Name Product shouldn't be empty");
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                ModelState.AddModelError("Description", "The Description field is required");
            }

            if (string.IsNullOrEmpty(product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "The CategoryId field is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productRepository.InsertProduct(product);
            string productIdString = product.Id.ToString();

            return Created("Created", new { Id = productIdString });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError("Name", "The Name Product shouldn't be empty");
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                ModelState.AddModelError("Description", "The Description field is required");
            }

            if (string.IsNullOrEmpty(product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "The CategoryId field is required");
            }

            // Validar el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = new MongoDB.Bson.ObjectId(id);
            await _productRepository.UpdateProduct(product);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productRepository.DeleteProduct(id);

            return NoContent();
        }
    }
}
