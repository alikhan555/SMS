using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.Cohorts.Commands.ChangeCohortStatus
{
    public class ChangeCohortStatusCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }

    public class ChangeCohortStatusCommandHandler : IRequestHandler<ChangeCohortStatusCommand, Result<Unit>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public ChangeCohortStatusCommandHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(ChangeCohortStatusCommand request, CancellationToken cancellationToken)
        {
            var cohort = _dbContext.Cohort
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (cohort == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"Cohort not found.");

            cohort.EntityStatus = request.Status;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
