using Constracts.Domains;
using Constracts.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Ordering.Domain.Configurations;
using Shared.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Services
{
    public class SmtpEmailService : IEmailSevices<MailRequest>
    {
       // private readonly ILogger _logger;
        private readonly SMTPEmailSetting _settings;
        private readonly SmtpClient _smtpClient;

        public SmtpEmailService(SMTPEmailSetting settings)
        {
          //  _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _smtpClient = new SmtpClient();
        }

        public async Task SendEmailRequest(MailRequest request, CancellationToken cancellationToken = default)
        {
            var emailMessgae = new MimeMessage
            {
                Sender = new MailboxAddress(_settings.DisplayName, request.From ?? _settings.From),
                Subject = request.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = request.Body,

                }.ToMessageBody()
            };

            if (request.ToAddresses.Any())
            {
                foreach(var toAddress in request.ToAddresses)
                {
                    emailMessgae.To.Add(MailboxAddress.Parse(toAddress));
                }
            }

            try
            {
                await _smtpClient.ConnectAsync(_settings.SmtpServer, _settings.Port, _settings.UseSsl, cancellationToken);
                await _smtpClient.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
                await _smtpClient.SendAsync(emailMessgae,cancellationToken);
                await _smtpClient.DisconnectAsync(true, cancellationToken);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.Message, ex);
            }
            finally
            {
                await _smtpClient.DisconnectAsync(true,cancellationToken);
                _smtpClient.Dispose();
            }
        }
    }
}
