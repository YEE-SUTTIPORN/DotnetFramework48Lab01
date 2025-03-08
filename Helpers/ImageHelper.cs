using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DotnetFramework48Lab01.Helpers
{
    public class ImageHelper
    {
        public static byte[] ResizeAndConvertToByteArray(HttpPostedFileBase file, int maxWidth, int maxHeight)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            using (var originalImage = Image.FromStream(file.InputStream))
            {
                int newWidth = originalImage.Width;
                int newHeight = originalImage.Height;

                // คำนวณขนาดใหม่ตามสัดส่วน
                if (originalImage.Width > maxWidth || originalImage.Height > maxHeight)
                {
                    double ratioX = (double)maxWidth / originalImage.Width;
                    double ratioY = (double)maxHeight / originalImage.Height;
                    double ratio = Math.Min(ratioX, ratioY);

                    newWidth = (int)(originalImage.Width * ratio);
                    newHeight = (int)(originalImage.Height * ratio);
                }

                using (var resizedImage = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                    }

                    using (var ms = new MemoryStream())
                    {
                        resizedImage.Save(ms, ImageFormat.Jpeg); // บันทึกเป็น JPEG เพื่อลดขนาดไฟล์
                        return ms.ToArray(); // แปลงเป็น byte[]
                    }
                }
            }
        }

    }
}