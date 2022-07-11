using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Totostore.Backend.Application.Common.Mailing;

namespace Totostore.Backend.Infrastructure.Mailing;

public class MailGunService : IMailService
{
    private readonly MailSettings _settings;
    private readonly ILogger<MailGunService> _logger;

    public MailGunService(IOptions<MailSettings> settings, ILogger<MailGunService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendAsync(MailRequest request)
    {
        try
        {
            // To
            var mailAddresses = request.To.ConvertAll(address => new Address(address));

            // Content
            var fluentEmail = Email
                .From(request.From ?? _settings.From, request.DisplayName ?? _settings.DisplayName)
                .To(mailAddresses)
                .Subject(request.Subject)
                .Body(request.Body, true)
                .Tag("totostore");

            // Reply To
            if (!string.IsNullOrEmpty(request.ReplyTo)) fluentEmail.ReplyTo(request.ReplyTo, request.ReplyToName);

            // Bcc
            var bccAddresses = new List<Address>();
            bccAddresses.AddRange(request.Bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue))
                .Select(address => new Address(address.Trim())));
            fluentEmail.BCC(bccAddresses);

            // Cc
            var ccAddresses = request.Cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue))
                .Select(address => new Address(address.Trim())).ToList();
            fluentEmail.CC(ccAddresses);

            // Headers
            foreach (var header in request.Headers) fluentEmail.Header(header.Key, header.Value);

            // Create the file attachments for this e-mail message
            var attachments = new List<Attachment>();
            attachments.AddRange(request.AttachmentData.Select(attachmentInfo => new Attachment
            {
                Filename = attachmentInfo.Key, Data = new MemoryStream(attachmentInfo.Value), ContentType = "text/html"
            }));
            fluentEmail.Attach(attachments);

            var mailgunSender = new MailgunSender(
                _settings.Domain,
                _settings.ApiKey);
            await mailgunSender.SendAsync(fluentEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}