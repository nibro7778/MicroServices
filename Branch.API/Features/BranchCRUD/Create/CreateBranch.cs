using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Raven.Client.Documents.Session;
using Branch;
using FluentValidation;

namespace Branch.API.Features.BranchCRUD.Create
{
    public class CreateBranchRequest : IRequest<CreateBranchResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
    }

    public class CreateBranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
    }

    public class CreateBranchValidator : AbstractValidator<CreateBranchRequest>
    {
        public CreateBranchValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x).NotEmpty();
        }
    }

    public class CreateBranchHandler : IRequestHandler<CreateBranchRequest, CreateBranchResponse>
    {
        readonly IAsyncDocumentSession _session;

        public CreateBranchHandler(IAsyncDocumentSession session)
        {
            _session = session;
        }
        
        public async Task<CreateBranchResponse> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
        {
            var branch = new Branch.Model.Branch()
            {
               Name = request.Name,
               Address = request.Address,
               ContactNo = request.ContactNo
            };

            await _session.StoreAsync(branch, cancellationToken);
            await _session.SaveChangesAsync(cancellationToken);
            var version = _session.Advanced.GetChangeVectorFor(branch);

            return new CreateBranchResponse
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                ContactNo = branch.ContactNo
            };
        }
    }
}
