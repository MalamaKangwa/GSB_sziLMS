using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEnrollmentRepository
    {
        IEnumerable<Enrollment> GetEnrollmentsForSection(Guid sectionId, bool trackChanges);
        IEnumerable<Enrollment> GetEnrollmentsForUser(Guid userId, bool trackChanges);
        Enrollment GetOneEnrollmentForSection(Guid sectionId, Guid id, bool trackChanges);
        Enrollment GetOneEnrollmentForUser(Guid userId, Guid id, bool trackChanges);
        void CreateEnrollmentForSection(Guid sectionId, Enrollment enrollment);
        void CreateEnrollmentForUser(Guid userId, Enrollment enrollment);
        void DeleteEnrollment(Enrollment enrollment);
    }
}
