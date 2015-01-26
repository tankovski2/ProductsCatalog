using ProductsCatalog.Data;
using ProductsCatalog.Models;
using ProductsCatalog.WebApi.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData;
using System.Net;
using Microsoft.Data.OData;
using ProductsCatalog.WebApi.Constants;

namespace ProductsCatalog.WebApi.Controllers
{
    public class CategoriesController : EntitySetController<CategoryViewModel, int>
    {

        #region Private Fields

        private IUowData data;

        #endregion

        #region Controllers

        public CategoriesController(IUowData data)
        {
            this.data = data;
        }

        #endregion

        #region Public Methods

        [EnableQuery]
        public override IQueryable<CategoryViewModel> Get()
        {
            IQueryable<CategoryViewModel> categories = data.Categories.All().Select(CategoryViewModel.FromCategory);

            return categories;
        }

        public override void Delete(int key)
        {
            var category = data.Categories.GetById(key);
            if (category == null)
            {
                throw new HttpResponseException(
                      Request.CreateErrorResponse(
                      HttpStatusCode.NotFound,
                      new ODataError
                      {
                          ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_FOUND,
                          Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_CATEGORY_MESSAGE_FORMAT, key)
                      }));
            }
            data.Categories.Delete(category);
            data.SaveChanges();
        }

        #endregion

        #region Protected Methods

        protected override CategoryViewModel GetEntityByKey(int key)
        {
            CategoryViewModel category = data.Categories.All().Select(CategoryViewModel.FromCategory).FirstOrDefault(cat => cat.Id == key);
            if (category == null)
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_FOUND,
                        Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_CATEGORY_MESSAGE_FORMAT, key)
                    }));
            }

            return category;
        }

        protected override CategoryViewModel CreateEntity(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = model.FromViewModel();
                data.Categories.Add(category);
                data.SaveChanges();
                model.Id = category.Id;

                return model;
            }

            throw new HttpResponseException(
                   Request.CreateErrorResponse(
                   HttpStatusCode.BadRequest,
                   new ODataError
                   {
                       ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_VALID,
                       Message = ErrorsConstants.ODATA_ERROR_MESSAGE_NOT_VALID_MODEL
                   }));
        }

        protected override int GetKey(CategoryViewModel model)
        {
            return model.Id;
        }

        protected override CategoryViewModel UpdateEntity(int key, CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!data.Categories.All().Any(c => c.Id == key))
                {
                    throw new HttpResponseException(
                        Request.CreateErrorResponse(
                        HttpStatusCode.NotFound,
                        new ODataError
                        {
                            ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_FOUND,
                            Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_CATEGORY_MESSAGE_FORMAT, key)
                        }));
                }
                model.Id = key; // ignore the ID in the entity use the ID in the URL.
                Category category = model.FromViewModel();

                data.Categories.Update(category);
                data.SaveChanges();

                return model;
            }

            throw new HttpResponseException(
                   Request.CreateErrorResponse(
                   HttpStatusCode.BadRequest,
                   new ODataError
                   {
                       ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_VALID,
                       Message = ErrorsConstants.ODATA_ERROR_MESSAGE_NOT_VALID_MODEL
                   }));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            data.Dispose();
        }

        #endregion
    }
}