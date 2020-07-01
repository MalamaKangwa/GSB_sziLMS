using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAssignments(Guid courseId, bool trackChanges);
        Assignment GetAssignment(Guid courseId, Guid id, bool trackChanges);
        IEnumerable<Assignment> GetAssignmentsByIds(Guid courseId, IEnumerable<Guid> ids, bool trackChanges);
        void CreateAssignmentForCourse(Guid Id, Assignment assignment);
        void DeleteAssignment(Assignment assignment);

    }
}
