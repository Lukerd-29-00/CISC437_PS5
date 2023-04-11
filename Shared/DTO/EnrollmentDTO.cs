using DOOR.EF.Models;

using Microsoft.EntityFrameworkCore;

namespace DOOR.Shared.DTO
{
    public class EnrollmentPK
    {
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public int SchoolId { get; set; }
    }

    public class EnrollmentDTO : TrackableDTO, IDTO<Enrollment, EnrollmentPK>
    {
        public Enrollment ToRecord()
        {
            return new Enrollment
            {
                StudentId = StudentId,
                SchoolId = SchoolId,
                SectionId = SectionId,
                EnrollDate = EnrollDate,
                FinalGrade = FinalGrade,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate
            };
        }
        public EnrollmentPK primaryKey()
        {
            return new EnrollmentPK
            {
                StudentId = StudentId,
                SchoolId = SchoolId,
                SectionId = SectionId
            };
        }

        [Precision(8)]
        public int StudentId { get; set; }
        [Precision(8)]
        public int SectionId { get; set; }
        public DateTime EnrollDate { get; set; }
        [Precision(3)]
        public byte? FinalGrade { get; set; }

        [Precision(8)]
        public int SchoolId { get; set; }
    }
}
