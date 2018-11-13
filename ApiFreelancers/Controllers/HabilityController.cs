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
    [Route("api/hability")]
    public class HabilityController : Controller
    {
        private readonly IHabilityService _hablility;
        public HabilityController(IHabilityService hablility)
        {
            _hablility = hablility;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _hablility.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Hability model)
        {
            return Ok(_hablility.Add(model));
        }
    }
}