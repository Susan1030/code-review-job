using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReviewJob
{
    public static class AppConfig
    {
        public static string EmailHost
            => ConfigurationManager.AppSettings[nameof(EmailHost)];

        public static string EmailFrom
            => ConfigurationManager.AppSettings[nameof(EmailFrom)];

        public static string EmailDisplay
            => ConfigurationManager.AppSettings[nameof(EmailDisplay)];

        public static string EmailPassword
            => ConfigurationManager.AppSettings[nameof(EmailPassword)];

        public static short EmailPort
            => short.Parse(ConfigurationManager.AppSettings[nameof(EmailPort)]);

        public static string ConnectionString
            => ConfigurationManager.ConnectionStrings[nameof(ConnectionString)].ConnectionString;

        public static int ReviewerGroupCount
            => int.Parse(ConfigurationManager.AppSettings[nameof(ReviewerGroupCount)]);
    }
}
