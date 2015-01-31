using System;

namespace PartageContext.Core
{
    public class ContentTypeAttribute : Attribute
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateView { get; set; }
        public string DisplayView { get; set; }
    }
}