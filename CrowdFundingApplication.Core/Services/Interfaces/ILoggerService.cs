namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogError(string errorcode, string text);

        void LogInformation(string text);
    }
}
