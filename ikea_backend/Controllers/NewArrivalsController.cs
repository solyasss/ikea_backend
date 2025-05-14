using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [ApiController]
    [Route("api/new-arrivals")]
    public class NewArrivalsController : ControllerBase
    {
        private readonly INewArrivalService _svc;
        public NewArrivalsController(INewArrivalService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
            await _svc.GetAsync(id) is { } n ? Ok(n) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Create(NewArrivalInput input)
        {
            var id = await _svc.CreateAsync(input);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, NewArrivalInput input) =>
            await _svc.UpdateAsync(id, input) ? Ok(new { id }) : NotFound();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            await _svc.DeleteAsync(id) ? Ok() : NotFound();
    }
}