using MongoDB.Driver;

namespace PartageContext.Core
{
    public class MongoRepository
    {
        private static MongoServer Server { get; set; }

        private static MongoServer GetMongoServer()
        {
            if(Server == null)
            {
                MongoClient client = new MongoClient("mongodb://localhost");
                Server = client.GetServer();
            }

            return Server;
        }

        public static MongoCollection<T> Open<T>(string nameCollection)
        {
            MongoDatabase database = GetMongoServer().GetDatabase("partage");
            MongoCollection<T> collection = database.GetCollection<T>(nameCollection);
          
            return collection;
        }
    }
}