namespace Infrastructure.CrossCutting.Settings.Interfaces
{
    public interface IMongoDBConnection
    {
        string Database { get; set; }
        string ConnectionString { get; set; }
    }
}
