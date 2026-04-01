using App.Application;
using App.core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        // GET: api/Disease
        [HttpGet]
        public async Task<IActionResult> GetAllDiseases()
        {
            var users = await _diseaseService.ListDiseases();
            return Ok(users);
        }

        // POST: api/Disease
        [HttpPost]
        public async Task<IActionResult> CreateDisease([FromBody] Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDisease = await _diseaseService.CreateDisease(disease);
            if (createdDisease == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Disease/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisease(int id, [FromBody] Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            disease.DiseaseId = id;
            var updatedDisease = await _diseaseService.UpdateDisease(disease);
            if (updatedDisease == null)
            {
                return NotFound();  
            }

            return Ok(updatedDisease);  
        }

        // DELETE: api/Disease/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _diseaseService.RemoveDisease(id);
            return NoContent();  
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetAverageNumberOfDiseases()
        {
            try
            {
                float average = await _diseaseService.AverageNumberOfDiseases();
                return Ok(new { averageNumberOfDiseases = average });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while calculating the average number of diseases.", error = ex.Message });
            }
        }
    }
}
