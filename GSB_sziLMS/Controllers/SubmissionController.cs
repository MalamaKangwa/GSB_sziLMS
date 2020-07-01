using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/users/{userId}/enrollments/{enrollmentId}/submissions")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SubmissionsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetSubmissionsForEnrollment(Guid userId, Guid enrollmentId)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollment = _repository.Enrollment.GetOneEnrollmentForUser(userId, enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"User with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionsFromDb = _repository.Submission.GetSubmissionsforEnrollment(enrollmentId, trackChanges: false);
            var submissionsDto = _mapper.Map<IEnumerable<SubmissionDto>>(submissionsFromDb);

            return Ok(submissionsDto);

        }


        [HttpGet("{id}", Name = "GetSubmissionForEnrollment")]
        public IActionResult GetSubmissionForEnrollment(Guid userId, Guid enrollmentId, Guid id)
        {
            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollment = _repository.Enrollment.GetOneEnrollmentForUser(userId, enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"User with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionsFromDb = _repository.Submission.GetOneSubmissionForEnrollment(enrollmentId, id, trackChanges: false);
            if (submissionsFromDb == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var submissionsDto = _mapper.Map<SubmissionDto>(submissionsFromDb);

            return Ok(submissionsDto);
        }
    }
}
