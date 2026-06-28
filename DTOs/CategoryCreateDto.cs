using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace dotnet_web_api.DTOs
{
    public class CategoryCreateDto
    {
        [Required (ErrorMessage = "Category name is required")]
        [StringLength(100 ,MinimumLength =3, ErrorMessage = "Category name must be between 3 and 100 characters long")]

        public string Name {get; set;} = string.Empty;

        [Required (ErrorMessage = "Category description is required")]
        [StringLength(500 , ErrorMessage = "Category Description must be less than 500 characters long")]
        public string Description {get; set;}= string.Empty;
    }
}