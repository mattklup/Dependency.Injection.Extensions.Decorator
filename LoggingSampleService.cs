using System.IO;

namespace Dependency.Injection.Extensions.Decorator
{
    public class LoggingSampleService : ISampleService
    {
        private readonly ISampleService service;
        private readonly TextWriter logger;

        public LoggingSampleService(ISampleService innerService, TextWriter logger)
        {
            this.service = innerService;
            this.logger = logger;
        }

        public void Process(int number)
        {
            this.logger.WriteLine($"LOG |Process: {number}");

            this.service.Process(number);
        }
    }
}