using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ProductsCatalog.WebApi.Constants
{
    public static class PathConstants
    {
        private static readonly string IMAGES_FOLDER = ConfigurationManager.AppSettings["ProductsImagesDirectory"];

        public static readonly string IMAGES_ABSOLUTE_FILE_SYSTEM_PATH =
                string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, IMAGES_FOLDER);

        public static readonly string IMAGES_ABSOLUTE_WEB_PATH = string.Format("{0}://{1}/{2}", HttpContext.Current.Request.Url.Scheme,
            HttpContext.Current.Request.Url.Authority,IMAGES_FOLDER);
    }
}