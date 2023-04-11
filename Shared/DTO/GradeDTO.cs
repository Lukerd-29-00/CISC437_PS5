using DOOR.EF.Models;
using DOOR.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DOOR.Shared.DTO
{
    public class GradePK
    {
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public string GradeTypeCode { get; set; } = null!;
        public byte GradeCodeOccurance { get; set; }
    }
    public class GradeDTO : TrackableDTO,IDTO<Grade, GradePK>
    {
        public GradePK primaryKey()
        {
            return new GradePK
            {
                SchoolId = SchoolId,
                StudentId = StudentId,
                SectionId = SectionId,
                GradeTypeCode = GradeTypeCode,
                GradeCodeOccurance = GradeCodeOccurrence,

            };
        }

        public Grade ToRecord()
        {
            return new Grade
            {
                SchoolId = SchoolId,
                StudentId = StudentId,
                SectionId = SectionId,
                GradeTypeCode = GradeTypeCode,
                GradeCodeOccurrence = GradeCodeOccurrence,
                NumericGrade = NumericGrade,
                Comments = Comments,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate
            };
        }

        [Precision(8)]
        public int SchoolId { get; set; }
        [Precision(8)]
        public int StudentId { get; set; }
        [Precision(8)]
        public int SectionId { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string GradeTypeCode { get; set; } = null!;
        [Precision(3)]
        public byte GradeCodeOccurrence { get; set; }
        public decimal NumericGrade { get; set; }
        public string? Comments { get; set; }
    }
}
