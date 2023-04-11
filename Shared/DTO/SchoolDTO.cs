using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DOOR.EF.Models;
namespace DOOR.Shared.DTO
{
    public class SchoolDTO : TrackableDTO,IDTO <School,int>
    {
        public int primaryKey()
        {
            return SchoolId;
        }
        public School ToRecord()
        {
            return new School
            {
                SchoolId = SchoolId,
                SchoolName = SchoolName,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate
            };
        }
        [Precision(8)]
        public int SchoolId { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string SchoolName { get; set; } = null!;
    }
}
