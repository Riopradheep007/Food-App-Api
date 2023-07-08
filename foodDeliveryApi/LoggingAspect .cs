using PostSharp.Aspects;
using Microsoft.Extensions.Logging;

namespace foodDeliveryApi
{

    [Serializable]
    public class LoggingAspect : OnMethodBoundaryAspect
    {
        private readonly ILogger _logger;

        public LoggingAspect(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _logger.LogInformation($"Entering method {args.Method.Name}.");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _logger.LogInformation($"Exiting method {args.Method.Name}.");
        }

        public override void OnException(MethodExecutionArgs args)
        {
            _logger.LogError(args.Exception, $"Exception in method {args.Method.Name}.");
        }
    }
}
