using System;
using System.Collections.Generic;
using System.Text;

namespace torque.Common.Contracts.Services
{
    public interface ILoggingService
    {
        void Log(string line);
    }
}
