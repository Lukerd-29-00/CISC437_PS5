using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DOOR.EF.Models;
namespace DOOR.Shared.DTO
{
    public class InstructorPK
    {
        public int SchoolId { get; set; }
        public int InstructorId { get; set; }
    }
    public class InstructorDTO : TrackableDTO,IDTO<Instructor,InstructorPK>
    {
        public InstructorPK primaryKey()
        {
            return new InstructorPK
            {
                SchoolId = SchoolId,
                InstructorId = InstructorId
            };     
        }
        public Instructor ToRecord()
        {
            return new Instructor
            {
                SchoolId = SchoolId,
                InstructorId = InstructorId,
                Salutation = Salutation,
                FirstName = FirstName,
                LastName = LastName,
                StreetAddress = StreetAddress,
                Zip = Zip,
                Phone = Phone,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate,
            };
        }
        [Precision(8)]
        public int SchoolId { get; set; }


        [Precision(8)]
        public int InstructorId { get; set; }

        [StringLength(5)]
        [Unicode(false)]
        public string? Salutation { get; set; }

        [StringLength(25)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;

        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;

        [StringLength(50)]
        [Unicode(false)]
        public string StreetAddress { get; set; } = null!;

        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;

        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }

    }
}
