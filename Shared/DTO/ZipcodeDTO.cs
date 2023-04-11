using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DOOR.EF.Models;

namespace DOOR.Shared.DTO
{
    public class ZipcodeDTO : TrackableDTO,IDTO<Zipcode,string>
    {
        public string primaryKey()
        {
            return Zip;
        }
        public Zipcode ToRecord()
        {
            return new Zipcode
            {
                Zip = Zip,
                City = City,
                State = State,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate,
            };
        }
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;

        [StringLength(25)]
        [Unicode(false)]
        public string? City { get; set; }

        [StringLength(2)]
        [Unicode(false)]
        public string? State { get; set; }
    }
}
