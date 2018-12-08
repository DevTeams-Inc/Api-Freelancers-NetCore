using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Vm;
using Service.Interface;

namespace ApiFreelancers.Controllers
{
    [Produces("application/json")]
    [Route("api/Proyects")]
    public class ProyectController : Controller
    {
        private readonly IProyectService _proyect;
        public ProyectController(IProyectService proyect)
        {
            _proyect = proyect;
        }

        [HttpGet("{page}")]
        [Route("")]
        [Route("getall")]
        [Route("getall/{page}")]
        public IActionResult Get(int page = 1)
        {
            return Ok(_proyect.GetAll(page));
        }

        [HttpGet]
        [Route("admin/getall")]
        public IActionResult GetAdmin()
        {
            return Ok(_proyect.GetAllAdmin());
        }


        [HttpGet("{id}" , Name = "GetByIdProyect")]
        [Route("getproyect/{id}")]
        public IActionResult GetProyect(int id)
        {
            var model = _proyect.GetById(id);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("This proyect not exist");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProyectVm model)
        {
            if (ModelState.IsValid)
            {
                _proyect.Add(model);
                return new CreatedAtRouteResult("GetByIdProyect", new { id = model.Id }, model);
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProyectVm model)
        {
            if (model.Id != 0 && model.Title != null)
            {
                return Ok(_proyect.Update(model));
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        { var model = _proyect.Delete(id);
            if (model)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("No se ha eliminado el proyecto");
            }
        }
    }
}