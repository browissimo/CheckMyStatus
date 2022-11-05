using CheckMyStatus.Service.Interfaces;
using MimeKit;
using Quartz;
using System.Text;
using CheckMyStatus.Domain.ViewModels;

namespace QuartzApp.Jobs
{
    public class EmailSender : IJob
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly IStatusService _statusService;

        public EmailSender(ILogger<EmailSender> logger, IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            StringBuilder sb = null;
            StringBuilder emailBody = new StringBuilder();
            var emeilService = new EmailService();
            string emailAddress = null;

            int sendingPack = 100;

            var recipients = await _statusService.PrepareRecipients();
            var recipientsGrouped = from recipient in recipients
                                    group recipient by recipient.Email;

            double recipientsGroupedCount = recipientsGrouped.Count();

            double iter;
            IEnumerable<IGrouping<string, RequestChanges>> recipientsGroupedPack;

            if (recipientsGroupedCount >= sendingPack)
            {
                iter = Math.Round((recipientsGroupedCount / sendingPack), MidpointRounding.ToPositiveInfinity);
                for (int i = 1; i <= iter; i++)
                {
                    recipientsGroupedPack = recipientsGrouped.Skip((i - 1) * sendingPack).Take(sendingPack);

                    foreach (var rec in recipientsGrouped)
                    {
                        _logger.LogInformation(rec.Key);
                        emailAddress = rec.Key;

                        foreach (var p in rec)
                        {
                            sb = new StringBuilder();
                            sb.Append($"По организации {p.Pan}");
                            if (p.Local)
                            {
                                sb.Append(" изменился локальный статус");
                            }
                            if (p.Remote)
                            {
                                sb.Append(" изменился статус на портале");
                            }

                            emailBody.Append(sb).AppendLine();
                        }

                        emeilService = new EmailService();

                        await emeilService.SendEmailAsync(emailAddress, emailBody.ToString());
                    }

                    Thread.Sleep(300000);
                   
                }
            }
            else
            {
                foreach (var rec in recipientsGrouped)
                {
                    _logger.LogInformation(rec.Key);
                    emailAddress = rec.Key;

                    foreach (var p in rec)
                    {
                        sb = new StringBuilder();
                        sb.Append($"По организации {p.Pan}");
                        if (p.Local)
                        {
                            sb.Append(" изменился локальный статус");
                        }
                        if (p.Remote)
                        {
                            sb.Append(" изменился статус на портале");
                        }

                        emailBody.Append(sb).AppendLine();
                    }

                    emeilService = new EmailService();

                    await emeilService.SendEmailAsync(emailAddress, emailBody.ToString());
                }
            }
        }
        public class EmailService
        {
            public async Task SendEmailAsync(string To, string emailBody)
            {
                try
                {
                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Моя компания", "mixer008@yandex.ru"));
                    message.To.Add(new MailboxAddress("test", $"{To}")); 
                    message.Subject = "Изменения в статусе компании"; 
                    message.Body = new BodyBuilder() { HtmlBody = emailBody }.ToMessageBody();

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.yandex.ru", 465, true); 
                        client.Authenticate("mixer008@yandex.ru", "PASSWORD"); 
                        client.Send(message);

                        client.Disconnect(true);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        
    }
}