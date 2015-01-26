using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsCatalog.WebApi.Constants
{
    public static class ErrorsConstants
    {
        public const string ODATA_ERROR_CODE_NOT_FOUND = "EntityNotFound";
        public const string ODATA_ERROR_NOT_FOUND_PRODUCT_MESSAGE_FORMAT = "Product with id {0} not found";
        public const string ODATA_ERROR_CODE_NOT_VALID = "NotValidEntity";
        public const string ODATA_ERROR_MESSAGE_NOT_VALID_MODEL = "Model is not valid";
        public const string ODATA_ERROR_MESSAGE_NOT_VALID_IMAGE = "Image is not valid";
        public const string ODATA_ERROR_NOT_FOUND_CATEGORY_MESSAGE_FORMAT = "Category with id {0} not found";
    }
}