using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Campuses.Commands.EditCampus
{
    public class EditCampusCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class EditCampusCommandHandler : IRequestHandler<EditCampusCommand, Result<Unit>>
    {
        ISMSDbContext _dbContext;

        public EditCampusCommandHandler(ISMSDbContext smsDbContext)
        {
            _dbContext = smsDbContext;
        }

        public async Task<Result<Unit>> Handle(EditCampusCommand request, CancellationToken cancellationToken)
        {
            var campus = _dbContext.Campus
                .Where(x => x.EntityStatus != EntityStatus.Deleted)
                .SingleOrDefault(x => x.Id == request.Id);

            if (campus == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"Campus with id {request.Id} not found.");

            campus.Name = request.Name;
            campus.Address = request.Address;

            var entityResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityResult <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, "Faild to update the campus.");
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}