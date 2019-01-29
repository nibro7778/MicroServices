using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;


namespace Branch.API.Features.BranchCRUD.Update
{
    public class UpdateBranchRequest : IRequest<UpdateBranchResponse>
    {
    }

    public class UpdateBranchResponse
    {
    }

    public class UpdateBranchHandler : IRequestHandler<UpdateBranchRequest, UpdateBranchResponse>
    {
        public Task<UpdateBranchResponse> Handle(UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
