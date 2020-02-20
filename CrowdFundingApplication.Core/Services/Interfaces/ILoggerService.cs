using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogError(string errorcode, string text);

        void LogInformation(string text);
    }
}
