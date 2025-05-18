using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [Route("api/productComments")]
    [ApiController]
    public class ProductCommentsController : ControllerBase
    {
        private readonly IProductCommentService _productCommentService;

        public ProductCommentsController(IProductCommentService productCommentService) 
        {
            _productCommentService = productCommentService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
           Ok(await _productCommentService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
          await _productCommentService.GetAsync(id) is { } p ? Ok(p) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Create(ProductCommentInput dto)
        {
            var id = await _productCommentService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductCommentInput dto) =>
           await _productCommentService.UpdateAsync(id, dto) ? Ok(new { id }) : NotFound();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            await _productCommentService.DeleteAsync(id) ? Ok(new { id }) : NotFound(); 
    }
}
