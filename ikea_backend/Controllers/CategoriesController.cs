using ikea_data.Models;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _svc;
        public CategoriesController(ICategoryService svc) => _svc = svc;

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _svc.GetPagedAsync(page, pageSize);
            return Ok(new { items, totalCount });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
            await _svc.GetAsync(id) is { } c ? Ok(c) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Create(Category cat)
        {
            var id = await _svc.CreateAsync(cat);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Category cat) =>
            await _svc.UpdateAsync(id, cat) ? Ok(new { id }) : NotFound();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            await _svc.DeleteAsync(id) ? Ok(new { id }) : NotFound();
    }
}