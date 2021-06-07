using Application.Common.Enums;
using Application.InstituteManagement.Campuses.Commands.CreateCampus;
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
    }
}
