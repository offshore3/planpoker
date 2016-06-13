using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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



            foreach (var p in provider.Contents.Where(x => x.Headers.ContentType != null))
            {
                var stream = await p.ReadAsStreamAsync();
                return ImgUpload(p.Headers.ContentDisposition.FileName, stream);
            }

            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

        }

        private string ImgUpload(string fileNameStr, Stream stream)
        {
            var dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            fileNameStr = fileNameStr.Replace('"', ' ').Trim();
            var lastDot = fileNameStr.LastIndexOf('.');
            var type = fileNameStr.Substring(lastDot + 1).ToLower();
            var fileName = fileNameStr.Substring(0, lastDot);
            var fileSaveName = dateTime + "_" + fileName + "." + type;

            if (fileSaveName.FileIsImage() && !fileSaveName.FileIsPNG())
            {
                stream = stream.StreamChangeOrientationAndSize();

            }
            string path = HttpContext.Current.Server.MapPath("\\Image");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Bitmap imageBitmap = new Bitmap(stream);
            var imagePath = path + "\\" + fileSaveName;
            imageBitmap.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            return imagePath.Substring(imagePath.LastIndexOf("Image", StringComparison.Ordinal));
        }
    }
}
