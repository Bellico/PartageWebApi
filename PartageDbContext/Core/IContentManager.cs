
using PartageContext.Models;
namespace PartageContext.Core
{
    public interface IContentManager
    {
        string Create();
        IContentType FindById(string id);
        void Execute(string key);
    }
}