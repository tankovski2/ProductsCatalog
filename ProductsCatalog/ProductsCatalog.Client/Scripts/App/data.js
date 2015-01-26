/// <reference path="../_references.js" />

var ProductsCatalogNS = ProductsCatalogNS || {};

ProductsCatalogNS.data = (function () {

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.manager = new breeze.EntityManager(rootUrl);
            breeze.config.initializeAdapterInstances({ dataService: "webApiOdata" });

            this.rootUrl = rootUrl;
            this.products = new ProductsPersister(rootUrl,this.manager);
            this.categories = new CategoriesPersister(rootUrl,this.manager);
        }
    });

    var ProductsPersister = Class.create({
        init: function (rootUrl, manager) {
            this.rootUrl = rootUrl;
            this.manager = manager;
        },
        loadAllProducts: function () {
            var query = new breeze.EntityQuery().from("Products").orderBy("Name");

            return this.manager.executeQuery(query);
        },
        loadProductsFromCategory: function (id) {
            var query = new breeze.EntityQuery().from("Products").orderBy("Name").where("CategoryId","==",id);

            return this.manager.executeQuery(query);
        },
        loadProductsByName: function (id) {
            var query = new breeze.EntityQuery().from("Products").orderBy("Name").where("Name", "substringof", id);

            return this.manager.executeQuery(query);
        },
        loadProductsById: function (id) {
            var query = new breeze.EntityQuery().from("Products").orderBy("Name").where("Id", "==", id);

            return this.manager.executeQuery(query);
        },
        loadProduct: function (id) {
            var query = new breeze.EntityQuery().from("Products").where("Id", "==", id);

            return this.manager.executeQuery(query);
        },
        createProduct: function (product) {
            var url = this.rootUrl + "/Products";
            product.Id = 0;
           return ProductsCatalogNS.httpRequester.postJSON(url, product);
        },
        updateProduct: function (product) {
            var url = this.rootUrl + "/Products("+product.Id+")";

            return ProductsCatalogNS.httpRequester.putJSON(url, product);
        },

        deleteProduct: function (id) {
            return ProductsCatalogNS.httpRequester.deleteJSON(this.rootUrl + "/Products(" + id + ")");
        }
    });

    var CategoriesPersister = Class.create({
        init: function (rootUrl, manager) {
            this.rootUrl = rootUrl;
            this.manager = manager;
        },
        loadAllCategories: function () {
            var query = new breeze.EntityQuery().from("Categories").orderBy("Name");

            return this.manager.executeQuery(query);
        },
        loadCategory: function (id) {
            var query = new breeze.EntityQuery().from("Categories").where("Id", "==", id);

            return this.manager.executeQuery(query);
        },
        createCategory: function (category) {
            var url = this.rootUrl + "/Categories";
            category.Id = 0;
            return ProductsCatalogNS.httpRequester.postJSON(url, category);
        },
        updateCategory: function (category) {
            var url = this.rootUrl + "/Categories(" + category.Id + ")";

            return ProductsCatalogNS.httpRequester.putJSON(url, category);
        },
        deleteCategory: function (id) {

            return ProductsCatalogNS.httpRequester.deleteJSON(this.rootUrl + "/Categories(" + id +")");
        }

    });



    return {
        get: function (roothUrl) {
            return new MainPersister(roothUrl)
        }
    }

}());