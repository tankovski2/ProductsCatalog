/// <reference path="../_references.js" />

var ProductsCatalogNS = ProductsCatalogNS || {};

ProductsCatalogNS.router = (function () {
    function changePage() {
        var page = $.bbq.getState("page");
        if (!page) {
            ProductsCatalogNS.controller.loadProductsPage();
        }
        else if (page == "ProductsFromCategory") {
            var categoryId = $.bbq.getState("id");
            ProductsCatalogNS.controller.loadProductsFromCategory(categoryId);
        }
        else if (page == "ProductDetail") {
            var productId = $.bbq.getState("id");
            ProductsCatalogNS.controller.loadProduct(productId);
        }
        else if (page == "CreateProduct") {
            ProductsCatalogNS.controller.loadCreateProductPage();
        }
        else if (page == "ProductsByName") {
            var name = $.bbq.getState("id");
            ProductsCatalogNS.controller.loadProductsByName(name);
        }
        else if (page == "ProductsById") {
            var productId = $.bbq.getState("id");
            ProductsCatalogNS.controller.loadProductsById(productId);
        }
        else if (page == "CategoryDetail") {
            var categoryId = $.bbq.getState("id");
            ProductsCatalogNS.controller.loadCategory(categoryId);
        }
        else if (page=="CreateCategory") {
            ProductsCatalogNS.controller.loadCreateCategoryPage();
        }
    }

    return {
        changePage:changePage
    }

}());