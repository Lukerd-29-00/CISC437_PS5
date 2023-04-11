using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Server.Controllers.Common;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;
using static Duende.IdentityServer.Models.IdentityResources;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController<Student, StudentDTO, StudentPK>
    {

        public StudentController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Student> _getContext()
        {
            return _context.Students;
        }


        protected override Expression<Func<Student, StudentDTO>> _getDTOExp()
        {
            return sp => new StudentDTO
            {
                StudentId = sp.StudentId,
                FirstName = sp.FirstName,
                LastName = sp.LastName,
                StreetAddress = sp.StreetAddress,
                Zip = sp.Zip,
                Phone = sp.Phone,
                Employer = sp.Employer,
                RegistrationDate = sp.RegistrationDate,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
            };
        }

        protected override Expression<Func<Student, bool>> _getPredicate(StudentPK Pkey)
        {
            return sp => (sp.StudentId == Pkey.StudentId) && (sp.SchoolId == Pkey.SchoolId);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentByPK([FromQuery] int? StudentId, [FromQuery] int? SchoolId)
        {
            if (StudentId == null && SchoolId == null)
                return await _getHandler();
            else if (StudentId == null || SchoolId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new StudentPK { StudentId = (int)StudentId, SchoolId = (int)SchoolId });
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] StudentDTO _StudentDTO)
        {
            return await _postHandler(_StudentDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutStudent([FromBody] StudentDTO _StudentDTO)
        {
            return await _putHandler(_StudentDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromQuery] int? StudentId, [FromQuery] int? SchoolId)
        {
            if (StudentId != null && SchoolId != null)
                return await _deleteHandler(new StudentPK { StudentId = (int)StudentId, SchoolId = (int)SchoolId });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}