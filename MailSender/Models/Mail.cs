using System;

namespace MailSender.Models
{
    /// <summary>
    /// Mail entity class.
    /// Using for deserialize body of POST request. 
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Autoincrement unique integer ID. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Subject of email in string format
        /// </summary>
        public string Subject { get; set; } = "(no subject)";

        /// <summary>
        /// Body of email in string format
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Recipients of emil in string array format. 
        /// In database saves as string.
        /// </summary>
        public string[] Recipients { get; set; }

        /// <summary>
        /// Message sending status.
        /// True if at least one message was sended.
        /// </summary>
        public bool IsSended { get; set; } = false;


        /// <summary>
        /// Message sent date.
        /// </summary>
        public DateTime SendingDateTime { get; set; }

        /// <summary>
        /// Error messages while sending. 
        /// </summary>
        public string ErrorMessage { get; set; } = "";
    }
}
