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
    [Route("api/freelancer")]
    public class FreelancerController : Controller
    {
        private readonly IFreelancerService _freelancer;
        public FreelancerController(IFreelancerService freelancer)
        {
            _freelancer = freelancer;
        }

        [HttpGet]
        public IActionResult Get(int page = 1)
        {
            return Ok(_freelancer.GetAll(page));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FreelancerVm model)
        {
            return Ok(_freelancer.AddFreelancerAndHability(model));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Freelancer model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_freelancer.Update(model));
            }
            else
            {
                return BadRequest(model);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_freelancer.Delete(id));
        }

    }
}