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
    [Route("freelancers")]
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
        public IActionResult Post([FromBody] Freelancer model)
        {
            return Ok(_freelancer.Add(model));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Freelancer model)
        {
            return Ok(_freelancer.Update(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_freelancer.Delete(id));
        }

    }
}