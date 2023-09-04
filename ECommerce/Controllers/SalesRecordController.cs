using Ecommerce.Models;
using Ecommerce.Repositories.Collections;
using Ecommerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRecordController : Controller
    {
        private readonly ISalesRecordCollection _salesRecordRepository;

        public SalesRecordController(ISalesRecordCollection salesRecordRepository)
        {
            _salesRecordRepository = salesRecordRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesRecords()
        {
            var salesRecords = await _salesRecordRepository.GetAllSalesRecords();
            return Ok(salesRecords);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesRecordById(string id)
        {
            var salesRecord = await _salesRecordRepository.GetSalesRecordById(id);
            if (salesRecord == null)
            {
                return NotFound();
            }
            return Ok(salesRecord);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesRecord([FromBody] SalesRecord salesRecord)
        {
            if (salesRecord == null)
            {
                return BadRequest();
            }

            if (salesRecord.SaleDate == DateTime.MinValue)
            {
                ModelState.AddModelError("SaleDate", "The SaleDate must be a valid date");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _salesRecordRepository.InsertSalesRecord(salesRecord);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesRecord(string id, [FromBody] SalesRecord salesRecord)
        {
            if (salesRecord == null)
            {
                return BadRequest();
            }

            if (salesRecord.SaleDate == DateTime.MinValue)
            {
                ModelState.AddModelError("SaleDate", "The SaleDate must be a valid date");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            salesRecord.Id = new MongoDB.Bson.ObjectId(id);

            await _salesRecordRepository.UpdateSalesRecord(salesRecord);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesRecord(string id)
        {
            await _salesRecordRepository.DeleteSalesRecord(id);

            return NoContent();
        }
    }
}
