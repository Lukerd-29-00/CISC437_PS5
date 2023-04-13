using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Server.Controllers.Common;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : BaseController<Instructor, InstructorDTO, InstructorPK>
    {

        public InstructorController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Instructor> _getContext()
        {
            return _context.Instructors;
        }


        protected override Expression<Func<Instructor, InstructorDTO>> _getDTOExp()
        {
            return sp => new InstructorDTO
            {
                SchoolId = sp.SchoolId,
                InstructorId = sp.InstructorId,
                Salutation = sp.Salutation,
                FirstName = sp.FirstName,
                LastName = sp.LastName,
                StreetAddress = sp.StreetAddress,
                Zip = sp.Zip,
                Phone = sp.Phone,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
            };
        }
        protected override void _mutateRecord(Instructor original, InstructorDTO dto)
        {
            original.Salutation = dto.Salutation;
            original.FirstName = dto.FirstName;
            original.LastName = dto.LastName;
            original.StreetAddress = dto.StreetAddress;
            original.Zip = dto.Zip;
            original.Phone = dto.Phone;
        }
        protected override Expression<Func<Instructor, bool>> _getPredicate(InstructorPK Pkey)
        {
            return sp => (sp.InstructorId == Pkey.InstructorId) && (sp.SchoolId == Pkey.SchoolId);
        }

        [HttpGet]
        [Route("GetInstructor")]
        public async Task<IActionResult> GetInstructor()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetInstructor/{SchoolId}/{InstructorId}")]
        public async Task<IActionResult> GetInstructorByPK(int SchoolId, int InstructorId)
        {
            return await _getByPkHandler(new InstructorPK { InstructorId = InstructorId, SchoolId = SchoolId });
        }

        [HttpPost]
        [Route("PostInstructor")]
        public async Task<IActionResult> PostInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            return await _postHandler(_InstructorDTO);
        }

        [HttpPut]
        [Route("PutInstructor")]
        public async Task<IActionResult> PutInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            return await _putHandler(_InstructorDTO);
        }

        [HttpDelete]
        [Route("DeleteInstructor/{SchoolId}/{InstructorId}")]
        public async Task<IActionResult> DeleteInstructor(int SchoolId, int InstructorId)
        {
            return await _deleteHandler(new InstructorPK { InstructorId = InstructorId, SchoolId = SchoolId });
        }
    }
}