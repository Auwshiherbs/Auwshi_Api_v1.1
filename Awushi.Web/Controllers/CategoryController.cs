using Awushi.Web.Data;
using Awushi.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awushi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult Get()
        {
            var categories = _dbContext.Categories.ToList();
            return Ok(categories);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        public ActionResult GetById(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public ActionResult Create([FromBody]Category category)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return Ok();
        }





        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public ActionResult Update([FromBody]Category category)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public ActionResult DeleteById(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x =>x.Id == id);

            if(category == null)
            {
                return BadRequest($"There is no Category on this {id}");
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
    
}
