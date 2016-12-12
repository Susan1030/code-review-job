using CodeReviewJob.Dtos;
using Nustache.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CodeReviewJob
{
    public static class EmailUtil
    {
        public static void Send(string subject, string body, IEnumerable<string> tos, IEnumerable<string> ccs)
        {
            using (var client = new SmtpClient(AppConfig.EmailHost, AppConfig.EmailPort))
            {
                client.Credentials = new NetworkCredential(AppConfig.EmailFrom, AppConfig.EmailPassword);

                var message = new MailMessage
                {
                    IsBodyHtml = true, 
                    Subject = subject, 
                    Body = body, 
                    From = new MailAddress(AppConfig.EmailFrom, AppConfig.EmailDisplay)
                };
                foreach (var to in tos) message.To.Add(to);
                foreach (var cc in ccs) message.CC.Add(cc);

                client.Send(message);
            }
        }

        public static void SendDebug(string subject, string body, IEnumerable<string> tos, IEnumerable<string> ccs)
        {
            Console.WriteLine("subject: ");
            Console.WriteLine(subject);

            Console.WriteLine("tos: ");
            Console.WriteLine(string.Join(",", tos));

            Console.WriteLine("ccs: ");
            Console.WriteLine(string.Join(",", ccs));

            Console.WriteLine("body: ");
            Console.WriteLine(body);
        }

        public static string RenderBody(IList<CodeReviewDto> dto)
        {
            return Render.FileToString(@"templates/email.html", new
            {
                Data = dto
            });
        }
    }
}
