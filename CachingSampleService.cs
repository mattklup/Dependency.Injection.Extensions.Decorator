using System.Collections.Generic;
using System.IO;

namespace Dependency.Injection.Extensions.Decorator
{
    public class CachingSampleService : ISampleService
    {
        private readonly ISampleService service;
        private readonly TextWriter logger;
        private readonly HashSet<int> cache = new HashSet<int>();

        public CachingSampleService(ISampleService innerService, TextWriter logger)
        {
            this.service = innerService;
            this.logger = logger;
        }

        public void Process(int number)
        {
            this.logger.WriteLine($"CACHE |Process: {number}");

            if (this.cache.Contains(number))
            {
                this.logger.WriteLine($"CACHE |-CacheHit: {number}");
            }
            else
            {
                this.service.Process(number);
                this.cache.Add(number);
            }
        }
    }

}