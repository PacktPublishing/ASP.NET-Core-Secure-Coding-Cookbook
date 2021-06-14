using System.Net;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Services
{
    public class EmailReputation : IEmailReputation
    {
        private readonly IConfiguration Configuration;
        public EmailReputation(IConfiguration config)
        {
            Configuration = config;
        }

        public bool IsRisky(string email)
        {
            var emailRepApiKey = Configuration["EmailRepApiKey"];
            HttpWebRequest repEmailRequest = (HttpWebRequest)WebRequest.Create($"https://emailrep.io/{email}");
            repEmailRequest.Headers.Add("Cookie", $"{emailRepApiKey}");
            repEmailRequest.Headers.Add("User-Agent", "MyAppName");
            HttpWebResponse repEmailResponse = (HttpWebResponse) repEmailRequest.GetResponse();

            Stream newStream = repEmailResponse.GetResponseStream();
            var repEmail = new StreamReader(newStream).ReadToEnd();    
            var reputation = JsonSerializer.Deserialize<Reputation>(repEmail);

            if (reputation.suspicious || reputation.details.blacklisted || reputation.details.spam || reputation.details.malicious_activity || reputation.details.malicious_activity_recent)
                return true;
            
            return false;
        }
    }
    public interface IEmailReputation
    {
        bool IsRisky(string input);
    }    
}