using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace ApiFreelancers.Controllers
{
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("proyects/count")]
        public IActionResult GetProyects()
        {
            return Ok(_adminService.ProjectsPublics());
        }

        [HttpGet]
        [Route("freelancers/count")]
        public IActionResult GetFreelancers()
        {
            return Ok(_adminService.FreelancersRegisters());
        }

        [HttpGet]
        [Route("category/count")]
        public IActionResult GetCategory()
        {
            return Ok(_adminService.TotalOfCategories());
        }

        [HttpGet]
        [Route("hability/count")]
        public IActionResult GetHability()
        {
            return Ok(_adminService.TotalOfHabilities());
        }

        [HttpGet]
        [Route("best/freelancer")]
        public IActionResult BestFreelancers()
        {
            return Ok(_adminService.BestFreelancer());
        }
    }
}