using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

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

    public class CreateBranchHandler : IRequestHandler<CreateBranchRequest, CreateBranchResponse>
    {
        public Task<CreateBranchResponse> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
