using System;
using System.Collections.Generic;
using System.Text;
using torque.Common.Contracts.Services;

namespace torque.Common.Services
{
    public class ConsoleLoggingService : ILoggingService
    {
        public void Log(string line)
        {
            Console.WriteLine($"{DateTime.UtcNow}: {line}");
        }
    }
}
