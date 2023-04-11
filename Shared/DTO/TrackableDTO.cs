using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DOOR.Shared.DTO
{
    public abstract class TrackableDTO
    {
        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TrackableDTO()
        {
            CreatedBy = "UD_LUCASD";
            CreatedDate = DateTime.UtcNow;
            ModifiedBy = "UD_LUCASD";
            ModifiedDate = DateTime.UtcNow;
        }

    }
}
