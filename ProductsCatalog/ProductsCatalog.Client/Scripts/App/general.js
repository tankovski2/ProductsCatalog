/// <reference path="../_references.js" />

$(function () {

    function getFormatedSubstring(value, charsCount, bufferCharsLenght) {
        var seeMoreChars = "...";
        var seperator = " ";

        if (value.length > charsCount + seeMoreChars.length) {

            var reminder = value.substring(charsCount);
            var firstEmptySpace = reminder.indexOf(seperator);
            if (firstEmptySpace > 0 && firstEmptySpace < bufferCharsLenght) {
                return value.substring(0, charsCount + firstEmptySpace) + seeMoreChars;
            }
            else if (firstEmptySpace > 0) {
                return value.substring(0, charsCount) + seeMoreChars;
            }
        }
        return value;
    }

    $.addTemplateFormatter({
        substringLong: function (value) {
            return getFormatedSubstring(value, 100, 20);
        }
    });

    $.addTemplateFormatter({
        substringShort: function (value) {
            return getFormatedSubstring(value, 50, 20);
        }

    });

    //Change page
    $(document).on("click", "a.bbq-link", function () {
        var currentItem = $(this);
        addHashTags(currentItem);
        return false;
    });

    $(document).on("click", "button.bbq-link", function () {
        var currentItem = $(this).siblings("input");
        var pageName = currentItem.data("page-name");
        var state = {};
        state["page"] = pageName;
        var id = currentItem.val();
        if (id) {
            state["id"] = id;
            $.bbq.pushState(state, false);
        }

        return false;
    });

    $(document).on("change", ".bbq-link", function () {
        var currentItem = $(this).find(":selected");
        addHashTags(currentItem);
    });
    //End Change page

    //Submit

    $(document).on("click", "#submit-button-products", function () {
       
        var button = $(this);
        var container = button.parents("#submit-container");
        var product = getDataFromInputs(container);
        product["Description"] = $("#description").val();
        product["CategoryId"] = $("#categoryId option:selected").val();
        product["ImagePath"] = $("#picture").attr("src");
        var fileUpload = $("#product-image").get(0);

        if (fileUpload.files && fileUpload.files[0]) {
            readImage(fileUpload).then(function (data) {
                product["Image"] = data;
                ProductsCatalogNS.controller.saveProduct(product);
            }).done();
        }
        else {
            ProductsCatalogNS.controller.saveProduct(product);
        }

        return false;
    })

    $(document).on("click", "#submit-button-category", function () {
        debugger;
        var button = $(this);
        var container = button.parents("#submit-container");
        var category = getDataFromInputs(container);
        category["Description"] = $("#description").val();

        ProductsCatalogNS.controller.saveCategory(category);

        return false;
    })

    //End Submit

    $(document).on("click", ".btn-delete", function () {
        var attribute = $(this).data("entity");
        var desssion = confirm("Are you sure you want to delete ''" + $("#name").val() + "'");
        if (desssion == true) {
            var id = $("#id").val();
            ProductsCatalogNS.controller.deleteEntity(attribute, id);
        }
        return false;
       
        
    });

    //Load page for the first time
    ProductsCatalogNS.router.changePage();

    //Change page
    $(window).on("hashchange", function () {
        ProductsCatalogNS.router.changePage();
    })


    //Private
    function addHashTags($item) {
            var pageName = $item.data("page-name");
            var id = $item.data("id");
            var state = {};
            state["page"] = pageName;
            if (id) {
                state["id"] = id;
            }

            $.bbq.pushState(state, false);
    }

    function getDataFromInputs($container) {
        var inputs = $container.find("input[type!=file]");
        data = {};
        inputs.each(function (index, element) {
            var input = $(this);
            var name = input.attr("name");
            var value = input.val();
            data[name] = value;
        })

        return data;
    }
})

function readImage(input) {
    var deferred = Q.defer();
        var FR = new FileReader();
        FR.onload = function (e) {
            deferred.resolve(e.target.result.replace(/^.*?,/, ''));
        };
        FR.readAsDataURL(input.files[0]);

    return deferred.promise;
}