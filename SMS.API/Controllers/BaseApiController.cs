using Application.Common.Enums;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace SMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult ResultHandler<T>(Result<T> result)
        {
            if (result.HttpStatus == HttpStatus.OK)
                if (result.Data != null)
                    return Ok(result.Data);
                else
                    return Ok();

            if (result.HttpStatus == HttpStatus.BadRequest)
                return BadRequest(result.Errors);

            if (result.HttpStatus == HttpStatus.Unauthorized)
                return Unauthorized(result.Errors);

            if (result.HttpStatus == HttpStatus.Forbidden)
                return Forbid();

            if (result.HttpStatus == HttpStatus.NotFound)
                return NotFound(result.Errors);

            throw new Exception("Unhandled result exception.");
        }
    }
}
