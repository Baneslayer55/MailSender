using System.IO;
using System.Text.Json;

namespace MailSender.Models
{
    /// <summary>
    /// Is the entity to deserialize the email provider config
    /// </summary>
    public class MailConfig
    {
        /// <summary>
        /// Email provider host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Email provider port.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Mail for sending messages
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Mails password.
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// This method reutrn deserialized MailConfig entity from mailconfig.json file.
        /// </summary>
        /// <param name="fileConfigName">Name of your config file.</param>
        /// <returns>MailConfig</returns>
        public static MailConfig GetConfig(string fileConfigName)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string jsonString = File.ReadAllText(fileConfigName);

            return JsonSerializer.Deserialize<MailConfig>(jsonString, options);
        }
    }
}
