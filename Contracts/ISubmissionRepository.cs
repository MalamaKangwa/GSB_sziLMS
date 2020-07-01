using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISubmissionRepository
    {
        IEnumerable<Submission> GetSubmissionsForAssignment(Guid assignmentId, bool trackChanges);
        IEnumerable<Submission> GetSubmissionsforEnrollment(Guid enrollmentId, bool trackChanges);
        Submission GetOneSubmissionForEnrollment(Guid enrollmentId, Guid id, bool trackChanges);
        Submission GetOneSubmissionForAssignment(Guid assignmentId, Guid id, bool trackChanges);
        void CreateSubmissionForAssignment(Guid assignmentId, Submission submission);
        void CreateSubmissionForEnrollment(Guid enrollmentId, Submission submission);
        void DeleteSubmission(Submission submission);

    }
}
