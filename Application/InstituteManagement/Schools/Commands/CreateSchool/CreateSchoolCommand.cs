﻿using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Common.Enums;
using Domain.Entities.Institute;
using Domain.Entities.User;

namespace Application.InstituteManagement.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Initial { get; set; }
        public string NTN { get; set; }

        public string OwnerEmail { get; set; }
        public string OwnerPassword { get; set; }
    }

    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, Result<int>>
    {
        private ISMSDbContext _context { get; }
        private IDateTime _datetime { get; }
        private IUserManager _userManager { get; }  

        public CreateSchoolCommandHandler(
            ISMSDbContext context,
            IDateTime dateTime,
            IUserManager userManager)
        {
            _context = context;
            _datetime = dateTime;
            _userManager = userManager;
        }

        public async Task<Result<int>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            //List<string> validationErrors = new List<string>();

            //if (_context.Schools.Any(x => x.Name == request.Name))
            //    validationErrors.Add("School Name must be unique.");

            //if (_context.Schools.Any(x => x.Initial == request.Initial))
            //    validationErrors.Add("School initial must be unique.");

            //if (_context.Schools.Any(x => x.NTN == request.NTN))
            //    validationErrors.Add("School NTN. must be unique.");

            //if (validationErrors.Count > 0) 
            //{
            //    return Result<Unit>.Failure(HttpStatus.BadRequest, validationErrors);
            //}
            //else
            //{
            //    var school = new School()
            //    {
            //        Name = request.Name,
            //        Initial = request.Initial,
            //        NTN = request.NTN
            //    };

            //    _context.Schools.Add(school);
            //    var result = await _context.SaveChangesAsync(cancellationToken);

            //    if (result <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, "Failed to save the school.");

            //    return Result<Unit>.Success(Unit.Value);
            //}

            
            // create new school
            var school = new School()
            {
                Name = request.Name,
                Initial = request.Initial,
                NTN = request.NTN
            };

            _context.Schools.Add(school);
            var entityResult = await _context.SaveChangesAsync(cancellationToken);
            if (entityResult <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "Failed to save the school.");

            // create school owner with schoolId / tenantId
            var owner = new AppUser
            {
                Email = request.OwnerEmail,
                UserName = request.OwnerEmail,
                SchoolId = school.Id,
                CampusId = 0,
                EntityStatus = EntityStatus.Active,
                Created = _datetime.UtcNow,
                CreatedBy = _userManager.GetCurrentUserId()
            };

            var identityResult = await _userManager.CreateUserAsync(owner, request.OwnerPassword, new List<string> { Role.SchoolOwner });
            if (!identityResult.Succeeded) return Result<int>.Failure(HttpStatus.BadRequest, "School owner could not be created.");

            // bind school with school owner
            school = _context.Schools.Find(school.Id);
            school.OwnerId = identityResult.Data;
            entityResult = await _context.SaveChangesAsync(cancellationToken);
            if (entityResult <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "School owner could not bind with school.");

            return Result<int>.Success(school.Id);
        }
    }
}