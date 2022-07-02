using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchMvc.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();

            if (products == null)
            {
                return NotFound("Products not found.");
            }
            
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        { 
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound($"Product {id} not found.");
            }
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest($"Invalid data.");
            }

            await _productService.Add(productDTO);
            
            return new CreatedAtRouteResult("GetProduct", new {id = productDTO.Id }, productDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id || productDTO == null)
            {
                return BadRequest($"Invalid data.");
            }

            await _productService.Update(productDTO);
            
            return Ok(productDTO); 
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound($"Product {id} not found.");
            }

            await _productService.Remove(id);
            
            return Ok(product); 
        }
    }
}