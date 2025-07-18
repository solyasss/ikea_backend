﻿using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;
        public ProductsController(IProductService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _svc.GetPagedAsync(page, pageSize);
            return Ok(new { items, totalCount });
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
            await _svc.GetAsync(id) is { } p ? Ok(p) : NotFound();
        

        [HttpPost]
        public async Task<IActionResult> Create(ProductInput dto)
        {
            var id = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductInput dto) =>
            await _svc.UpdateAsync(id, dto) ? Ok(new { id }) : NotFound();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            await _svc.DeleteAsync(id) ? Ok(new { id }) : NotFound();

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var results = await _svc.SearchByNameAsync(name);
            return Ok(results);
        }

    }
}