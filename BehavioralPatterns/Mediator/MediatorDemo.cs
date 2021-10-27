using Autofac;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.BehavioralPatterns.Mediator
{
    public class MediatorDemo
    {
        //public static void Main(string [] args)
        //{
        //    //Problem with registration of Mediator type
        //    var cb = new ContainerBuilder();
        //}
    }

    public class PingCommand : IRequest<PongResponse>
    {

    }

    public class PongResponse
    {
        public DateTime TimeStamp;
        public PongResponse(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }
    }

    [UsedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.UtcNow))
                .ConfigureAwait(false);
        }
    }
}
