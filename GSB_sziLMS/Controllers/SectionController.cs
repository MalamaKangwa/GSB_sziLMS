using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace GSB_sziLMS.Controllers
{
    [Route("api/v1/courses/{courseId}/sections")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SectionsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetSectionsForCourse(Guid courseId)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogError($"Course with id{courseId} doesn't exist in the database.");
                return NotFound();
            }

            var sectionForCourses = _repository.Section.GetSections(courseId, trackChanges: false);
            var sectionDto = _mapper.Map<IEnumerable<SectionDto>>(sectionForCourses);

            return Ok(sectionDto);
        }


        [HttpGet("{id}", Name = "GetSectionForCourse")]
        public IActionResult GetSectionForCourse(Guid courseId, Guid id)
        {
            var course = _repository.Course.GetCourse(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogError($"Course with id{courseId} doesn't exist in the database.");
                return NotFound();
            }

            var sections = _repository.Section.GetSection(courseId, id, trackChanges: false);
            if (sections == null)
            {
                _logger.LogError($"Section with id{id} doesn't exist in the database.");
                return NotFound();
            }

            var sectionsDto = _mapper.Map<SectionDto>(sections);
            return Ok(sectionsDto);
        }
    }
}
