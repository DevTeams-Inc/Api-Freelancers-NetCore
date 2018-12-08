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
    [Route("api/ratings")]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rating model)
        {
            if (ModelState.IsValid)
            {
                var result = _ratingService.Add(model);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se ha podido puntuar");
                }
            }
            else
            {
                return BadRequest("No se ha podido puntuar");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_ratingService.GetById(id));
        }


    }
}