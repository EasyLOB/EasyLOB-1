using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;

/*
<system.net>
  <mailSettings>
    <smtp from="email@gmail.com">
      <network enableSsl="true" host="smtp.gmail.com" port="587" userName="email@gmail.com" password="password" defaultCredentials="false"/>
    </smtp>
  </mailSettings>
</system.net>
 */

/*
System.Configuration.Configuration configuration = null;
if (System.Web.HttpContext.Current != null)
{
    configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
}
else
{
    configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
}
MailSettingsSectionGroup mailSettings = configuration.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
 */

namespace EasyLOB.Extensions.Mail
{
    /// <summary>
    /// Mail Manager - EasyLOB
    /// </summary>
    public partial class MailManager : IMailManager
    {
        #region Methods Interface

        public void Mail(string toAddress,
            string subject, string body, bool isHtml = false)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            if (smtpSection != null)
            {
                MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new System.Net.Mail.MailAddress(smtpSection.From);
                message.To.Add(toAddress);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                Mail(message);
            }
        }

        public void Mail(string fromName, string toName, string toAddress,
            string subject, string body, bool isHtml = false, string[] fileAttachmentPaths = null)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            if (smtpSection != null)
            {
                MailAddress from = new MailAddress(smtpSection.From, fromName);
                MailAddress to = new MailAddress(toAddress, toName);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                if (fileAttachmentPaths != null)
                {
                    foreach (string fileAttachmentPath in fileAttachmentPaths)
                    {
                        Attachment attach = new Attachment(fileAttachmentPath);
                        message.Attachments.Add(attach);
                    }
                }

                bool enableSsl = smtpSection.Network.EnableSsl;
                string host = smtpSection.Network.Host;
                string password = smtpSection.Network.Password;
                int port = smtpSection.Network.Port;
                string userName = smtpSection.Network.UserName;

                Mail(message, host, port, userName, password, false, enableSsl);
            }
        }

        public void Mail(System.Net.Mail.MailMessage message)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            if (smtpSection != null)
            {
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                smtp.Host = smtpSection.Network.Host;
                smtp.Port = smtpSection.Network.Port;

                smtp.Send(message);
            }
        }

        public void Mail(System.Net.Mail.MailMessage message,
            string host, int port, string userName, string password, bool defaultCredentials, bool enableSsl)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.UseDefaultCredentials = defaultCredentials;
            smtp.Credentials = new System.Net.NetworkCredential(userName, password);
            smtp.EnableSsl = enableSsl;
            smtp.Host = host;
            smtp.Port = port;

            smtp.Send(message);
        }

        #endregion Interface
    }
}