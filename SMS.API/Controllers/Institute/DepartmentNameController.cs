using Application.Common.Enums;
using Application.InstituteManagement.DepartmentNames.Commands.ChangeDepartmentNameStatus;
using Application.InstituteManagement.DepartmentNames.Commands.CreateDepartmentName;
using Application.InstituteManagement.DepartmentNames.Commands.EditDepartmentName;
using Application.InstituteManagement.DepartmentNames.Queries.GetDepartmentNameDetails;
using Application.InstituteManagement.DepartmentNames.Queries.GetDepartmentNames;
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
    public class DepartmentNameController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetDepartmentNamesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetDepartmentNameDetailsQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<ActionResult> Create(CreateDepartmentNameCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Edit(int id, EditDepartmentNameCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("Status")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> ChangeStatus([FromQuery] ChangeDepartmentNameStatusCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }
    }
}
