using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ordering.Application.Common.Behaviours
{
    public class PerformanceBehavious<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavious(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapseMilisenconds = _timer.ElapsedMilliseconds;

            if (elapseMilisenconds <= 500) return response;
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Application Long Running Request:{Name} ({ElapseMilisenconds}) milliseconds {@Request}", requestName, elapseMilisenconds);
            
            return response;
        }
    }
}
