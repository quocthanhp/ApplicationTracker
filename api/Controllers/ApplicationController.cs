using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Application;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public readonly ApplicationDBContext _context;
        public readonly IApplicationRepository _applicationRepo;
        public ApplicationController(ApplicationDBContext context, IApplicationRepository applicationRepository)
        {
            _context = context;
            _applicationRepo = applicationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applications = await _applicationRepo.GetAllAsync();
            var applicationDto = applications.Select(s => s.ToApplicationDto());
            return Ok(applications);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _applicationRepo.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application.ToApplicationDto());
        }  
        
    }
}