using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Raven.Client.Documents.Session;

namespace Branch.API.Features.BranchCRUD.Delete
{
    public class DeleteBranchRequest : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBranchValidator : AbstractValidator<DeleteBranchRequest>
    {
        IAsyncDocumentSession _session;

        public DeleteBranchValidator(IAsyncDocumentSession session)
        {
            _session = session;

            RuleFor(x => x.Id).NotEmpty();

        }
    }

    public class DeleteBranchHanlder : AsyncRequestHandler<DeleteBranchRequest>
    {
        IAsyncDocumentSession _session;

        public DeleteBranchHanlder(IAsyncDocumentSession session)
        {
            _session = session;
        }

        protected override async Task Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
        {
            _session.Delete(request.Id);
            await _session.SaveChangesAsync(cancellationToken);
        }
    }
}

