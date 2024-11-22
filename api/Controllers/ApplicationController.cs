using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Application;
using api.Helpers;
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
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applications = await _applicationRepo.GetAllAsync(query);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateApplicationRequestDto applicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = applicationDto.ToApplicationFromCreateDto();
            await _applicationRepo.CreateAsync(application);
            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application.ToApplicationDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateApplicationRequestDto applicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _applicationRepo.UpdateAsync(id, applicationDto);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application.ToApplicationDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var application = await _applicationRepo.DeleteAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}