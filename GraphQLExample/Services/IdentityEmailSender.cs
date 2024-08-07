using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace GraphQLExample.Services
{
    public class IdentityEmailSender : IEmailSender
    {
        private ILogger<IdentityEmailSender> _logger;
        private EnvSecrets _secrets;

        public IdentityEmailSender(EnvSecrets secrets, ILogger<IdentityEmailSender> logger)
        {
            _logger = logger;
            _secrets = secrets;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        { 
            SendgridEmail.EmailSender sender = new SendgridEmail.EmailSender(_logger, _secrets.SendGridKey!, _secrets.FromEmail!);
            await sender.Send(subject, body, toEmail);
        }

    }
}
