namespace DAL.Clients.Implementations
{
    using DnsClient.Internal;
    using IdentityModel.Client;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using System;
    using System.ComponentModel;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class TokenClient
    {
        private string _token;
        private AuthenticationSettings _settings;
        public TokenClient(AuthenticationSettings settings)
        {
            _settings = settings;
        }

        public async Task<string> GetToken()
        {
            if (_token != null)
                return _token;
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _settings.Authority + "/connect/token",

                ClientId = _settings.Client_Id,
                ClientSecret = _settings.Client_Secret,
                Scope = _settings.Scopes
            }).ConfigureAwait(false);

            if (tokenResponse.IsError)
                Console.WriteLine(tokenResponse.Error);
            else _token = tokenResponse.AccessToken;

            return _token;
        }
    }
}
