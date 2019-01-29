using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Branch.API.Features.BranchCRUD.Get
{
    public class GetBranchRequest : IRequest<GetBranchResponse>
    {
        public int Id { get; set; }
    }

    public class GetBranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
    }

    public class GetBranchHandler : IRequestHandler<GetBranchRequest, GetBranchResponse>
    {
        public Task<GetBranchResponse> Handle(GetBranchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
