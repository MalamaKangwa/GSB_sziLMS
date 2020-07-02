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

namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]

    public class CoursesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CoursesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetCourses()
        {
            try
            {
                var courses = _repository.Course.GetAllCourses(trackChanges: false);
                var courseDto = _mapper.Map<IEnumerable<CourseDto>>(courses);
                return Ok(courses);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCourses)} action {ex}");
                return Ok("Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "CourseById")]
        public IActionResult GetCourse(Guid id)
        {
            var course = _repository.Course.GetCourse(id, trackChanges: false);
            if (course == null) { _logger.LogInfo($"Course with id: {id} doesn't exist in the database."); return NotFound(); }
            else
            {
                var courseDto = _mapper.Map<CourseDto>(course);
                return Ok(courseDto);
            }
        }


        
        [HttpPost]
        public IActionResult CreateCourse([FromBody]CourseForCreationDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForCreationDto object sent from this client is null.");
                return BadRequest("CourseForCreationDto object is null");
            }

            var courseEntity = _mapper.Map<Course>(course);

            _repository.Course.CreateCourse(courseEntity);
            _repository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("CourseById", new { id = courseToReturn.Id }, courseToReturn);

        }


        
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(Guid id, [FromBody] CourseForUpdateDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForUpdateDto object sent from client is null.");
                return BadRequest("CourseForUpdateDto object is null");
            }

            var courseEntity = _repository.Course.GetCourse(id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(course, courseEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateCourse(Guid id,
        [FromBody] JsonPatchDocument<CourseForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var courseEntity = _repository.Course.GetCourse(id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseEntity);
            patchDoc.ApplyTo(courseToPatch);

            _mapper.Map(courseToPatch, courseEntity);
            _repository.Save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(Guid id)
        {
            var course = _repository.Course.GetCourse(id, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Course.DeleteCourse(course);
            _repository.Save();

            return NoContent();
        }
    }
}
