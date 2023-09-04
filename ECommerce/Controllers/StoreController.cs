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
    public class StoreController : Controller
    {
        private readonly IStoreCollection _storeRepository;

        public StoreController(IStoreCollection storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            return Ok(await _storeRepository.GetAllStores());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(string id)
        {
            return Ok(await _storeRepository.GetStoreById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(store.Name))
            {
                ModelState.AddModelError("Name", "The Name Store shouldn't be empty");
            }

            if (string.IsNullOrEmpty(store.Location))
            {
                ModelState.AddModelError("Location", "The Location field is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _storeRepository.InsertStore(store);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(string id, [FromBody] Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(store.Name))
            {
                ModelState.AddModelError("Name", "The Name Store shouldn't be empty");
            }

            if (string.IsNullOrEmpty(store.Location))
            {
                ModelState.AddModelError("Location", "The Location field is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            store.Id = new MongoDB.Bson.ObjectId(id);

            await _storeRepository.UpdateStore(store);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(string id)
        {
            await _storeRepository.DeleteStore(id);

            return NoContent();
        }
    }
}
