using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/courses/{courseId}/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AssignmentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAssignmentsForCourse(Guid courseId)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsFromDb = _repository.Assignment.GetAssignments(courseId, trackChanges: false);
            var assignmentsDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsFromDb);

            return Ok(assignmentsDto);
        }

        [HttpGet("{id}", Name = "GetAssignmentForCourse")]
        public IActionResult GetAssignmentForCourse(Guid courseId, Guid id)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsDb = _repository.Assignment.GetAssignment(courseId, id, trackChanges: false);
            if (assignmentsDb == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignments = _mapper.Map<AssignmentDto>(assignmentsDb);

            return Ok(assignments);

        }
    }
}