using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TDD.Application.Contracts;
using TDD.Domain;

namespace TDD_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }
        [HttpGet("Category")]
        public IActionResult GetAll()
        {
          var result= _categoryService.GetAll();
            return Ok(result);
        }
        [HttpGet("Category/{id:guid}")]
        public IActionResult GetById(Guid id) { 
        
         var category= _categoryService.GetById(id);
            if (category == null) { 
            
                return NotFound();  
            }
           return Ok(category);    
        }

        [HttpPost("Category")]
        public IActionResult Create(Category category) {

            var result = _categoryService.Create(category);
            if (result == 0)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        [HttpDelete("Category/{id:guid}")]
        public IActionResult DeleteById(Guid id)
        {
            var deleted = _categoryService.DeleteById(id);
            if (deleted==0)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}
