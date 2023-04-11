using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DOOR.EF.Models;

namespace DOOR.Shared.DTO
{
    public class StudentPK
    {
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
    }

    public class StudentDTO : TrackableDTO,IDTO<Student,StudentPK>
    {

        public StudentPK primaryKey()
        {
            return new StudentPK
            {
                SchoolId = SchoolId,
                StudentId = StudentId
            };
        }
        public Student ToRecord()
        {
            return new Student
            {
                StudentId = StudentId,
                FirstName = FirstName,
                LastName = LastName,
                StreetAddress = StreetAddress,
                Zip = Zip,
                Phone = Phone,
                Employer = Employer,
                RegistrationDate = RegistrationDate,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate,
                SchoolId = SchoolId
            };
        }
        [Precision(8)]
        public int StudentId { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string? Salutation { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? FirstName { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? StreetAddress { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Employer { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Precision(8)]
        public int SchoolId { get; set; }
    }
}
