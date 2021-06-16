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

namespace Application.InstituteManagement.Cohorts.Commands.EditCohort
{
    public class EditCohortCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditCohortCommandHandler : IRequestHandler<EditCohortCommand, Result<int>>
    {
        ISMSDbContext _context { get; }
        IUserManager _userManager { get; }

        public EditCohortCommandHandler(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<int>> Handle(EditCohortCommand request, CancellationToken cancellationToken)
        {
            var cohort = _context.Cohort
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus == EntityStatus.Active &&
                    x.Id == request.Id);

            if (cohort == null) return Result<int>.Failure(HttpStatus.NotFound, $"Cohort with id: {request.Id} not found.");

            cohort.Name = request.Name;
            cohort.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(cohort.Id);
        }
    }
}