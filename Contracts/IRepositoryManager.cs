using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        ICourseRepository Course { get; }
        ISectionRepository Section { get; }
        IEnrollmentRepository Enrollment { get; }
        IAssignmentRepository Assignment { get; }
        ISubmissionRepository Submission { get; }
        void Save();
    }
}
