using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Branch.API.Features.BranchCRUD.Delete
{
    public class DeleteBranchRequest : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBranchHanlder : AsyncRequestHandler<DeleteBranchRequest>
    {
        protected override Task Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

