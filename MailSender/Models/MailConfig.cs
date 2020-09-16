using System.IO;
using System.Text.Json;

namespace MailSender.Models
{
    public class MailConfig
    {     
        public string Host { get; set; }

        public string Port { get; set; }

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
