

using NLog;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LogerManager : ILogerService
    {
        private static ILogger loger=LogManager.GetCurrentClassLogger();
        //LogManager.GetCurrentClassLogger(); ile artık logerı kullanabiliriz 
        //GetCurrentClassLogger() yöntemi, çağrıldığı anki sınıfın adını kullanarak, o sınıf için bir ILogger örneği döndürür. Bu ILogger örneği, loglama işlemlerini yapmak için kullanılabilir.
        public void LogDebug(string message)=> loger.Debug(message);

        public void LogEror(string message)=> loger.Error(message);

        public void LogInfo(string message)=> loger.Info(message);

        public void LogWorning(string message)=> loger.Warn(message);
    }
}
