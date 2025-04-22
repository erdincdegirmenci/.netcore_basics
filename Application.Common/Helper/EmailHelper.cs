using Application.Common.Models;
using DefineXwork.Library.Logging;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Application.Common.Helper
{
    public class EmailHelper
    {

        private readonly MailSettings _mailSettings;
        private readonly ILogManager<EmailHelper> _logManager;
        public EmailHelper(IOptions<MailSettings> mailSettings, ILogManager<EmailHelper> logManager)
        {
            _mailSettings = mailSettings.Value;
            _logManager = logManager;
        }

        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_mailSettings.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_mailSettings.SmtpServer, _mailSettings.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailSettings.Username, _mailSettings.Password);

                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception, or both.
                    _logManager.LogError("Email Gönderim Hata:", ex);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception, or both.
                    _logManager.LogError("Email Gönderim Hata:", ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}