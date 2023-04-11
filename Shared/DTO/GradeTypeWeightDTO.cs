using DOOR.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DOOR.EF.Models;

namespace DOOR.EF.DTO
{
    public class GradeTypeWeightPK
    {
        public int SchoolId { get; set; }
        public int SectionId { get; set; }
        public string GradeTypeCode { get; set; } = null!;
    }
    public class GradeTypeWeightDTO : TrackableDTO,IDTO<GradeTypeWeight, GradeTypeWeightPK>
    {

        public GradeTypeWeightPK primaryKey() 
        {
            return new GradeTypeWeightPK
            {
                SchoolId = SchoolId,
                SectionId = SectionId,
                GradeTypeCode = GradeTypeCode
            };
        } 
        public GradeTypeWeight ToRecord()
        {
            return new GradeTypeWeight
            {
                SchoolId = SchoolId,
                SectionId = SectionId,
                GradeTypeCode = GradeTypeCode,
                NumberPerSection = NumberPerSection,
                PercentOfFinalGrade = PercentOfFinalGrade,
                DropLowest = DropLowest,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate,
            };
        }
        [Precision(8)]
        public int SchoolId { get; set; }


        [Precision(8)]
        public int SectionId { get; set; }


        [StringLength(2)]
        [Unicode(false)]
        public string GradeTypeCode { get; set; } = null!;

        [Precision(3)]
        public byte NumberPerSection { get; set; }

        [Precision(3)]
        public byte PercentOfFinalGrade { get; set; }

        [Precision(1)]
        public bool DropLowest { get; set; }
    }
}
