using DOOR.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DOOR.Shared.DTO
{
    public class CoursePK
    {
        public int SchoolId { get; set; }
        public int CourseNo { get; set; }
    }
    public class CourseDTO : TrackableDTO,IDTO<Course, CoursePK>
    {
        public CoursePK primaryKey()
        {
            return new CoursePK
            {
                SchoolId = SchoolId,
                CourseNo = CourseNo,
            };
        }
        public Course ToRecord()
        {
            return new Course
            {
                CourseNo = CourseNo,
                Description = Description,
                Cost = Cost,
                Prerequisite = Prerequisite,
                SchoolId = SchoolId,
                PrerequisiteSchoolId = PrerequisiteSchoolId,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedDate = ModifiedDate,
                ModifiedBy = ModifiedBy
            };
        }
        [Precision(8)]
        public int SchoolId { get; set; }
        public int CourseNo { get; set; }
        [StringLength(50)]
        public string Description { get; set; } = null!;
        public decimal? Cost { get; set; }
        public int? Prerequisite { get; set; }
        public int? PrerequisiteSchoolId { get; set; }

    }
}
