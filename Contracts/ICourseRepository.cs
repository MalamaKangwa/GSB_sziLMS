using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses(bool trackChanges);
        Course GetCourse(Guid courseId, bool trackChanges);
        IEnumerable<Course> GetCoursesById(IEnumerable<Guid> ids, bool trackChanges);
        void CreateCourse(Course course);
        void DeleteCourse(Course course);

    }
}
