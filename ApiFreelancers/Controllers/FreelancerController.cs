using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class FreelancerController : Controller
    {
        private readonly IFreelancerService _freelancer;
        private readonly IImageWriter _imageHandler;
        public FreelancerController(IFreelancerService freelancer ,
            IImageWriter imageHandler)
        {
            _freelancer = freelancer;
            _imageHandler = imageHandler;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FreelancerVm model, [FromForm]IFormFile file)
        {

            if (model.ApplicationUserId != null && model.PriceHour > 0 && model.Lenguaje != null
                && model.Interest != null)
            {

                if (file == null)
                {
                    model.Avatar = "d3697ecd-96f7-4bc0-b4df-de05673f7639.png";
                    _freelancer.Add(model);
                    return new CreatedAtRouteResult("freelancerCreated", new { id = model.Id }, model);
                }
                else
                {
                    var avatar = await _imageHandler.UploadImage(file);
                    model.Avatar = avatar.ToString();
                    _freelancer.Add(model);
                    return new CreatedAtRouteResult("freelancerCreated", new { id = model.Id }, model);
                }
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
        [Route("exist")]
        public IActionResult UserExist(string id)
        {
              return Ok(_freelancer.UserExist(id));
        }
    }
}