using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Course_Name { get; set; }
        public string Course_Description { get; set; }
    }
}
