using Microsoft.Data.OData;
using ProductsCatalog.Data;
using ProductsCatalog.Models;
using ProductsCatalog.WebApi.Constants;
using ProductsCatalog.WebApi.Helpers;
using ProductsCatalog.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;

namespace ProductsCatalog.WebApi.Controllers
{
    public class ProductsController : EntitySetController<ProductViewModel, int>
    {

        #region Private Fields

        private IUowData data;

        #endregion

        #region Constructors
        public ProductsController(IUowData data)
        {
            this.data = data;
        }

        #endregion

        #region Public Methods

        [EnableQuery]
        public override IQueryable<ProductViewModel> Get()
        {
            IQueryable<ProductViewModel> products = data.Products.All().Select(ProductViewModel.FromProduct);

            return products;
        }

        public override void Delete(int key)
        {
            var product = data.Products.GetById(key);
            if (product == null)
            {
                throw new HttpResponseException(
                      Request.CreateErrorResponse(
                      HttpStatusCode.NotFound,
                      new ODataError
                      {
                          ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_FOUND,
                          Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_PRODUCT_MESSAGE_FORMAT, key)
                      }));
            }
            data.Products.Delete(product);
            data.SaveChanges();
        }

        #endregion

        #region Protected Methods

        protected override ProductViewModel GetEntityByKey(int key)
        {
            ProductViewModel product = data.Products.All().Select(ProductViewModel.FromProduct)
                .FirstOrDefault(prod => prod.Id == key);
            if (product == null)
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_FOUND,
                        Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_PRODUCT_MESSAGE_FORMAT, key)
                    }));
            }

            return product;
        }

        protected override ProductViewModel CreateEntity(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ValidateCategory(model.CategoryId);

                Product product = model.FromViewModel();
                data.Products.Add(product);

                SaveEntity(model, product,false);

                model.Id = product.Id;

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

        protected override int GetKey(ProductViewModel model)
        {
            return model.Id;
        }

        protected override ProductViewModel UpdateEntity(int key, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ValidateCategory(model.CategoryId);
                if (!data.Products.All().Any(prod => prod.Id == key))
                {
                    throw new HttpResponseException(
                        Request.CreateErrorResponse(
                        HttpStatusCode.NotFound,
                        new ODataError
                        {
                            ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_FOUND,
                            Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_PRODUCT_MESSAGE_FORMAT, key)
                        }));
                }
                model.Id = key; // ignore the ID in the entity use the ID in the URL.
                Product product = model.FromViewModel();
                //string oldPath = product.ImageName;

                data.Products.Update(product);
                SaveEntity(model, product,true);

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

        #region Private Methods

        private void ValidateCategory(int categoryId)
        {
            Category category = data.Categories.GetById(categoryId);
            if (category == null)
            {
                throw new HttpResponseException(
               Request.CreateErrorResponse(
               HttpStatusCode.NotFound,
               new ODataError
               {
                   ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_VALID,
                   Message = string.Format(ErrorsConstants.ODATA_ERROR_NOT_FOUND_CATEGORY_MESSAGE_FORMAT, categoryId)
               }));
            }
        }

        private void SaveEntity(ProductViewModel model, Product product, bool removeOldData)
        {
            string oldImageName = product.ImageName;
            using (TransactionScope transaction = new TransactionScope())
            {
                data.SaveChanges();

                if (!string.IsNullOrWhiteSpace(model.Image))
                {
                    try
                    {
                        product.ImageName = ImageHelper.CreateImage(model.Image, product.Id);
                        data.SaveChanges();
                        transaction.Complete();
                    }
                    catch (Exception)
                    {
                        throw new HttpResponseException(
                                     Request.CreateErrorResponse(
                                     HttpStatusCode.BadRequest,
                                     new ODataError
                                     {
                                         ErrorCode = ErrorsConstants.ODATA_ERROR_CODE_NOT_VALID,
                                         Message = ErrorsConstants.ODATA_ERROR_MESSAGE_NOT_VALID_IMAGE
                                     }));
                    }
                    //Not important for the transaction
                    if (removeOldData && !string.IsNullOrWhiteSpace(oldImageName))
                    {
                        ImageHelper.DeleteImage(oldImageName);
                    }

                }
                else
                {
                    transaction.Complete();
                }
            }
        }

        #endregion
    }
}
