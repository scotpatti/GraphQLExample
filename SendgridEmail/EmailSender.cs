using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendgridEmail
{
    public class EmailSender
    {
        private string _apiKey;
        private string _fromEmail;
        private string _fromEmailName;
        private ILogger _logger;
        public EmailSender(ILogger logger, string apiKey, string fromEmail, string? fromEmailName = null)
        {
            _logger = logger;
            _apiKey = apiKey;
            _fromEmail = fromEmail;
            if (fromEmailName != null)
            {
                _fromEmailName = fromEmailName;
            }
            else
            {
                _fromEmailName = _fromEmail;
            }
        }

        public async Task Send(string subject, string message, string toEmail)
        {
            var client = new SendGridClient(_apiKey);
            var mesg = new SendGridMessage()
            {
                From = new EmailAddress(_fromEmail, _fromEmailName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
            };
            mesg.AddTo(new EmailAddress(toEmail));
            mesg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(mesg);
            if (response != null && response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Reset email succesfully sent to {toEmail} at {DateTime.UtcNow.ToString()} UTC.");
            }
            else
            {
                _logger.LogCritical($"Unable to send reset password message to {toEmail}");
            }
                    
        }

    }
}
