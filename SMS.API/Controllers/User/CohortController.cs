using Application.Common.Enums;
using Application.InstituteManagement.Cohorts.Commands.EditCohort;
using Application.UserManagement.Cohorts.Commands.ChangeCohortStatus;
using Application.UserManagement.Cohorts.Commands.CreateCohort;
using Application.UserManagement.Cohorts.Queries.GetCohortDetails;
using Application.UserManagement.Cohorts.Queries.GetCohorts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.API.Controllers.User
{
    public class CohortController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetCohortsQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetCohortDetailsQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<ActionResult> Create(CreateCohortCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Edit(int id, EditCohortCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("Status")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> ChangeStatus([FromQuery] ChangeCohortStatusCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }
    }
}