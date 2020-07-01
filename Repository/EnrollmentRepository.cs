using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public Enrollment GetOneEnrollmentForUser(Guid userId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();
        public Enrollment GetOneEnrollmentForSection(Guid sectionId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.SectionId.Equals(sectionId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Enrollment> GetEnrollmentsForSection(Guid sectionId, bool trackChanges) =>
            FindByCondition(e => e.SectionId.Equals(sectionId), trackChanges)
                .OrderBy(e => e.SectionId);

        public IEnumerable<Enrollment> GetEnrollmentsForUser(Guid userId, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId), trackChanges)
                .OrderBy(e => e.UserId);

        public void CreateEnrollmentForSection(Guid sectionId, Enrollment enrollment)
        {
            enrollment.SectionId = sectionId;
            Create(enrollment);
        }
        public void CreateEnrollmentForUser(Guid userId, Enrollment enrollment)
        {
            enrollment.UserId = userId;
            Create(enrollment);
        }
        public void DeleteEnrollment(Enrollment enrollment)
        {
            Delete(enrollment);
        }
    }
}
