using Iterates.Bwm.Domain.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog;

namespace Iterates.Bwm.Infrastructure.Logging;

public class LoggerManager : ILoggerManager
{
    public void LogDebug(string message) => WatchLogger.Log(message);
    public void LogError(string message) => WatchLogger.LogError(message);
    public void LogInfo(string message) => WatchLogger.Log(message);
    public void LogWarn(string message) => WatchLogger.LogWarning(message);
}
