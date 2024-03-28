using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaMarketplace.Presentation.Web.Controllers
{
    [Route("api/category/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<CategoryEntity>> CreateCategory([FromBody] CreateCategoryDto model)
        {
            return Ok(await _categoryService.CreateCategory().Execute(model)); 
        }

        [HttpGet("")]
        public async Task<ActionResult<ICollection<CategoryEntity>>> GetCategoriesList([FromQuery] GetCategoriesListDto dto)
        {
            return Ok(await _categoryService.GetCategoriesList().Execute(dto)); 
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryEntity>> GetCategory(Guid categoryId)
        {
            return Ok(await _categoryService
                .GetCategory()
                .Execute(new GetCategoryDto() { CategoryId = categoryId })); 
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<CategoryEntity>> DeleteCategory(Guid categoryId)
        {
            return Ok(await _categoryService
                .DeleteCategory()
                .Execute(new DeleteCategoryDto() { CategoryId = categoryId }));
        }

        [HttpPatch("{categoryId}")]
        public async Task<ActionResult<CategoryEntity>> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryDto model)
        {
            return Ok(await _categoryService.UpdateCategory().Execute(model)); 
        }
    }
}
