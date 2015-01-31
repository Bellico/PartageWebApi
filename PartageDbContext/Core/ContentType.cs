using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PartageContext.Models;
using PartageContext.ModelsContentType;
using System;
using System.Collections.Specialized;
using System.Reflection;

namespace PartageContext.Core
{
    public class ContentType
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public Container Container;

        public void SetContainer(Container container)
        {
            this.Container = container;
        }

        private static Type[] contentTypeEnable = new Type[]
        {
             typeof(TextContentType),
             typeof(TestContentType),
             typeof(TodoContentType)
        };

        public static Type[] GetListAvailable()
        {
            return contentTypeEnable;
        }

        public static IContentType Default()
        {
            return new TextContentType();
        }

        public static IContentType GetContentType(Container container)
        {
            IContentType type = GetContentType(container.ContentType).GetManager().FindById(container.DataContentId);
            type.SetContainer(container);
            return type;
        }

        public static IContentType GetContentType(string key)
        {
            foreach (Type type in contentTypeEnable)
            {
                ContentTypeAttribute contentTypeAttr = GetContentTypeAttribute(type);
                if (contentTypeAttr.Key == key) return (IContentType)Activator.CreateInstance(type);
            }

            throw new IndexOutOfRangeException();
        }

        public static IContentType Bind(IContentType contentType, NameValueCollection paramater)
        {
            Type type = contentType.GetType();
            foreach (string param in paramater)
            {
                string nameFormat = param[0].ToString().ToUpper() + param.Substring(1).ToLower();
                PropertyInfo property = type.GetProperty(nameFormat);
                if (property != null)
                {
                    string value = paramater.Get(param);
                    property.SetValue(contentType, value);
                }
            }
            return contentType;
        }

        public static string GetKey(IContentType type)
        {
            return GetContentTypeAttribute(type).Key;
        }

        public static string GetKey(Type type)
        {
            return GetContentTypeAttribute(type).Key;
        }

        public static string GetName(IContentType type)
        {
            return GetContentTypeAttribute(type).Name;
        }

        public static string GetName(Type type)
        {
            return GetContentTypeAttribute(type).Name;
        }

        public static string GetDescription(IContentType type)
        {
            return GetContentTypeAttribute(type).Description;
        }

        public static string GetDescription(Type type)
        {
            return GetContentTypeAttribute(type).Description;
        }

        public static string GetCreateView(Type type)
        {
            return GetContentTypeAttribute(type).CreateView;
        }

        public static string GetCreateView(IContentType type)
        {
            return GetContentTypeAttribute(type).CreateView;
        }

        public static string GetDisplayView(IContentType type)
        {
             var attr =  GetContentTypeAttribute(type);
             return (attr.DisplayView != null) ? attr.DisplayView : attr.CreateView;
        }

        public static string GetDisplayView(Type type)
        {
            var attr = GetContentTypeAttribute(type);
            return (attr.DisplayView != null) ? attr.DisplayView : attr.CreateView;
        }

        private static ContentTypeAttribute GetContentTypeAttribute(IContentType type)
        {
            var attr = Attribute.GetCustomAttributes(type.GetType(), typeof(ContentTypeAttribute));
            return (ContentTypeAttribute)attr[0];
        }

        private static ContentTypeAttribute GetContentTypeAttribute(Type type)
        {
            var attr = Attribute.GetCustomAttributes(type, typeof(ContentTypeAttribute));
            return (ContentTypeAttribute)attr[0];
        }
    }
}