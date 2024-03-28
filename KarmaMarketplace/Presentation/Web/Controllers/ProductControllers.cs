using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/product/")]
    [ApiController]
    public class ProductControllers : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductControllers(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]   
        public async Task<ActionResult<ProductEntity>> GetProducts([FromQuery] GetProductsListDto dto)
        {
            return Ok(await _productService.GetProductsList().Execute(dto));
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductEntity>> GetProductById(Guid productId)
        {
            return Ok(await _productService
                .GetProduct()
                .Execute(new GetProductDto() { ProductId = productId })); 
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<ProductEntity>> DeleteProduct(Guid productId)
        {
            return (Ok(await _productService
                .DeleteProduct()
                .Execute(new DeleteProductDto() { ProductId = productId}))); 
        }

        [HttpPost("")]
        public async Task<ActionResult<ProductEntity>> CreateProduct([FromBody] CreateProductDto model)
        {
            return Ok(await _productService
                .CreateProduct()
                .Execute(model)); 
        }

        [HttpPatch("{productId}")]
        public async Task<ActionResult<ProductEntity>> UpdateProduct([FromBody] UpdateProductDto model)
        {
            return Ok(await _productService
                .UpdateProduct()
                .Execute(model));
        }

    }
}
