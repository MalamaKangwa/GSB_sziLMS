using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext)
                : base(repositoryContext)
        {
        }

        public IEnumerable<Course> GetAllCourses(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Course_Name)
                .ToList();

        public Course GetCourse(Guid courseId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(courseId), trackChanges)
            .SingleOrDefault();


        public void CreateCourse(Course course) => Create(course);

        public void DeleteCourse(Course course) { Delete(course); }

        public IEnumerable<Course> GetCoursesById(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
    }
}

