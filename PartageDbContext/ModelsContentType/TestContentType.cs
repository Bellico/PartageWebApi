using PartageContext.Core;
using PartageContext.Manager;

namespace PartageContext.ModelsContentType
{
    [ContentType(Key = "test", Name = "TestContent", CreateView = "TestContent.View", Description = "Create a content test")]
    public class TestContentType : ContentType, IContentType
    {
         public string Test { get; set; }

         public IContentManager GetManager()
         {
             return new TestContentTypeManager(this);
         }
    }
}