using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace Branch.API.Features.BranchCRUD.Update
{
    public class UpdateBranchRequest : IRequest<UpdateBranchResponse>
    {
        [FromRoute]
        public string Id { get; set; }

        [FromBody]
        public UpdateBranchPayload Body { get; set; }

        public class UpdateBranchPayload
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string ContactNo { get; set; }
            public string Email { get; set; }
            
        }
    }

    public class UpdateBranchValidator : AbstractValidator<UpdateBranchRequest>
    {
        public UpdateBranchValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Body.Email).EmailAddress();
            RuleFor(x => x.Body.Name).NotEmpty();
        }
    }

    public class UpdateBranchResponse
    {
        public string Id { get; set; }
    }

    public class UpdateBranchHandler : IRequestHandler<UpdateBranchRequest, UpdateBranchResponse>
    {
        readonly IAsyncDocumentSession _session;

        public UpdateBranchHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task<UpdateBranchResponse> Handle(UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            var branch = await _session.LoadAsync<Model.Branch>(request.Id, cancellationToken);

            branch.Name = request.Body.Name;
            branch.Address = string.IsNullOrWhiteSpace(request.Body.Address) ? null : request.Body.Address;
            branch.ContactNo = string.IsNullOrWhiteSpace(request.Body.ContactNo) ? null : request.Body.ContactNo;
            branch.Email = string.IsNullOrWhiteSpace(request.Body.Email) ? null : request.Body.Email;

            await _session.StoreAsync(branch, request.Id, cancellationToken);
            await _session.SaveChangesAsync(cancellationToken);

            return new UpdateBranchResponse()
            {
                Id = request.Id
            };

        }
    }
}
