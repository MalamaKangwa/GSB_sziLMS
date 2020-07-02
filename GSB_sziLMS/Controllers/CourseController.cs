using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjectsForCreation;
using Entities.DataTransferObjectsForUpdate;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]

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


        [HttpGet("collection/({ids})", Name = "CourseCollection")]
        public IActionResult GetCourseCollection([ModelBinder(BinderType =
            typeof(ArrayModelBinders))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var courseEntities = _repository.Course.GetCoursesById(ids, trackChanges: false);

            if (ids.Count() != courseEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var coursesToReturn = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
            return Ok(coursesToReturn);
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


        [HttpPost("collection")]
        public IActionResult CreateCourseCollection([FromBody] IEnumerable<CourseForCreationDto> courseCollection)
        {
            if (courseCollection == null)
            {
                _logger.LogError("Course collection sent from client is null.");
                return BadRequest("Course collection is null");
            }

            var courseEntities = _mapper.Map<IEnumerable<Course>>(courseCollection);
            foreach (var course in courseEntities)
            {
                _repository.Course.CreateCourse(course);
            }

            _repository.Save();

            var courseCollectionToReturn = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
            var ids = string.Join(",", courseCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("CourseCollection", new { ids }, courseCollectionToReturn);
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
