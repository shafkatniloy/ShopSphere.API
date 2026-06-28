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
            return Ok(categoryList);
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
            

            return Created($"/api/Categories/{Category1.CategoryId}", CategoryReadDto);
        }

        // DELETE: api/categories/{catID} => delete a category by ID
        [HttpDelete("{CatID:guid}")]
        public IActionResult DeleteCategory(Guid CatID)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == CatID);

            if (foundCategory == null)
            {
                return NotFound("category eith this id does not exist");
            }

            categories.Remove(foundCategory);
            return NoContent();

        }

        // PUT: api/categories/{catID} => update a category by ID
        [HttpPut("{CatID:guid}")]
        public IActionResult UpdateCategory(Guid CatID, [FromBody] CategoryCreateDto CatData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == CatID);

            if (foundCategory == null)
            {
                return NotFound("category eith this id does not exist");
            }

            if (CatData.Name == null)
            {
                return BadRequest("Category name is Required");
            }
            if (!string.IsNullOrEmpty(CatData.Name))
            {
                if (CatData.Name.Length < 3)
                {
                    return BadRequest("Category name must be at least 3 characters long");
                }
                foundCategory.Name = CatData.Name;
            }

            if (!string.IsNullOrEmpty(CatData.Description))
            {
                foundCategory.Description = CatData.Description;
            }

            return NoContent();
        }

    }
}