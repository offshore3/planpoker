

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public class FileUpload:IFileUpload
    {
         string IFileUpload.ImgUpload(string fileNameStr, Stream stream)
        {
            return ImgUpload(fileNameStr, stream);
        }

        public static string ImgUpload(string fileNameStr, Stream stream)
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
            var path = HttpContext.Current.Server.MapPath("\\Image");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var imageBitmap = new Bitmap(stream);
            var imagePath = path + "\\" + fileSaveName;
            imageBitmap.Save(imagePath, ImageFormat.Jpeg);
            return WebConfigurationManager.AppSettings["ApiPath"] +
                   imagePath.Substring(imagePath.LastIndexOf("Image", StringComparison.Ordinal));
        }
    }
}