using Abp.Logging;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoilerplatePro.Features
{
    public class UserMailerService
    {
        private readonly SendGridClient client = null;
        private SmtpClient smtpClient = null;

        //SendGrid Data

        private string sendGridApiKey = ConfigurationManager.AppSettings["SendGridApi"];

        // SMTP Data

        private string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
        private string smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
        private string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
        private int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
        private bool smtpUseSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpUseSsl"]);

        // OneSignal Settings

        string oneSignalAppId = ConfigurationManager.AppSettings["OneSignalAppId"];
        string oneSignalApiKey = ConfigurationManager.AppSettings["OneSignalApiKey"];

        // Mail Settings

        private string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
        private string fromName = ConfigurationManager.AppSettings["FromName"];
        private string replyTo = ConfigurationManager.AppSettings["replyTo"];

        public UserMailerService()
        {
            if (!string.IsNullOrEmpty(sendGridApiKey))
            {
                client = new SendGridClient(sendGridApiKey);
            }
        }

        public async Task SendMailAsync(string toAddress, string toName, string mailSubject, string body)
        {
            var from = new EmailAddress(fromEmail, fromName);
            var subject = mailSubject;
            var to = new EmailAddress(toAddress, toName);
            var htmlContent = body;

            try
            {
                if (!string.IsNullOrEmpty(smtpServer))
                {
                    using (smtpClient = new SmtpClient())
                    {
                        smtpClient.CheckCertificateRevocation = false;
                        smtpClient.ServerCertificateValidationCallback = (mysender, certificate, chain, sslPolicyErrors) => { return true; };
                        smtpClient.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

                        // Note: only needed if the SMTP server requires authentication
                        smtpClient.Authenticate(smtpUsername, smtpPassword);

                        MimeMessage message = new MimeMessage();
                        message.From.Add(new MailboxAddress(fromName, fromEmail));
                        message.To.Add(new MailboxAddress(toName, toAddress));
                        message.Subject = mailSubject;
                        message.Body = new TextPart("html")
                        {
                            Text = body
                        };
                        message.ReplyTo.Add(new MailboxAddress(fromName, replyTo));

                        await smtpClient.SendAsync(message);
                        smtpClient.Disconnect(true);
                    }
                }
                else if (client != null)
                {
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, htmlContent);
                    msg.ReplyTo = new EmailAddress(replyTo);

                    await client.SendEmailAsync(msg);
                }
                else
                {
                    LogHelper.Logger.Info($"SendGridApi key and SMTP settings not found in config. Printing mail to {toName} <{toAddress}>:\n" +
                    $"Subject: {mailSubject}\n" +
                    $"Body:\n" +
                    $"{body}");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger.Error("Could not send mail from EmailController", ex);
            }
        }

        public async Task SendPushNotification(string notification, string userId)
        {
            if (string.IsNullOrEmpty(oneSignalAppId))
            {
                LogHelper.Logger.Info($"OneSignalAppId not found in config file. Printing notification to User ID {userId} :\n" +
                        $"Notification: {notification}\n");
                return;
            }

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Basic " + sendGridApiKey);

            var obj = new
            {
                app_id = oneSignalAppId,
                contents = new { en = notification },
                include_external_user_ids = new string[] { userId }
            };

            var param = JsonConvert.SerializeObject(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }

                LogHelper.Logger.Info("Response for push notification: " + responseContent);

            }
            catch (WebException ex)
            {
                LogHelper.Logger.Error($"Could not send Push Notification: {new StreamReader(ex.Response.GetResponseStream()).ReadToEnd()}", ex);
            }

        }
    }
}
