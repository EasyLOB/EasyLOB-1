namespace EasyLOB.Extensions.Mail
{
    /// <summary>
    /// IMailManager
    /// </summary>
    public interface IMailManager
    {
        #region Methods

        /// <summary>
        /// Mail
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        void Mail(string toAddress,
            string subject, string body, bool isHtml = false);

        /// <summary>
        /// Mail
        /// </summary>
        /// <param name="fromName"></param>
        /// <param name="toName"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        /// <param name="fileAttachmentPaths"></param>
        void Mail(string fromName, string toName, string toAddress,
            string subject, string body, bool isHtml = false, string[] fileAttachmentPaths = null);

        /// <summary>
        /// Mail
        /// </summary>
        /// <param name="message"></param>
        void Mail(System.Net.Mail.MailMessage message);

        /// <summary>
        /// Mail
        /// </summary>
        /// <param name="message"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="defaultCredentials"></param>
        /// <param name="enableSsl"></param>
        void Mail(System.Net.Mail.MailMessage message,
            string host, int port, string userName, string password, bool defaultCredentials, bool enableSsl);

        #endregion Methods
    }
}