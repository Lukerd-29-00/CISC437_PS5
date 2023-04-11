﻿using DOOR.EF.Data;
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

        protected override Expression<Func<Instructor, bool>> _getPredicate(InstructorPK Pkey)
        {
            return sp => (sp.InstructorId == Pkey.InstructorId) && (sp.SchoolId == Pkey.SchoolId);
        }
        [HttpGet]
        public async Task<IActionResult> GetInstructorByPK([FromQuery] int? InstructorId, [FromQuery] int? SchoolId)
        {
            if (InstructorId == null && SchoolId == null)
                return await _getHandler();
            else if (InstructorId == null || SchoolId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new InstructorPK { InstructorId = (int)InstructorId, SchoolId = (int)SchoolId });
        }

        [HttpPost]
        public async Task<IActionResult> PostInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            return await _postHandler(_InstructorDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            return await _putHandler(_InstructorDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInstructor([FromQuery] int? InstructorId, [FromQuery] int? SchoolId)
        {
            if (InstructorId != null && SchoolId != null)
                return await _deleteHandler(new InstructorPK { InstructorId = (int)InstructorId, SchoolId = (int)SchoolId });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}