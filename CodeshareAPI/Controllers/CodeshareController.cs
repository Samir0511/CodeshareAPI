using Application.DTOs;
using DomainLayer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodeshareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeshareController : ControllerBase
    {
        private readonly IDataShare _dataShare;

        public CodeshareController(IDataShare dataShare)
        {
            _dataShare = dataShare;
        }

        [HttpGet("{urlKey}")]
        public async Task<IActionResult> GetByName(string urlKey)
        {
            if (string.IsNullOrEmpty(urlKey))
            {
                return BadRequest("Key cannot be null or empty.");
            }

            var result = await _dataShare.GetAsync(urlKey);
            if (result == null)
            {
                return NotFound("No data found for the provided key.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Codeshare codeshare)
        {
            if (codeshare == null || string.IsNullOrEmpty(codeshare.UrlKey))
            {
                return BadRequest("Invalid codeshare data.");
            }

            var existing = await _dataShare.GetAsync(codeshare.UrlKey);

            if (existing != null)
            {
                existing.Description = codeshare.Description;
                var updated = await _dataShare.UpdateAsync(existing);
                return Ok(updated);
            }
            else
            {
                var data = new DomainLayer.Entity.CodeshareEntity
                {
                    UrlKey = codeshare.UrlKey,
                    Description = codeshare.Description
                };

                var created = await _dataShare.CreateAsync(data);
                return Ok(created);
            }
        }

    }
}
