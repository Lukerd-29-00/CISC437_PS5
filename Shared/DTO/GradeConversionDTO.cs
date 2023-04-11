using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DOOR.EF.Models;

namespace DOOR.Shared.DTO
{
    public class GradeConversionPK
    {
        public int SchoolId { get; set; }
        public string LetterGrade { get; set; } = null!;
    }
    public class GradeConversionDTO : TrackableDTO,IDTO<GradeConversion, GradeConversionPK>
    {
        public GradeConversionPK primaryKey()
        {
            return new GradeConversionPK
            {
                SchoolId = SchoolId,
                LetterGrade = LetterGrade
            };

        }
        public GradeConversion ToRecord()
        {
            return new GradeConversion
            {
                SchoolId = SchoolId,
                LetterGrade = LetterGrade,
                GradePoint = GradePoint,
                MaxGrade = MaxGrade,
                MinGrade = MinGrade,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate
            };
        }
        [Precision(8)]
        public int SchoolId { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string LetterGrade { get; set; } = null!;
        public decimal GradePoint { get; set; }
        [Precision(3)]
        public byte MaxGrade { get; set; }
        [Precision(3)]
        public byte MinGrade { get; set; }
    }
}
