namespace DAL.Clients.Implementations
{
    using DAL.Clients.Interfaces;
    using Models.Domain.Models;
    using System.Text.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using IdentityModel.Client;
    using Newtonsoft.Json;

    public class DepartmentClient : IDepartmentClient
    {
        private readonly string DEPARTMENTURI = "/api/department";
        public HttpClient Client;
        public DepartmentClient(HttpClient client, UriSettings uriSettings, TokenClient tokenClient)
        {
            client.BaseAddress = new Uri(uriSettings.ConfigurationServiceUri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var token = tokenClient.GetToken().Result;
            client.SetBearerToken(token);

            this.Client = client;
        }

        public async Task<Department> GetDefaultDepartment()
        {
            try
            {
                var response = await Client.GetAsync(this.DEPARTMENTURI + "/default").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var resp = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var department = JsonConvert.DeserializeObject<Department>(resp);

                return department;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + $"Error getting default department + {ex.Message}");
                throw;
            }
        }
    }
}
