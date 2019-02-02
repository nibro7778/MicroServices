using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Raven.Client.Documents.Session;

namespace Branch.API.Features.BranchCRUD.Get
{
    public class GetBranchRequest : IRequest<GetBranchResponse>
    {
        public string Id { get; set; }
    }

    public class GetBranchResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
    }

    public class GetBranchValidator : AbstractValidator<GetBranchRequest>
    {
        public GetBranchValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class GetBranchHandler : IRequestHandler<GetBranchRequest, GetBranchResponse>
    {
        IAsyncDocumentSession _session;

        public GetBranchHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task<GetBranchResponse> Handle(GetBranchRequest request, CancellationToken cancellationToken)
        {
            var branch = await _session.LoadAsync<Model.Branch>(request.Id);

            return new GetBranchResponse()
            {
                Id = branch.Id,
                Address = branch.Address,
                ContactNo = branch.ContactNo,
                Email = branch.Email
            };
        }
    }
}
