using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SubmissionRepository : RepositoryBase<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public Submission GetOneSubmissionForAssignment(Guid assignmentId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.AssignmentId.Equals(assignmentId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public Submission GetOneSubmissionForEnrollment(Guid enrollmentId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.EnrollmentId.Equals(enrollmentId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Submission> GetSubmissionsForAssignment(Guid assignmentId, bool trackChanges) =>
            FindByCondition(e => e.AssignmentId.Equals(assignmentId), trackChanges)
                .OrderBy(e => e.AssignmentId);

        public IEnumerable<Submission> GetSubmissionsforEnrollment(Guid enrollmentId, bool trackChanges) =>
            FindByCondition(e => e.EnrollmentId.Equals(enrollmentId), trackChanges)
                .OrderBy(e => e.EnrollmentId);

        public void CreateSubmissionForAssignment(Guid assignmentId, Submission submission)
        {
            submission.AssignmentId = assignmentId;
            Create(submission);
        }
        public void CreateSubmissionForEnrollment(Guid enrollmentId, Submission submission)
        {
            submission.EnrollmentId = enrollmentId;
            Create(submission);
        }
        public void DeleteSubmission(Submission submission)
        {
            Delete(submission);
        }

    }
}
