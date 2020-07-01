using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext repositoryContext)
                : base(repositoryContext)
        {

        }

        public IEnumerable<Section> GetSections(Guid courseId, bool trackChanges) =>
            FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
                .OrderBy(e => e.CourseId);

        public Section GetSection(Guid courseId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateSectionForCourse(Guid Id, Section section)
        {
            section.CourseId = Id;
            Create(section);
        }

        public void DeleteSection(Section section)
        {
            Delete(section);
        }

        public IEnumerable<Section> GetSectionsById(Guid courseId, IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id) && x.CourseId == courseId, trackChanges).ToList();


    }
}