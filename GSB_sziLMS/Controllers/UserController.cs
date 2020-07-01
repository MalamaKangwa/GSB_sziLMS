using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/users")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers(trackChanges: false);
                var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(userDto);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetUsers)} action {ex}");
                return Ok("Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetOneUser(Guid id)
        {
            var user = _repository.User.GetUser(id, trackChanges: false);
            if (user == null) { _logger.LogInfo($"user with id: {id} doesn't exist in the database."); return NotFound(); }
            else
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
        }
    }
}
