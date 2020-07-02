using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjectsForCreation;
using Entities.DataTransferObjectsForUpdate;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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


        [HttpPost]
        public IActionResult CreateUser([FromBody]UserForCreationDto user)
        {
            if (user == null)
            {
                _logger.LogError("CourseForCreationDto object sent from this client is null.");
                return BadRequest("CourseForCreationDto object is null");
            }

            var userEntity = _mapper.Map<User>(user);

            _repository.User.CreateUser(userEntity);
            _repository.Save();

            var userToReturn = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("UserById", new { id = userToReturn.Id }, userToReturn);

        }


        [HttpPost("collection")]
        public IActionResult CreateUserCollection([FromBody] IEnumerable<UserForCreationDto> userCollection)
        {
            if (userCollection == null)
            {
                _logger.LogError("User collection sent from client is null.");
                return BadRequest("User collection is null");
            }

            var userEntities = _mapper.Map<IEnumerable<User>>(userCollection);
            foreach (var user in userEntities)
            {
                _repository.User.CreateUser(user);
            }

            _repository.Save();

            var userCollectionToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);
            var ids = string.Join(",", userCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("UserCollection", new { ids }, userCollectionToReturn);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserForUpdateDto user)
        {
            if (user == null)
            {
                _logger.LogError("UserForUpdateDto object sent from client is null.");
                return BadRequest("UserForUpdateDto object is null");
            }

            var userEntity = _repository.User.GetUser(id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(user, userEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateUser(Guid id,
        [FromBody] JsonPatchDocument<UserForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var userEntity = _repository.User.GetUser(id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var userToPatch = _mapper.Map<UserForUpdateDto>(userEntity);
            patchDoc.ApplyTo(userToPatch);

            _mapper.Map(userToPatch, userEntity);
            _repository.Save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _repository.User.GetUser(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var userEnrollmets = _repository.Enrollment.GetEnrollmentsForUser(id, trackChanges: true)
                .SingleOrDefault(p => p.UserId == id);

            var userSubmissions = _repository.Submission.GetSubmissionsforEnrollment(userEnrollmets.Id, trackChanges: true);
            foreach (var submission in userSubmissions)
            {
                _repository.Submission.DeleteSubmission(submission);
            }

            _repository.User.DeleteUser(user);
            _repository.Save();

            return NoContent();
        }
    }
}
