using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [Route("api/productsImage")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        public ProductImageController(IProductImageService productImageService) 
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _productImageService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
             await _productImageService.GetAsync(id) is { } p ? Ok(p) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Create(ProductImageInput dto)
        {
            var id = await _productImageService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductImageInput dto) =>
            await _productImageService.UpdateAsync(id, dto) ? Ok(new { id }) : NotFound();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
         await _productImageService.DeleteAsync(id) ? Ok(new { id }) : NotFound();

    }
}
