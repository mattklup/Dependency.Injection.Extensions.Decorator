using System.IO;

namespace Dependency.Injection.Extensions.Decorator
{
    public class SampleService : ISampleService
    {
        private readonly TextWriter _logger;

        public SampleService(TextWriter logger)
        {
            _logger = logger;
        }

        public void Process(int number)
        {
            _logger.WriteLine($"SampleService received a number: {number}");
        }
    }
}