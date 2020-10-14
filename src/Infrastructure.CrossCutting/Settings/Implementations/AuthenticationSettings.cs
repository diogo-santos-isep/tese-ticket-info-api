namespace Infrastructure.CrossCutting.Settings.Implementations
{
    public class AuthenticationSettings
    {
        public string Authority { get; set; }
        public string Client_Id { get; set; }
        public string Client_Secret { get; set; }
        public string Scopes { get; set; }
    }
}
