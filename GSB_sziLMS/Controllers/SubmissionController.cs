using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjectsForCreation;
using Entities.DataTransferObjectsForUpdate;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult CreateSubmissionForEnrollment(Guid userId, Guid enrollmentId, [FromBody]SubmissionForCreationDto submission)
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

            var submissionEntity = _mapper.Map<Submission>(submission);

            _repository.Submission.CreateSubmissionForEnrollment(enrollmentId, submissionEntity);
            _repository.Save();

            var submissionToReturn = _mapper.Map<SubmissionDto>(submissionEntity);

            return CreatedAtRoute("GetSubmissionForEnrollment",
                new { userId, enrollmentId, id = submissionToReturn.Id }, submissionToReturn);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateSubmission(Guid userId, Guid enrollmentId, Guid id, [FromBody] SubmissionForUpdateDto submission)
        {
            if (submission == null)
            {
                _logger.LogError("SubmissionForUpdateDto object sent from client is null.");
                return BadRequest("SubmisionForUpdateDto object is null");
            }

            var user = _repository.User.GetUser(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollment = _repository.Enrollment.GetOneEnrollmentForUser(userId, enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionsEntity = _repository.Submission.GetOneSubmissionForEnrollment(enrollmentId, id, trackChanges: false);
            if (submissionsEntity == null)
            {
                _logger.LogInfo($"Submission with id: {id} doesn't exist in the database.");
                return NotFound();
            }


            _mapper.Map(submission, submissionsEntity);
            _repository.Save();

            var submissionToReturn = _mapper.Map<SubmissionDto>(submissionsEntity);

            return CreatedAtRoute("GetSubmissionForEnrollment",
                new { userId, enrollmentId, id = submissionToReturn.Id }, submissionToReturn);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteSubmissionForEnrollment(Guid userId, Guid enrollmentId, Guid id)
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
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionEntity = _repository.Submission.GetOneSubmissionForEnrollment(enrollmentId, id, trackChanges: false);
            if (submissionEntity == null)
            {
                _logger.LogInfo($"Submission with id: {id} doesn't exist in the database.");
                return NotFound();
            }



            _repository.Submission.DeleteSubmission(submissionEntity);
            _repository.Save();

            return NoContent();
        }




    }

}
