using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(RepositoryContext repositoryContext)
                : base(repositoryContext)
        {

        }

        public IEnumerable<Assignment> GetAssignments(Guid courseId, bool trackChanges) =>
            FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
                .OrderBy(e => e.Assignment_Name);

        public Assignment GetAssignment(Guid courseId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Assignment> GetAssignmentsByIds(Guid courseId, IEnumerable<Guid> ids, bool trackChnages) =>
            FindByCondition(x => ids.Contains(x.Id) && courseId == x.CourseId, trackChnages).ToList();

        public void CreateAssignmentForCourse(Guid Id, Assignment assignment)
        {
            assignment.CourseId = Id;
            Create(assignment);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            Delete(assignment);
        }

    }
}