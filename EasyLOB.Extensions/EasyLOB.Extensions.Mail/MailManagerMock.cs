namespace EasyLOB.Extensions.Mail
{
    /// <summary>
    /// Mail Manager - Mock
    /// </summary>
    public partial class MailManagerMock : IMailManager
    {
        #region Methods Interface

        public void Mail(string toAddress,
            string subject, string body, bool isHtml = false)
        {
        }

        public void Mail(string fromName, string toName, string toAddress,
            string subject, string body, bool isHtml = false, string[] fileAttachmentPaths = null)
        {
        }

        public void Mail(System.Net.Mail.MailMessage message)
        {
        }

        public void Mail(System.Net.Mail.MailMessage message,
            string host, int port, string userName, string password, bool defaultCredentials, bool enableSsl)
        {
        }

        #endregion Methods Interface
    }
}