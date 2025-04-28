using AcunMedyaAkademiWebAPI.Context;
using AcunMedyaAkademiWebAPI.DTOs.CategoriesDto;
using AcunMedyaAkademiWebAPI.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public CategoriesController(WebAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _context.Categories.ToList();
            return Ok(category); //200 ok
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound(); //404 NOt Found

            return Ok(category); //200 ok
        }

        [HttpPost]
        public IActionResult Create(CategoriesCreateDto categorydto)
        {
            //modeli kontrol ediyor
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var category = new Category
            {
                CategoryName = categorydto.CategoryName,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Created("", categorydto);  //201 
            //return Ok("Veri başarıyla eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id ,CategoriesUpdateDto categorydto)
        {
            //modeli kontrol ediyor
            var category= _context.Categories.Find(id);
            if (category == null)
                return NotFound();

          category.CategoryName = categorydto.CategoryName;
          _context.SaveChanges();

            //return NoContent(); //204
            /*return StatusCode(204, new {message="Kategori güncellendi"})*/;  //204 
            return Ok("Veri başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //modeli kontrol ediyor
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent(); //204
            /*return StatusCode(204, new {message="Kategori güncellendi"})*/
            ;  //204 
            //return Ok("Veri başarıyla silindi");
        }
    }
}

