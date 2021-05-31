using Application.InstituteManagement.Schools.Commands.CreateSchool;
using Application.InstituteManagement.Schools.Commands.DeleteSchool;
using Application.InstituteManagement.Schools.Commands.EditSchool;
using Application.InstituteManagement.Schools.Queries.GetSchoolDetails;
using Application.InstituteManagement.Schools.Queries.GetSchools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.API.Controllers.Institute
{
    public class SchoolController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetSchoolsQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetSchoolDetailsQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateSchoolCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, EditSchoolCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new DeleteSchoolCommand { Id = id }, cancellationToken));
        }
    }
}