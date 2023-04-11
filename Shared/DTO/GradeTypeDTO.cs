using DOOR.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DOOR.Shared.DTO
{
    public class GradeTypePK
    {
        public int SchoolId { get; set; }
        public string GradeTypeCode { get; set; } = null!;
    }


    public class GradeTypeDTO : TrackableDTO, IDTO<GradeType, GradeTypePK>
    {
        public GradeTypePK primaryKey()
        {
            return new GradeTypePK
            {
                SchoolId = SchoolId,
                GradeTypeCode = GradeTypeCode
            };
        }

        public GradeType ToRecord()
        {
            return new GradeType
            {
                SchoolId = SchoolId,
                GradeTypeCode = GradeTypeCode,
                Description = Description,
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
        public string GradeTypeCode { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
    }
}
