using Application.Common.Enums;
using Application.InstituteManagement.Schools.Commands.ChangeSchoolStatus;
using Application.InstituteManagement.Schools.Commands.CreateSchool;
using Application.InstituteManagement.Schools.Commands.EditSchool;
using Application.InstituteManagement.Schools.Queries.GetSchoolDetails;
using Application.InstituteManagement.Schools.Queries.GetSchools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.API.Controllers.Institute
{
    public class SchoolController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = Role.Developer)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetSchoolsQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.Developer)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetSchoolDetailsQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = Role.Developer)]
        public async Task<ActionResult> Create(CreateSchoolCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Developer)]
        public async Task<IActionResult> Edit(int id, EditSchoolCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("Status")]
        [Authorize(Roles = Role.Developer)]
        public async Task<IActionResult> ChangeStatus(int id, string status, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new ChangeSchoolStatusCommand { Id = id, status = status }, cancellationToken));
        }
    }
}