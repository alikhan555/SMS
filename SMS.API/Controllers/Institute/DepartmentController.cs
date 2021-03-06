using Application.Common.Enums;
using Application.InstituteManagement.Departments.Commands.ChangeDepartmentStatus;
using Application.InstituteManagement.Departments.Commands.CreateDepartment;
using Application.InstituteManagement.Departments.Commands.EditDepartment;
using Application.InstituteManagement.Departments.Queries.GetDepartment;
using Application.InstituteManagement.Departments.Queries.GetDepartmentDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.API.Controllers.Institute
{
    public class DepartmentController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetDepartmentsQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetDepartmentDetailsQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<ActionResult> Create(CreateDepartmentCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Edit(int id, EditDepartmentCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("Status")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> ChangeStatus([FromQuery] ChangeDepartmentStatusCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }
    }
}
