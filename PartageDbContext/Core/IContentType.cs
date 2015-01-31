
using PartageContext.Models;

namespace PartageContext.Core
{
    public interface IContentType
    {
        IContentManager GetManager();

        void SetContainer(Container container);
    }
}
