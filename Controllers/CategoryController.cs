using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net_web_api.Models;
using dotnet_web_api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        //   categories creation--
        private static List<Category> categories = new List<Category>();

        // GET: api/categories => read categories
        [HttpGet]
        public IActionResult getCategories([FromQuery] string SearchValue = "")
        {
            // if (SearchValue != null)
            // {
            //     var SearchedCategories = categories.Where(c => c.Name != null && c.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            //     return Ok(SearchedCategories);
            // }

           var categoryList = categories.Select (c=> new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, 200, "Categories retrieved successfully"));
        }

        // GET: api/categories/{CategoryID} => read categories by catgory id
        [HttpGet("{CatId:guid}")]
        public IActionResult getCategoryById(Guid CatId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == CatId);
            
            if (foundCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 404, "Category not found"));
            }

           var categoryReadDto =  new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, 200, "Category returned successfully"));
        }

        // POST: api/categories => create a category
        [HttpPost]
        public IActionResult CreateCategories([FromBody] CategoryCreateDto CatData)
        {

  

            var Category1 = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = CatData.Name,
                Description = CatData.Description,
                CreatedAt = DateTime.UtcNow
            };
            categories.Add(Category1);

            //making a dto copy of the created category to return in the response
            var CategoryReadDto = new CategoryReadDto
            {
                CategoryId = Category1.CategoryId,
                Name = Category1.Name,
                Description = Category1.Description,
                CreatedAt = Category1.CreatedAt
            };
            

            return Created($"/api/Categories/{Category1.CategoryId}", ApiResponse<CategoryReadDto>.SuccessResponse(CategoryReadDto, 201, "Category created successfully"));
        }

        // DELETE: api/categories/{catID} => delete a category by ID
        [HttpDelete("{CatID:guid}")]
        public IActionResult DeleteCategory(Guid CatID)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == CatID);

            if (foundCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 404, "Category not found"));
            }

            categories.Remove(foundCategory);
            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category deleted successfully"));
        }

        // PUT: api/categories/{catID} => update a category by ID
        [HttpPut("{CatID:guid}")]
        public IActionResult UpdateCategory(Guid CatID, [FromBody] CategoryCreateDto CatData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == CatID);

            if (foundCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 404, "Category not found"));
            }

            
            foundCategory.Name = CatData.Name;
            foundCategory.Description = CatData.Description;
            

            return Ok(ApiResponse<object>.SuccessResponse( null, 204, "Category updated successfully"));
        }

    }
}