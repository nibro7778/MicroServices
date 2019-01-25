using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace Branch.API.Features.PingMe.Post
{

    public class DoEchoRequest : IRequest<DoEchoResponse>
    {
        public string Value { get; set; }
    }

    public class DoEchoResponse
    {
        public string Result { get; set; }
    }

    public class DoEchoValidator : AbstractValidator<DoEchoRequest>
    {
        public DoEchoValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
        }
    }

    public class DoEchoHandler : IRequestHandler<DoEchoRequest, DoEchoResponse>
    {
        public string Value { get; set; }

        public async Task<DoEchoResponse> Handle(DoEchoRequest request, CancellationToken cancellationToken)
        {
            return new DoEchoResponse()
            {
                Result = $"Hello World!!! {request.Value}"
            };
        }
    }
}
