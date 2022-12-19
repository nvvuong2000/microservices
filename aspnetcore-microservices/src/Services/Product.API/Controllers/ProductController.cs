using AutoMapper;
using Constracts.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistance;
using Product.API.Repository.Interfaces;
using Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly IRepositoryBaseAsync<Entities.Product,long, ProductContext> _repository;

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProducts();
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(result);

        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct([Required] long id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto  createProductDto)
        {
            var exitsProduct = GetProductByProductNo(createProductDto.No);
            if (exitsProduct != null) return BadRequest($"Product No {createProductDto.No} is existed");
            
            var product = _mapper.Map<Entities.Product>(createProductDto);
            await _repository.CreateProduct(product);
            await _repository.SaveChangeAsync();
            return Ok(product);

        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            var updateProduct = _mapper.Map(updateProductDto, product);
            await _repository.UpdateProduct(updateProduct);
            await _repository.SaveChangeAsync();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);

        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            await _repository.DeleteProduct(id);
            await _repository.SaveChangeAsync();
            return NoContent();

        }

        [HttpGet("api/get-product-by-no/{productNo}")]
        public async Task<IActionResult> GetProductByProductNo([Required] string productNo)
        {
            var product = await _repository.GetProductByNo(productNo);
            if (product == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);

        }
    }
}
