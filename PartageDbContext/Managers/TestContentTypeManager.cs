using MongoDB.Bson;
using MongoDB.Driver;
using PartageContext.Core;
using PartageContext.ModelsContentType;
using System;

namespace PartageContext.Manager
{
    public class TestContentTypeManager : IContentManager
    {
        private MongoCollection<TestContentType> mongoCollection = MongoRepository.Open<TestContentType>("testContentCollection");
        private TestContentType model;

        public TestContentTypeManager(TestContentType model)
        {
            this.model = model;
        }

        public string Create()
        {
            mongoCollection.Insert(model);
            return model.Id.ToString();
        }

        public IContentType FindById(string id)
        {
            return mongoCollection.FindOneById(ObjectId.Parse(id));
        }

        public void Execute(string key)
        {
            throw new NotImplementedException();
        }
    }
}