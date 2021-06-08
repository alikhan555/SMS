using Application.Common.Enums;
using Application.InstituteManagement.Campuses.Commands.ChangeCampusStatus;
using Application.InstituteManagement.Campuses.Commands.CreateCampus;
using Application.InstituteManagement.Campuses.Commands.EditCampus;
using Application.InstituteManagement.Campuses.Queries.GetCampusDetails;
using Application.InstituteManagement.Campuses.Queries.GetCampuses;
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
    public class CampusController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Create(CreateCampusCommand model, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(model, cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Edit(int id, EditCampusCommand model, CancellationToken cancellationToken)
        {
            if (id != model.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(model, cancellationToken));
        }

        [HttpGet]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetCampusesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetCampusDetailsQuery{ Id = id }, cancellationToken));
        }

        [HttpPut("Status")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> ChangeStatus(int id, int status, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new ChangeCampusStatusCommand { Id = id, Status = status }, cancellationToken));
        }
    }
}