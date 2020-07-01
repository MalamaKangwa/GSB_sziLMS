using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetSections(Guid courseId, bool trackChanges);
        IEnumerable<Section> GetSectionsById(Guid courseId, IEnumerable<Guid> ids, bool trackChanges);
        Section GetSection(Guid courseId, Guid id, bool trackChanges);
        void CreateSectionForCourse(Guid Id, Section section);
        void DeleteSection(Section section);

    }
}
