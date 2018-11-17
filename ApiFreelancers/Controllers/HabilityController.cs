using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Interface;

namespace ApiFreelancers.Controllers
{
    [Produces("application/json")]
    [Route("api/habilities")]
    public class HabilityController : Controller
    {
        private readonly IHabilityService _hablility;
        public HabilityController(IHabilityService hablility)
        {
            _hablility = hablility;
        }

        [HttpGet("{id}", Name = "createdHability")]
        public IActionResult Get(int id)
        {
            var model = _hablility.GetById(id);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("This hability not exist");
            }
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_hablility.GetAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Hability model)
        {
            if (ModelState.IsValid)
            {
                _hablility.Add(model);
                return new CreatedAtRouteResult("createdHability" , new { id = model.Id } , model);
                
            }
                return BadRequest("Some fields are empty");
        }

        [HttpPut]
        public IActionResult Put([FromBody] Hability model)
        {
            if (model.Id > 0 && model.Title != null )
            {
                return Ok(_hablility.Update(model));
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             return Ok(_hablility.Delete(id));
        }
    }
}