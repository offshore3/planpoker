using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Shinetech.PlanPoker.WebApi.Tools;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UpLoadFileController : ApiController
    {
        [HttpPost]
        [Route("picture/upload")]
        public async Task<string> UploadPicture()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(provider);

            var content = provider.Contents.FirstOrDefault(x => x.Headers.ContentType != null);
            if (content != null)
            {
                var stream = await content.ReadAsStreamAsync();

                return FileUpload.ImgUpload(content.Headers.ContentDisposition.FileName, stream);
            }
            return string.Empty;
        }
    }
}