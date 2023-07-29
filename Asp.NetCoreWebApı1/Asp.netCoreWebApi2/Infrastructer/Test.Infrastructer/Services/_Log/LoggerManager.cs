using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract.Services._Log;

namespace Test.Infrastructer.Services._Log
{
    public class LoggerManager : ILoggerService
    {
        private static NLog.ILogger loger = LogManager.GetCurrentClassLogger();
        
        public void LogDebug(string message) => loger.Debug(message);

        public void LogError(string message) => loger.Error(message);

        public void LogInfo(string message) => loger.Info(message);

        public void LogWarning(string message) => loger.Warn(message);
    }
}
