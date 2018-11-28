using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Interface;

namespace ApiFreelancers.Controllers
{
    [Produces("application/json")]
    [Route("api/categories")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _category;
        private readonly DateTime _dateTime;
        public CategoryController(ICategoryService category)
        {
            _category = category;
            _dateTime = DateTime.Now;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_category.GetAll());
        }

        [HttpGet("{id}", Name = "GetbyIdCategory")]
        public IActionResult Get(int id)
        {
            var model = _category.GetById(id);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("This category not exist");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category model) {

            if (ModelState.IsValid)
            {
                _category.Add(model);
                return new CreatedAtRouteResult("GetByIdCategory", new { id = model.Id }, model);
            }
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Category model)
        {
            if (model.Id != 0)
            {
                return Ok(_category.Update(model));
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_category.Delete(id));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            var model =  _category.GetTree();
            return Ok(model);
        }
    }
}