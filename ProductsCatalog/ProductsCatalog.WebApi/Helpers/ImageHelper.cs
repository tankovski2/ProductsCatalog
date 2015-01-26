using ProductsCatalog.WebApi.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace ProductsCatalog.WebApi.Helpers
{
    /// <summary>
    /// Helper that works with images
    /// </summary>
    public static class ImageHelper
    {
        public static string CreateImage(string imgBase64, int productId)
        {
            byte[] imgBytes = Convert.FromBase64String(imgBase64);

            string imgName = string.Format("{0}_{1}.png", GetTimeStamp(), productId);

            using (MemoryStream ms = new MemoryStream(imgBytes))
            {
                Image image = Image.FromStream(ms);
                string absoluteImagePath = Path.Combine(PathConstants.IMAGES_ABSOLUTE_FILE_SYSTEM_PATH, imgName);
                image.Save(absoluteImagePath, ImageFormat.Png);
            }

            return imgName;
        }

        public static bool DeleteImage(string filePath)
        {
            string absoluteFilePath = Path.Combine(PathConstants.IMAGES_ABSOLUTE_FILE_SYSTEM_PATH, filePath);
            if (File.Exists(absoluteFilePath))
            {
                File.Delete(absoluteFilePath);
               
                return true;
            }

            return false;
        }

        private static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }
    }
}