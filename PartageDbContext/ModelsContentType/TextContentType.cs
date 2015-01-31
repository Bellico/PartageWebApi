using PartageContext.Core;
using PartageContext.Manager;
using System.ComponentModel.DataAnnotations;

namespace PartageContext.ModelsContentType
{
    [ContentType(Key = "text", Name = "Text", CreateView = "TextContent.Create", DisplayView = "TextContent.Display", Description = "A simple text to share")]
    public class TextContentType : ContentType, IContentType
    {
        [Display(Name = "Texte")]
        [Required]
        public string Text { get; set; }

        public IContentManager GetManager()
        {
            return new TextContentTypeManager(this); 
        }
    }
}