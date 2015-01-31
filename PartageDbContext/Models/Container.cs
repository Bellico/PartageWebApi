using PartageContext.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace PartageContext.Models
{
    public class Container
    {
        public int Id { get; set; }

        [Required]
        public string DataContentId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Link { get; set; }

        [Required]
        public string ContentType { get; set; }

        [StringLength(128)]
        public string User_Id { get; set; }

        public DateTime DateOnline { get; set; }

        public DateTime? DateExpire { get; set; }

        public int Visibility { get; set; }

        public string Description { get; set; }

        public static Container Create(string dataContentId, IContentType type)
        {
            return new Container
            {
                DataContentId = dataContentId,
                Visibility = 1,
                DateOnline = DateTime.Now,
                ContentType = PartageContext.Core.ContentType.GetKey(type),
                Link = Guid.NewGuid().ToString("N")
            };
        }
    }
}
