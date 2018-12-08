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
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("{id}")]
        [Route("contacted/{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_contactService.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _contactService.Delete(id);
            if (model)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("No se pudo descartar");
            }
        }

        [HttpGet("{id}")]
        [Route("contactme/{id}")]
        public IActionResult GetMyContacts(int id)
        {
            var model = _contactService.GetByIdMyContacts(id);
            if (model != null)
            {
                return Ok(model);

            }
            else
            {
                return BadRequest("Este freelancer no existe");
            }
        }

        [HttpPost]
        public IActionResult Validate([FromBody]ValidateContact model)
        {
            return Ok(_contactService.Exist(model));
        }
    }
}