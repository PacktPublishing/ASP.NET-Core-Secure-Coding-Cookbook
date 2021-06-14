using OnlineBankingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace OnlineBankingApp.Services
{
    public class PayeeService : IPayeeService
    {
        private Root payees;

        public async Task<Root> GetPayeesAsync(string mongoDBRestUri = null){

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(mongoDBRestUri))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        payees = JsonConvert.DeserializeObject<Root>(apiResponse);
                    }
                }
            }

            return payees;
        }


    }

    public interface IPayeeService
    {
        Task<Root> GetPayeesAsync(string uri);
    }
}