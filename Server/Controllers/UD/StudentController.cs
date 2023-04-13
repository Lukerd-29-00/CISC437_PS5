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
        protected override void _mutateRecord(Student original, StudentDTO dto)
        {
            original.FirstName = dto.FirstName;
            original.LastName = dto.LastName;
            original.StreetAddress = dto.StreetAddress;
            original.Zip = dto.Zip;
            original.Phone = dto.Phone;
            original.Employer = dto.Employer;
            original.RegistrationDate = dto.RegistrationDate;
        }
        protected override Expression<Func<Student, bool>> _getPredicate(StudentPK Pkey)
        {
            return sp => (sp.StudentId == Pkey.StudentId) && (sp.SchoolId == Pkey.SchoolId);
        }

        [HttpGet]
        [Route("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetStudent/{SchoolId}/{StudentId}")]
        public async Task<IActionResult> GetStudentByPK(int StudentId,int SchoolId)
        {
             return await _getByPkHandler(new StudentPK { StudentId = StudentId, SchoolId = SchoolId });
        }

        [HttpPost]
        [Route("PostStudent")]
        public async Task<IActionResult> PostStudent([FromBody] StudentDTO _StudentDTO)
        {
            return await _postHandler(_StudentDTO);
        }

        [HttpPut]
        [Route("PutStudent")]
        public async Task<IActionResult> PutStudent([FromBody] StudentDTO _StudentDTO)
        {
            return await _putHandler(_StudentDTO);
        }

        [HttpDelete]
        [Route("DeleteStudent/{SchoolId}/{StudentId}")]
        public async Task<IActionResult> DeleteStudent(int SchoolId, int StudentId)
        {
            return await _deleteHandler(new StudentPK { StudentId = StudentId, SchoolId = SchoolId });
        }
    }
}