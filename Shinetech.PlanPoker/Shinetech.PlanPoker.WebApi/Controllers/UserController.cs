using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Web;
using System.Net.Http;
using System.IO;
using System.Globalization;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UserController : BaseController
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic userLogic) : base(userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [Route("user")]
        public void Create(UserViewModel userViewModel) {
            var userLogicModel = userViewModel.ToLogicModel();
            _userLogic.Create(userLogicModel);
        }

        [HttpGet]
        [Route("users")]
        public List<UserViewModel> GetUsers()
        {
            return _userLogic.GetAll().Select(x => new UserViewModel
            {
                Email = x.Email,
                Name = x.Name,
                Password = x.Password
            }).ToList();
        }

        [HttpGet]
        [Route("checkemail")]
        public bool CheckEmailExist(string email)
        {
            return _userLogic.CheckEmailExist(email);
        }
        [HttpGet]
        [Route("login")]
        public string Login(string email, string password)
        {
            return _userLogic.Login(email, password);
        }

        [HttpGet]
        [Route("user")]
        [BasicAuthorize]
        public UserViewModel GetUserViewModel()
        {
            return LoginUser;
        }

        [HttpPut]
        [Route("user")]
        [BasicAuthorize]
        public void EditUser(UserViewModel userViewModel)
        {
            _userLogic.Edit(userViewModel.ToLogicModel());
        }

        //     [HttpPost]
        //     [Route("Upload/ImgUpload")]
        //     public void ImgUpload(HttpContext context)
        //     {
        //        Hashtable hash = new Hashtable();

        //        if (context.Request.QueryString["fileName"] != null)
        //        {
        //            string urlFilePath = context.Request.QueryString["fileName"];
        //            string fileName = urlFilePath.Substring(urlFilePath.LastIndexOf("/") + 1);
        //            string filePath = context.Request.MapPath(urlFilePath.Substring(0, urlFilePath.LastIndexOf("/")));
        //            if (File.Exists(filePath + "/" + fileName))
        //            {
        //                File.Delete(filePath + "/" + fileName);
        //                hash.Add("error", 0);
        //                hash.Add("url", "../Images/no_pic.gif");
        //            }
        //            else
        //            {
        //                hash.Add("error", 1);
        //                hash.Add("message", "未找到原文件！");
        //            }
        //        }
        //        else
        //        {
        //            String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

        //            //文件保存目录路径
        //            String savePath = "../FileUpload/";

        //            //文件保存目录URL
        //            String saveUrl = "../FileUpload/";

        //            //定义允许上传的文件扩展名
        //            Hashtable extTable = new Hashtable();
        //            extTable.Add("tempPhoto", "gif,jpg,jpeg,png,bmp");

        //            //最大文件大小
        //            int maxSize = 500;
        //            //this.context = context;

        //            HttpPostedFile imgFile = context.Request.Files[0];
        //            if (imgFile == null)
        //            {
        //                //showError("请选择文件。");
        //            }

        //            String dirPath = context.Server.MapPath(savePath);
        //            if (!Directory.Exists(dirPath))
        //            {
        //                //showError("上传目录不存在。");
        //            }

        //            String dirName = "tempPhoto";
        //            if (!extTable.ContainsKey(dirName))
        //            {
        //                //showError("目录名不正确。");
        //            }

        //            String fileName = imgFile.FileName;
        //            String fileExt = Path.GetExtension(fileName).ToLower();

        //            if (imgFile.InputStream == null || (imgFile.ContentLength / 1024) > maxSize)
        //            {
        //                //showError("上传文件大小超过限制,照片大小不能超过500K!");
        //            }

        //            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        //            {
        //                //showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        //            }
        //            System.Drawing.Image photo = System.Drawing.Bitmap.FromStream(imgFile.InputStream);
        //            if (photo.Width > 480 || photo.Width < 390 || photo.Height > 640 || photo.Height < 567)
        //            {
        //                //showError("照片尺寸不合格：照片尺寸(480至390)px * (640至567)px");
        //            }

        //            //创建文件夹
        //            dirPath += dirName + "/";
        //            saveUrl += dirName + "/";
        //            if (!Directory.Exists(dirPath))
        //            {
        //                Directory.CreateDirectory(dirPath);
        //            }
        //            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
        //            dirPath += ymd + "/";
        //            saveUrl += ymd + "/";
        //            if (!Directory.Exists(dirPath))
        //            {
        //                Directory.CreateDirectory(dirPath);
        //            }

        //            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        //            String filePath = dirPath + newFileName;

        //            imgFile.SaveAs(filePath);

        //            String fileUrl = saveUrl + newFileName;

        //            hash["error"] = 0;
        //            hash["url"] = fileUrl;
        //        }
        //        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        //        //context.Response.Write(Json.ToJson(hash));
        //        context.Response.End();

        //        //// 检查是否是 multipart/form-data 
        //        //if (!Request.Content.IsMimeMultipartContent("form-data"))
        //        //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

        //        //// 文件保存目录路径 
        //        //const string saveTempPath = "~/UploadFiles/";
        //        //var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);

        //        //// 设置上传目录 
        //        //var provider = new MultipartFormDataStreamProvider(dirTempPath);

        //        //var task = Request.Content.ReadAsMultipartAsync(provider).
        //        //    ContinueWith<Hashtable>(o =>
        //        //    {
        //        //        var hash = new Hashtable();
        //        //        var file = provider.FileData[0];
        //        //        // 最大文件大小
        //        //        const int maxSize = 10000000;
        //        //        // 定义允许上传的文件扩展名 
        //        //        const string fileTypes = "gif,jpg,jpeg,png,bmp";

        //        //        //// token验证
        //        //        //var token = string.Empty;
        //        //        //try
        //        //        //{
        //        //        //   token = provider.FormData["token"].ToString();
        //        //        //}
        //        //        //catch (Exception exception)
        //        //        //{
        //        //        //    throw new Exception("未附带token，非法访问!", exception);
        //        //        //}

        //        //        //if (!string.IsNullOrEmpty(token))
        //        //        //{
        //        //        //    if (!CacheManager.TokenIsExist(token))
        //        //        //    {
        //        //        //        throw new UserLoginException("Token已失效，请重新登陆!");
        //        //        //    }
        //        //        //    if (accountInfoService.Exist_User_IsForzen(AccountHelper.GetUUID(token)))
        //        //        //    {
        //        //        //        CacheManager.RemoveToken(token);
        //        //        //        tempCacheService.Delete_OneTempCaches(new Guid(token));
        //        //        //        throw new UserLoginException("此用户已被冻结,请联系管理员（客服）!");
        //        //        //    }
        //        //        //}
        //        //        //else
        //        //        //{
        //        //        //    throw new Exception("token为空，非法访问!");
        //        //        //}

        //        //        string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
        //        //        var fileinfo = new FileInfo(file.LocalFileName);
        //        //        if (fileinfo.Length <= 0)
        //        //        {
        //        //            hash["Code"] = -1;
        //        //            hash["Message"] = "请选择上传文件。";
        //        //        }
        //        //        else if (fileinfo.Length > maxSize)
        //        //        {
        //        //            hash["Code"] = -1;
        //        //            hash["Message"] = "上传文件大小超过限制。";
        //        //        }
        //        //        else
        //        //        {
        //        //            var fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));

        //        //            if (String.IsNullOrEmpty(fileExt) ||
        //        //                Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
        //        //            {
        //        //                hash["Code"] = -1;
        //        //                hash["Message"] = "不支持上传文件类型。";
        //        //            }
        //        //            else
        //        //            {
        //        //                try
        //        //                {
        //        //                    //AttachmentFileResultDTO attachmentFileResult;
        //        //                    //attachmentFileService.UploadAttachmentFile(fileinfo, dirTempPath, fileExt,
        //        //                    //    Path.GetFileNameWithoutExtension(file.LocalFileName), out attachmentFileResult);

        //        //                    //hash["Code"] = 0;
        //        //                    //hash["Message"] = "上传成功";
        //        //                    //hash["FileId"] = attachmentFileResult.ID;
        //        //                    //hash["FileName"] = attachmentFileResult.FileName + attachmentFileResult.FileType;
        //        //                    //hash["NetImageUrl"] = attachmentFileResult.FileUrl;

        //        //                    dirPath += dirName + "/";
        //        //                    saveUrl += dirName + "/";
        //        //                    if (!Directory.Exists(dirPath))
        //        //                    {
        //        //                        Directory.CreateDirectory(dirPath);
        //        //                    }
        //        //                    String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
        //        //                    dirPath += ymd + "/";
        //        //                    saveUrl += ymd + "/";
        //        //                    if (!Directory.Exists(dirTempPath))
        //        //                    {
        //        //                        Directory.CreateDirectory(dirTempPath);
        //        //                    }

        //        //                    String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        //        //                    String filePath = dirTempPath + newFileName;

        //        //                    imgFile.SaveAs(filePath);

        //        //                    String fileUrl = saveUrl + newFileName;

        //        //                    hash["error"] = 0;
        //        //                    hash["url"] = fileUrl;
        //        //                }
        //        //                catch (Exception exception)
        //        //                {
        //        //                    throw new Exception("上传失败!", exception);
        //        //                }
        //        //            }
        //        //        }
        //        //        return hash;
        //        //    });
        //        //return task;

    }
}