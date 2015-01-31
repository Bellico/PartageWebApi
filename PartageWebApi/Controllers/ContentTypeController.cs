using Newtonsoft.Json;
using PartageContext.Core;
using PartageContext.Models;
using PartageWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PartageWebApi.Controllers
{
    [RoutePrefix ("api")]
    public class ContentTypeController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route ("ContentType/{link}")]
        public IHttpActionResult Get(string link)
        {
            if (link == null) return BadRequest();

            Container container = db.Container.Where(c => c.Link == link).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }
            var contentType = ContentType.GetContentType(container);

            return Ok(contentType);
        }

        [Route("ContentType/{type}")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(string type, HttpRequestMessage request)
        {
            if (type == null) return BadRequest();
            try
            {
                string jsonString = await request.Content.ReadAsStringAsync();
               //dynamic jsonString2 = await Request.Content.ReadAsAsync<JObject>();

                var typeClass = ContentType.GetContentType(type);
                IContentType contentType = (IContentType) JsonConvert.DeserializeObject(jsonString, typeClass.GetType());
   
                var context = new ValidationContext(contentType);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(contentType, context, validationResults, true);

                if (isValid)
                {
                    string idContentType = contentType.GetManager().Create();
                    Container container = Container.Create(idContentType, contentType);
                    db.Container.Add(container);
                    await db.SaveChangesAsync();
                    contentType.SetContainer(container);
                    
                    return Ok(contentType);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest(type + "it is not referenced");
            }
            catch (Exception)
            {
                return BadRequest("Data sent invalid");
            }
        }

        //public IHttpActionResult PostForm(string type, FormDataCollection formData)
        //{
        //    var contentType = ContentType.GetContentType(type);
        //    ContentType.Bind(contentType, formData.ReadAsNameValueCollection());
        //    return Ok(contentType);
        //}

        [Route("ContentType/{link}")]
        [HttpOptions]
        public IHttpActionResult Option(string link, FormDataCollection formData)
        {
            return Ok();
        }

        [Route("ContentType/{link}")]
        public async Task<IHttpActionResult> Put(string link, FormDataCollection formData)
        {
            throw new NotImplementedException();
        }

        [Route("ContentType/{link}")]
        public async Task<IHttpActionResult> Delete(string link)
        {
            throw new NotImplementedException();
        }
    }
}
