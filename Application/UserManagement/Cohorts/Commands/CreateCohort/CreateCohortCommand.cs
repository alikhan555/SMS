using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities.User;

namespace Application.UserManagement.Cohorts.Commands.CreateCohort
{
    public class CreateCohortCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateCohortCommandHandler : IRequestHandler<CreateCohortCommand, Result<int>>
    {
        private ISMSDbContext _context { get; }
        private IUserManager _userManager { get; }

        public CreateCohortCommandHandler(
            ISMSDbContext context,
            IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<int>> Handle(CreateCohortCommand request, CancellationToken cancellationToken)
        {
            var cohort = new Cohort()
            {
                Name = request.Name,
                Description = request.Description,
                SchoolId = _userManager.GetCurrentSchoolId()
            };

            _context.Cohort.Add(cohort);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(cohort.Id);
        }
    }
}