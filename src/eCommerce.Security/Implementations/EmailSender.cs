using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eCommerce.Security.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly EmailSettings _options;

        public EmailSender(IOptions<EmailSettings> options, ILogger<EmailSender> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            try
            {
                var fromAddress = new MailAddress(_options.FromEmail, _options.FromEmailName);
                var toAddress = new MailAddress(email);

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                var smtp = new SmtpClient
                {
                    Credentials = new NetworkCredential(_options.FromEmail, _options.Password),
                    Host = _options.Host,
                    Port = _options.Port,
                    EnableSsl = _options.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };

                if (_options.EnableSendEmail)
                {
                    await smtp.SendMailAsync(message); 
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(1, exception, exception.Message);
            }
        }
    }
}