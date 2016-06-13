using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public static class FileTypeFilter
    {
        public static string FilterImageFile(this string url)
        {
            var fileName = url.Split('_').LastOrDefault();

            var imageTypes = new[]
            {
                ".jpg",".png",".jpeg"
            };

            return fileName != null && imageTypes.Any(fileName.ToLower().Contains) ? "image" : fileName;
        }

        public static bool FileIsImage(this string url)
        {
            var fileName = url.Split('_').LastOrDefault();

            var imageTypes = new[]
            {
                ".jpg",".png",".jpeg"
            };

            return fileName != null && imageTypes.Any(fileName.ToLower().Contains);
        } 
        
        public static bool FileIsPNG(this string url)
        {
            var fileName = url.Split('_').LastOrDefault();

            var imageTypes = new[]{ ".png" };

            return fileName != null && imageTypes.Any(fileName.ToLower().Contains);
        }

        public static Bitmap BitmapResize(this Image mSrcImage, int dHeight, int dWidth)
        {
            int sW, sH;
            var temSize = new Size(mSrcImage.Width, mSrcImage.Height);

            if (temSize.Height > dHeight || temSize.Width > dWidth)
            {
                if ((temSize.Width / dHeight) > (temSize.Height / dWidth))
                {
                    sW = dWidth;
                    sH = (temSize.Height * dWidth) / temSize.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (temSize.Width * dHeight) / temSize.Height;
                }
            }
            else
            {
                sH = temSize.Height;
                sW = temSize.Width;
            }

            var bbmp = new Bitmap(sW, sH);
            using (var mGraphics = Graphics.FromImage(bbmp))
            {
                mGraphics.SmoothingMode = SmoothingMode.HighQuality;
                mGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                mGraphics.DrawImage(mSrcImage, 0, 0, bbmp.Width, bbmp.Height);
            }
            return bbmp;
        }

        public static Stream StreamChangeOrientationAndSize(this Stream stream)
        {
            Stream targetStream = new MemoryStream();
            var img = Image.FromStream(stream);

            if (img.Width > 850)
            {
                var width = 850;
                var height = img.Height * (width / Convert.ToDecimal(img.Width));
                img = img.BitmapResize((int)height, width);
            }

            var jpgEncoder = ImageCodecInfo.GetImageDecoders().Single(a => a.FormatID == ImageFormat.Jpeg.Guid);
            var jpgEncoderParameters = new EncoderParameters(1);
            jpgEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 70L);

            img.Save(targetStream, jpgEncoder, jpgEncoderParameters);
            return targetStream;
        }
        public static byte[] ImageChangeOrientation(this byte[] buffer)
        {
            var ms = new MemoryStream(buffer);
            ms.Flush();

            var mSrcImage = Image.FromStream(ms);

            if (Array.IndexOf(mSrcImage.PropertyIdList, 274) > -1)
            {
                var orientation = (int)mSrcImage.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        mSrcImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                        break;
                    case 2:
                        mSrcImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        mSrcImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        mSrcImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        mSrcImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        mSrcImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        mSrcImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        mSrcImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                mSrcImage.RemovePropertyItem(274);
            }

            ms = new MemoryStream();
            mSrcImage.Save(ms, ImageFormat.Jpeg);
            var bufferReturn = ms.GetBuffer();
            ms.Close();
            return bufferReturn;
        }

        public static async Task<Image> DownloadImageFromUrl(this string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return null;

            try
            {
                var webRequest = (HttpWebRequest)HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                using (var webResponse = await webRequest.GetResponseAsync())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await responseStream.CopyToAsync(memoryStream);
                            memoryStream.Position = 0;
                            return Image.FromStream(memoryStream);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}