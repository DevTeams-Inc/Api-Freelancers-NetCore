using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Vm;
using Service.Interface;

namespace ApiFreelancers.Controllers
{
    [Produces("application/json")]
    [Route("api/freelancers")]
    //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FreelancerController : Controller
    {
        private readonly IFreelancerService _freelancer;
        private readonly IImageWriter _imageHandler;
        private readonly IFreelancerHabilityService _freelancerHabilityService;
        public FreelancerController(IFreelancerService freelancer,
            IImageWriter imageHandler,
            IFreelancerHabilityService freelancerHabilityService)
        {
            _freelancer = freelancer;
            _imageHandler = imageHandler;
            _freelancerHabilityService = freelancerHabilityService;
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
        public IActionResult GetUser(string id)
        {
            var model = _freelancer.GetByIdUser(id);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("This user not exist");
            }
        }

        [HttpGet("{id}")]
        [Route("edit/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_freelancer.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FreelancerVm model)
        {

            if (model.ApplicationUserId != null && model.PriceHour > 0 && model.Lenguaje != null
                && model.Interest != null)
            {
                    model.Avatar = "d3697ecd-96f7-4bc0-b4df-de05673f7639.png";
                    _freelancer.Add(model);
                    return new CreatedAtRouteResult("freelancerCreated", new { id = model.Id }, model);
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] FreelancerVm model)
        {
            if (model.Id != 0)
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
            var model = _freelancer.Delete(id);
            if (model == true)
            {
                return Ok(_freelancer.Delete(id));
            }
            else
            {
                return BadRequest("ocurrio un error al eliminar");
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            return Ok(_freelancer.GetTree());
        }

        [AllowAnonymous]
        [HttpGet("{query}")]
        [Route("search")]
        public IActionResult Search([FromQuery]string query)
        {
            if (query != null)
            {
                var model = _freelancer.Search(query);
                if (model != null)
                {
                    return Ok(_freelancer.Search(query));
                }
                else
                {
                    return BadRequest("No se encontraron resultados");
                }
            }
            else
            {
                return Ok(_freelancer.GetAll(1));
            }
            
        }

        [HttpGet]
        [Route("admin/getall")]
        public IActionResult GetAdmin()
        {
            return Ok(_freelancer.GetAllAdmin());
        }

        [HttpGet("{id}")]
        [Route("exist/{id}")]
        public IActionResult UserExist(string id)
        {
            var model = _freelancer.UserExist(id);
            if (model)
            {
                return Ok(model);
            }
            return BadRequest("Este usuario no existe");
        }

        [HttpGet]
        [Route("map")]
        public IActionResult Map()
        {
            return Ok(_freelancer.GetAllMap());
        }

        [HttpPost]
        [Route("contact")]
        public IActionResult Contact([FromBody] ContactVm model)
        {
            if (ModelState.IsValid)
            {
                var user = _freelancer.GetByIdUser(model.FromId);
                try
                {
                    model.FreelancerId = user.Id;
                }
                catch (Exception)
                {

                    model.FreelancerId = 0;
                }
                return Ok(_freelancer.Contact(model));
            }
            else
            {
                return BadRequest("Error contact failure");
            }
        }

        [HttpPost]
        [Route("delete/hability")]
        public IActionResult DeleteHability([FromBody] DeleteHabilityVm model)
        {
            if (ModelState.IsValid)
            {
                var result = _freelancerHabilityService.DeleteByFreelancerAndHability(model.Freelancer, model.Hability);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Error to delete the hability");
                }
            }
            else
            {
                return BadRequest("Esa habilidad o freelancer no existe");

            }

        }

        [HttpGet("{idHability,rate}")]
        [Route("filter")]
        public IActionResult Filter([FromQuery]int? idHability, [FromQuery]int? rate)
        {
            return Ok(_freelancer.Filter(idHability, rate));
        }

        
    }
}