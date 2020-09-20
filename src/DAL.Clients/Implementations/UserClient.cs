namespace DAL.Clients.Implementations
{
    using DAL.Clients.Interfaces;
    using Models.Domain.Models;
    using System.Text.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using System.Collections.Generic;
    using System.Text;
    using Models.Filters;
    using IdentityModel.Client;
    using Newtonsoft.Json;
    using Models.DTO.Grids;

    public class UserClient : IUserClient
    {
        private readonly string USERURI = "/api/user";
        public HttpClient Client;
        public UserClient(HttpClient client, UriSettings uriSettings, TokenClient tokenClient)
        {
            client.BaseAddress = new Uri(uriSettings.UserServiceUri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var token = tokenClient.GetToken().Result;
            client.SetBearerToken(token);

            this.Client = client;
        }

        public async Task<IEnumerable<User>> GetCollaboratorsFromDepartment(Department defaultDepartment)
        {
            try
            {
                var body = new UserFilter
                {
                    Department_Id = defaultDepartment.Id
                };
                var response = await Client.PostAsync(this.USERURI + "/search", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var resp = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var users = JsonConvert.DeserializeObject<UserGrid>(resp);

                return users.List;
            }
            catch(Exception ex)
            {
                Console.WriteLine(Environment.NewLine + $"Error getting collaborators + {ex.Message}");
                throw;
            }
        }
    }
}
