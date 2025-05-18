using ikea_business.DTO;
using ikea_business.Services.Implementations;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCharacteristicController : ControllerBase
    {
        private readonly IProductCharacteristicService _productCharacteristic;
       
        public ProductCharacteristicController(IProductCharacteristicService productCharacteristic)
        {
            _productCharacteristic = productCharacteristic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
           Ok(await _productCharacteristic.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
          await _productCharacteristic.GetAsync(id) is { } p ? Ok(p) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Create(ProductCharacteristicInput dto)
        {
            var id = await _productCharacteristic.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductCharacteristicInput dto) =>  
           await _productCharacteristic.UpdateAsync(id, dto) ? Ok(new { id }) : NotFound(); 

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>  
            await _productCharacteristic.DeleteAsync(id) ? Ok(new { id }) : NotFound();     

    }
}
