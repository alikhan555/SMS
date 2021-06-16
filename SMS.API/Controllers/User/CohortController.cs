using Application.Common.Enums;
using Application.UserManagement.Cohorts.Commands.CreateCohort;
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
        //[HttpGet]
        //[Authorize(Roles = Role.Developer)]
        //public async Task<IActionResult> Get(CancellationToken cancellationToken)
        //{
        //    return ResultHandler(await Mediator.Send(new GetCohortsQuery(), cancellationToken));
        //}

        //[HttpGet("{id}")]
        //[Authorize(Roles = Role.Developer)]
        //public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        //{
        //    return ResultHandler(await Mediator.Send(new GetCohortDetailsQuery { Id = id }, cancellationToken));
        //}

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

        //[HttpPut("Status")]
        //[Authorize(Roles = Role.Developer)]
        //public async Task<IActionResult> ChangeStatus([FromQuery] ChangeCohortStatusCommand command, CancellationToken cancellationToken)
        //{
        //    return ResultHandler(await Mediator.Send(command, cancellationToken));
        //}
    }
}