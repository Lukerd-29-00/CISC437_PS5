using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DOOR.Server.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Numerics;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using Telerik.SvgIcons;
using System.Linq.Expressions;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController<Course, CourseDTO, int>
    {
        public CourseController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Course> _getContext()
        {
            return _context.Courses;
        }


        protected override Expression<Func<Course, CourseDTO>> _getDTOExp()
        {
            return sp => new CourseDTO
            {
                Cost = sp.Cost,
                CourseNo = sp.CourseNo,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                Description = sp.Description,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
                Prerequisite = sp.Prerequisite
            };
        }

        protected override Expression<Func<Course, bool>> _getPredicate(int CourseNo)
        {
            return c => c.CourseNo == CourseNo;
        }

        protected override int _extract_Pkey(CourseDTO data)
        {
            return data.CourseNo;
        }

        protected override Course _createRecord(CourseDTO obj)
        {
            return new Course
            {
                Cost = obj.Cost,
                CourseNo = obj.CourseNo,
                CreatedBy = obj.CreatedBy,
                CreatedDate = obj.CreatedDate,
                Description = obj.Description,
                ModifiedBy = obj.ModifiedBy,
                ModifiedDate = obj.ModifiedDate,
                Prerequisite = obj.Prerequisite
            };
        }
        protected override void _mutateRecord(Course record, CourseDTO newRecord)
        {
            record.Cost = newRecord.Cost;
            record.Description = newRecord.Description;
            record.Prerequisite = newRecord.Prerequisite;
        }

        [HttpGet]
        [Route("GetCourse")]
        public async Task<IActionResult> GetCourse()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetCourse/{_CourseNo}")]
        public async Task<IActionResult> GetCourse(int _CourseNo)
        {
            return await _getByPkHandler(_CourseNo);
        }

        [HttpPost]
        [Route("PostCourse")]
        public async Task<IActionResult> PostCourse([FromBody] CourseDTO _CourseDTO)
        {
            return await _postHandler(_CourseDTO);
        }

        [HttpPut]
        [Route("PutCourse")]
        public async Task<IActionResult> PutCourse([FromBody] CourseDTO _CourseDTO)
        {
            return await _putHandler(_CourseDTO);
        }

        [HttpDelete]
        [Route("DeleteCourse/{_CourseNo}")]
        public async Task<IActionResult> DeleteCourse(int _CourseNo)
        {
            return await _deleteHandler(_CourseNo);
        }
    }
}