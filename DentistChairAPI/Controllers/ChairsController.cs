using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DentistChairAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChairsController : ControllerBase
    {
        private readonly IChairService _chairService;

        public ChairsController(IChairService chairService) {
            _chairService = chairService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chair>>> Get() {
            var chairs = await _chairService.GetAllChairsAsync();
            return Ok(chairs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chair>> Get(int id) {
            var chair = await _chairService.GetChairByIdAsync(id);
            if (chair == null) {
                return NotFound();
            }
            return Ok(chair);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Chair chair) {
            await _chairService.AddChairAsync(chair);
            return CreatedAtAction(nameof(Get), new { id = chair.Id }, chair);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Chair chair) {
            if (id != chair.Id) {
                return BadRequest();
            }
            await _chairService.UpdateChairAsync(chair);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var chair = await _chairService.GetChairByIdAsync(id);
            if (chair == null) {
                return NotFound();
            }
            await _chairService.DeleteChairAsync(id);
            return NoContent();
        }

        //[HttpPost("allocate")]
        //public async Task<IActionResult> AllocateChair([FromBody] AllocationRequest request) {
        //    var chair = await _chairService.AllocateChairAsync(request.StartTime, request.EndTime);

        //    if (chair == null) {
        //        return NotFound("No available chairs for the given time period.");
        //    }

        //    return Ok(chair);
        //}
    }
}
