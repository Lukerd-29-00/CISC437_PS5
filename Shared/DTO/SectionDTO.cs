using DOOR.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DOOR.Shared.DTO
{
    public class SectionPK
    {
        public int SectionId { get; set; }
        public int SchoolId { get; set; }
    }

    public class SectionDTO : TrackableDTO,IDTO<Section, SectionPK>
    {
        public SectionPK primaryKey()
        {
            return new SectionPK
            {
                SectionId = SectionId,
                SchoolId = SchoolId
            };
        }
        public Section ToRecord()
        {
            return new Section
            {
                SectionId = SectionId,
                CourseNo = CourseNo,
                SectionNo = SectionNo,
                StartDateTime = StartDateTime,
                Location = Location,
                InstructorId = InstructorId,
                Capacity = Capacity,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate,
                SchoolId = SchoolId
            };
        }
        [Precision(8)]
        public int SectionId { get; set; }

        [Precision(8)]
        public int CourseNo { get; set; }

        [Precision(3)]
        public int SectionNo { get; set; }

        public DateTime? StartDateTime { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Location { get; set; }

        [Precision(8)]
        public int InstructorId { get; set; }

        [Precision(3)]
        public int? Capacity { get; set; }
        [Precision(8)]
        public int SchoolId { get; set; }

    }
}
