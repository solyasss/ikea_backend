using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;
        public UsersController(IUserService svc) => _svc = svc;

       
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _svc.GetPagedAsync(page, pageSize);
            return Ok(new { items, totalCount });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
            await _svc.GetAsync(id) is { } u ? Ok(u) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Create(UserInput dto)
        {
            var id = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UserUpdateInput dto) =>
            await _svc.UpdateAsync(id, dto) ? Ok(new { id }) : NotFound();

        [HttpPost("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Password is required");
            if (!await _svc.ChangePasswordAsync(id, dto.Password))
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            await _svc.DeleteAsync(id) ? Ok(new { id }) : NotFound();
    }
}