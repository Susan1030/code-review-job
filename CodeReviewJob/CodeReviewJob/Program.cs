using sdmap.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeReviewJob
{
    class Program
    {
        static void Main(string[] args)
        {
            SdmapExtensions.SetSqlDirectoryAndWatch("sqls");

            var date = DateTime.Now.Date;
            var groups = DataSource
                .GetOrCreateCodeReviewGroup(date);
            var body = EmailUtil.RenderBody(groups);

            var tos = DataSource.GetFrontendDevelopers(isLeader: false)
                .Select(x => x.Email);
            var ccs = DataSource.GetFrontendDevelopers(isLeader: true)
                .Select(x => x.Email);
            
            EmailUtil.Send(
                "Code Review for: " + date.ToShortDateString(),
                body,
                tos,
                ccs);
        }
    }
}
