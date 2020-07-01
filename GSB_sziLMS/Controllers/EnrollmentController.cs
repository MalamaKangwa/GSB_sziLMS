using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/users/{userId}/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EnrollmentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEnrollmentsForUser(Guid userId)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentsFromDb = _repository.Enrollment.GetEnrollmentsForUser(userId, trackChanges: false);
            var enrollmentsDto = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentsFromDb);

            return Ok(enrollmentsDto);

        }

        [HttpGet("{id}", Name = "GetEnrollmentForUser")]
        public IActionResult GetEnrollmentForUser(Guid userId, Guid id)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentFromDb = _repository.Enrollment.GetOneEnrollmentForUser(userId, id, trackChanges: false);
            if (enrollmentFromDb == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollmentFromDb);

            return Ok(enrollmentDto);
        }
    }
}