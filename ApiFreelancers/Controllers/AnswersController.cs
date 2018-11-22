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
    [Route("api/answers")]
    public class AnswersController : Controller
    {
        private readonly IAnswersService _answersService;
        public AnswersController(IAnswersService answersService)
        {
            _answersService = answersService;
        }


        [HttpGet("{id}" , Name = "GetByIdAnswer")]
        public IActionResult Get(int id)
        {
            return Ok(_answersService.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Answer model)
        {
            if (ModelState.IsValid)
            {
                _answersService.Add(model);
                return new CreatedAtRouteResult("GetByIdAnswer" , new { id = model.Id} , model);
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }
    }
}