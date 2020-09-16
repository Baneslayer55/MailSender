using System.IO;
using System.Text.Json;

namespace MailSender.Models
{
    /// <summary>
    /// Is the entity to deserialize the email provider config
    /// </summary>
    public class MailConfig
    {     
        public string Host { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; }

        public string Password { get; set; }

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
