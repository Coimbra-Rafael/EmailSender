using System.Net;
using System.Net.Mail;
using Backgroud.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace Backgroud.Services.Services;

public class EmailSenderService(BackgroundContext context) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 30)
        {
            var emails = await GetAllEmailAddresses();

            foreach (string email in emails)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.Subject = "Curriculo";
                mail.Body = "Corpo do email";
                
                string pdfFilePath = "/home/curriculo/curriculo.pdf";
                Attachment attachment = new Attachment(pdfFilePath, "application/pdf");
                mail.Attachments.Add(attachment);
                
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = Environment.GetEnvironmentVariable("SMTP_HOST")!;
                    smtp.Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!);
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USERNAME"),
                        Environment.GetEnvironmentVariable("SMTP_PASSWORD"));

                    await smtp.SendMailAsync(mail);
                }
            }
        }

        await Task.Delay(360000,stoppingToken);
    }

    private async Task<List<string>> GetAllEmailAddresses()
    {
        var emails = await context.Emails.ToListAsync();
        // ReSharper disable once CollectionNeverQueried.Local
        var emailAddresses = new List<string>();
        foreach (var email in emails)
        {
            if (email.EmailAddress != null)
                emailAddresses.Add(email.EmailAddress);
        }

        return emailAddresses;
    }
}