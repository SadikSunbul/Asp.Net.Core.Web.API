using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.Abstract.Services._Log
{
    public interface ILoggerService
    {
        void LogInfo(string message);

        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);
    }
}
