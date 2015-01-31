using MongoDB.Bson.Serialization.Attributes;
using PartageContext.Core;
using PartageContext.Manager;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartageContext.ModelsContentType
{
    [ContentType(Key = "todo", Name = "Todo", Description = "TodoList")]
    public class TodoContentType : ContentType, IContentType
    {
        [BsonRequired]
        public List<TodoTask> Todos = new List<TodoTask>();

        public IContentManager GetManager()
        {
            return new TodoContentTypeManager(this);
        }
    }

    public class TodoTask
    {
        public bool Done { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
