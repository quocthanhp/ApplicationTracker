using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Application;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public readonly ApplicationDBContext _context;
        public readonly IApplicationRepository _applicationRepo;
        private readonly UserManager<AppUser> _userManager;
        public ApplicationController(ApplicationDBContext context, IApplicationRepository applicationRepository, UserManager<AppUser> userManager)
        {
            _context = context;
            _applicationRepo = applicationRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var applications = await _applicationRepo.GetAllAsync(query, appUser.Id);
            var applicationDto = applications.Select(s => s.ToApplicationDto());
            return Ok(applicationDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var application = await _applicationRepo.GetByIdAsync(id, appUser.Id);
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

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var application = applicationDto.ToApplicationFromCreateDto();
            application.UserId = appUser.Id;
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

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var application = await _applicationRepo.UpdateAsync(id, applicationDto);
            if (application == null || application.UserId != appUser.Id)
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

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var application = await _applicationRepo.DeleteAsync(id);
            if (application == null || application.UserId != appUser.Id)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}