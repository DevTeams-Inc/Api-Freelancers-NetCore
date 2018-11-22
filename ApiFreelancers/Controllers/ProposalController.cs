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
    [Route("api/proposals")]
    public class ProposalController : Controller
    {
        private readonly IProposalService _proposalService;
        public ProposalController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_proposalService.GetAllVm());
        }

        [HttpGet("{id}", Name = "GetByIdProposal")]
        public IActionResult Get(int id)
        {
            return Ok(_proposalService.GetbyIdVm(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Proposal model)
        {
            if (ModelState.IsValid)
            {
                _proposalService.Add(model);
                return new CreatedAtRouteResult("GetByIdProposal", new { id = model.Id }, model);
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Proposal model)
        {
            if (model.Id != 0 && model.ProyectId != 0 && model.ApplicationUserId != null)
            {
                var m = _proposalService.Update(model);
                if (m)
                {
                   return Ok(m);
                }
                else
                {
                    return BadRequest("Error to update this proposal");
                }
            }
               return BadRequest("This proposal not exist");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _proposalService.Delete(id);
            if (model)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("This proposal not exist");
            }
        }


    }
}