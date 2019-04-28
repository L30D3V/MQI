namespace WebApi
{
    public class ServerConfig
    {
        public MongoDbConfiguration MongoDB { get; set; } = new MongoDbConfiguration();
    }
}