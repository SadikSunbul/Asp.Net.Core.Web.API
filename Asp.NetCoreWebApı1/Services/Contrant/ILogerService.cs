using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contrant
{
    public interface ILogerService
    {
        void LogInfo(string message);
        void LogWorning(string message);
        void LogEror(string message);
        void LogDebug(string message);
    }
}
