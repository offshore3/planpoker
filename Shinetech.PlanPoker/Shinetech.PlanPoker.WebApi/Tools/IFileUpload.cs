using System.IO;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public interface IFileUpload
    {
        string ImgUpload(string fileNameStr, Stream stream);
    }
}