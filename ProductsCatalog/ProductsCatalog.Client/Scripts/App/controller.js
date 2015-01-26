var ProductsCatalogNS = ProductsCatalogNS || {};

ProductsCatalogNS.controller = (function () {

    var apiUrl = "http://localhost:20125/odata";
    var data = ProductsCatalogNS.data.get(apiUrl);

    function loadProductsPage() {

        loadLayout();
        data.products.loadAllProducts()
            .then(function (data) {
                $("#productsContainer").loadTemplate("../../Templates/productItemTemplate.html", data.results)
            }).catch(function (error) {
                console.log(error);
            }).done();

    }

    function loadProductsFromCategory(id) {
        if ($("#productsContainer").length < 1) {
            loadLayout();
        }

        if (id == -1) {
            data.products.loadAllProducts()
                .then(function (data) {
                    $("#productsContainer").loadTemplate("../../Templates/productItemTemplate.html", data.results)

                }).catch(function (error) {
                    console.log(error);
                }).done();
        }
        else {
            data.products.loadProductsFromCategory(id)
                .then(function (data) {
                    $("#productsContainer").loadTemplate("../../Templates/productItemTemplate.html", data.results)

                }).catch(function (error) {
                    console.log(error);
                }).done();
        }
    }

    function loadProductsById(id) {
        if ($("#productsContainer").length < 1) {
            loadLayout();
        }

        data.products.loadProductsById(id)
            .then(function (data) {
                $("#productsContainer").loadTemplate("../../Templates/productItemTemplate.html", data.results)

            }).catch(function (error) {
                console.log(error);
            }).done();
    }

    function loadProductsByName(id) {
        if ($("#productsContainer").length < 1) {
            loadLayout();
        }

        data.products.loadProductsByName(id)
            .then(function (data) {
                $("#productsContainer").loadTemplate("../../Templates/productItemTemplate.html", data.results)

            }).catch(function (error) {
                console.log(error);
            }).done();
    }

    function loadProduct(id) {
        data.products.loadProduct(id)
            .then(function (data) {
                $("#content").loadTemplate("../../Templates/productDetailTemplate.html", data.results)
                loadCategories("#categoryId", data.results[0].CategoryId);
            }).catch(function (error) {
                console.log(error);
            }).done();
    }

    function loadCreateProductPage() {
        $("#content").loadTemplate("../../Templates/productDetailTemplate.html", {})
        loadCategories("#categoryId");
    }

    function saveProduct(product) {
        debugger;
        if (product.Id) {
            data.products.updateProduct(product)
            .then(function (data) {
                //redirect to home page
                $.bbq.removeState();
            }).catch(function (error) {
                console.log(error);
            }).done();
        }
        else {
            data.products.createProduct(product)
             .then(function (data) {
                 //redirect to home page
                 $.bbq.removeState();
             }).catch(function (error) {
                 console.log(error);
             }).done();
        }
    }

    function saveCategory(category) {
        debugger;
        if (category.Id) {
            data.categories.updateCategory(category)
             .then(function (data) {
                 //redirect to home page
                 $.bbq.removeState();
             }).catch(function (error) {
                 console.log(error);
             }).done();
        }
        else {
            data.categories.createCategory(category)
             .then(function (data) {
                 //redirect to home page
                 $.bbq.removeState();
             }).catch(function (error) {
                 console.log(error);
             }).done();
        }
    }

    function loadCategory(id) {
        data.categories.loadCategory(id)
            .then(function (data) {

                $("#content").loadTemplate("../../Templates/categoryDetailTemplate.html", data.results);

            }).catch(function (error) {
                console.log(error);
            }).done();
    }

    function loadCreateCategoryPage() {
        $("#content").loadTemplate("../../Templates/categoryDetailTemplate.html", {})
    }

    //Private
    function loadLayout() {
        $("#content").loadTemplate("../../Templates/productsPageTemplate.html", {})
        loadCategories("#categoryId",-1,true);

    }

    function loadCategories(container, selectedCategory,allOption) {
        data.categories.loadAllCategories()
           .then(function (data) {
               if (allOption) {
                   data.results.unshift({ Id: -1, Name: "All" });
               }
               $(container).loadTemplate("../../Templates/categoriesListiItemTemplate.html", data.results)
               var element = $(container + ' option[value="' + selectedCategory + '"]');
               element.attr('selected', 'selected');
           }).catch(function (error) {
               console.log(error);
           }).done();
    }

    function deleteEntity(entityType, id) {
        if (entityType === "category") {
            data.categories.deleteCategory(id).then(function (data) {
                loadProductsPage();
            }).catch(function (error) {
                console.log(error);
                alert("The operation was unsecssesful");
            }).done();
        }
        else if (entityType === "product") {
            data.products.deleteProduct(id).then(function (data) {
                loadProductsPage();
            }).fail(function (error) {
                console.log(error);
                alert("The operation was unsecssesful");
            }).done();
        }
      
    }

    return {
        loadProductsPage: loadProductsPage,
        loadProductsFromCategory: loadProductsFromCategory,
        loadProduct: loadProduct,
        loadCategory: loadCategory,
        loadProductsById: loadProductsById,
        loadProductsByName: loadProductsByName,
        deleteEntity: deleteEntity,
        loadCreateProductPage: loadCreateProductPage,
        saveProduct: saveProduct,
        saveCategory: saveCategory,
        loadCreateCategoryPage: loadCreateCategoryPage
    }

}());