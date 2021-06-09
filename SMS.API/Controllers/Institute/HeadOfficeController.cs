using Application.Common.Enums;
using Application.InstituteManagement.HeadOffices.Commands.CreateHeadOffice;
using Application.InstituteManagement.HeadOffices.Commands.EditHeadOffice;
using Application.InstituteManagement.HeadOffices.Queries.GetHeadOffice;
using Application.InstituteManagement.HeadOffices.Queries.GetHeadOfficeDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.API.Controllers.Institute
{
    public class HeadOfficeController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Create(CreateHeadOfficeCommand model, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(model, cancellationToken));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Edit(int id, EditHeadOfficeCommand model, CancellationToken cancellationToken)
        {
            if (id != model.Id) return BadRequest();
            return ResultHandler(await Mediator.Send(model, cancellationToken));
        }

        [HttpGet]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetHeadOfficesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Role.SchoolOwner)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(new GetHeadOfficeDetailsQuery { Id = id }, cancellationToken));
        }

        //[HttpPut("Status")]
        //[Authorize(Roles = Role.SchoolOwner)]
        //public async Task<IActionResult> ChangeStatus(int id, int status, CancellationToken cancellationToken)
        //{
        //    return ResultHandler(await Mediator.Send(new ChangeCampusStatusCommand { Id = id, Status = status }, cancellationToken));
        //}
    }
}
