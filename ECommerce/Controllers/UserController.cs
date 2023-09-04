using Ecommerce.Models;
using Ecommerce.Repositories.Collections;
using Ecommerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations; 

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserCollection _userRepository;

        public UserController(IUserCollection userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await _userRepository.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                ModelState.AddModelError("Name", "The Name User shouldn't be empty");
            }

            if (string.IsNullOrEmpty(user.UserName))
            {
                ModelState.AddModelError("UserName", "The UserName field is required");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "The Password field is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.InsertUser(user);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                ModelState.AddModelError("Name", "The Name User shouldn't be empty");
            }

            if (string.IsNullOrEmpty(user.UserName))
            {
                ModelState.AddModelError("UserName", "The UserName field is required");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "The Password field is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Id = new MongoDB.Bson.ObjectId(id);
            await _userRepository.UpdateUser(user);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userRepository.DeleteUser(id);

            return NoContent();
        }
    }
}
