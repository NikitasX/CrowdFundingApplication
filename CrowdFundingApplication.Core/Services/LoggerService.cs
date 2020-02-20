using Serilog;
using CrowdFundingApplication.Core.Services.Interfaces;

namespace CrowdFundingApplication.Core.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogError(string errorCode, string errorText)
        {
            var Log = new LoggerConfiguration()
              .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();
            Log.Error(errorCode);
            Log.Information(errorText);
        }
        public void LogInformation(string errorText)
        {
            var Log = new LoggerConfiguration()
              .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();
            Log.Information(errorText);
        }
    }
}
