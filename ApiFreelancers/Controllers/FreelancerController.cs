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
    [Route("api/freelancers")]
    public class FreelancerController : Controller
    {
        private readonly IFreelancerService _freelancer;
        public FreelancerController(IFreelancerService freelancer)
        {
            _freelancer = freelancer;
        }

        [HttpGet]
        [Route("")]
        [Route("getall")]
        [Route("getall/{page}")]
        public IActionResult Get(int page = 1)
        {
            return Ok(_freelancer.GetAll(page));
        }

        [HttpGet("{id}", Name = "freelancerCreated")]
        [Route("getuser/{id}")]
        public IActionResult GetUser(int id)
        {
            var model = _freelancer.GetById(id);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("This user not exist");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] FreelancerVm model)
        {

            if (model.Lenguaje != null && model.Interest != null && model.PriceHour > 0)
            {
                _freelancer.Add(model);
                return new CreatedAtRouteResult("freelancerCreated", new { id = model.Id} , model);
            }
            else
            {
                return BadRequest("Some fields are empty");
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] FreelancerVm model)
        {
            if (model.Id > 0 && model.ApplicationUserId != null)
            {
                return Ok(_freelancer.Update(model));
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_freelancer.Delete(id));
        }

    }
}