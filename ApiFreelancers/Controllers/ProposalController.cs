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

        [HttpGet("{id}" , Name = "GetByIdProposal")]
        public IActionResult Get(int id)
        {
            return Ok(_proposalService.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Proposal model)
        {
            if (ModelState.IsValid)
            {
                _proposalService.Add(model);
                return new CreatedAtRouteResult("GetByIdProposal", new { id = model.Id } , model);
            }
            else
            {
                return BadRequest("Some fields are empty");
            }
        }


    }
}