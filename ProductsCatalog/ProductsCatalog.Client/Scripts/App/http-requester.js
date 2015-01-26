var ProductsCatalogNS = ProductsCatalogNS || {};

ProductsCatalogNS.httpRequester = (function () {

    function getJSON(serviceUrl, headers) {
        var deferred = Q.defer();
            jQuery.ajax({
                url: serviceUrl,
                type: "GET",
                dataType: "json",
                headers: headers,
                success: function (data) {
                    deferred.resolve(data);
                },
                error: function (err) {
                    deferred.reject(err);
                }
            });
       
            return deferred.promise;;
    }

    function postJSON(serviceUrl, data, headers) {
        var deferred = Q.defer();
            jQuery.ajax({
                url: serviceUrl,
                dataType: "json",
                headers: headers,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function (data) {
                    deferred.resolve(data);
                },
                error: function (err) {
                    deferred.reject(err);
                }
            });
     
        return deferred.promise;
    }

    function putJSON(serviceUrl, data, headers) {
        var deferred = Q.defer();
        jQuery.ajax({
            url: serviceUrl,
            dataType: "json",
            headers: headers,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (err) {
                deferred.reject(err);
            }
        });

        return deferred.promise;
    }

    function deleteJSON(serviceUrl) {
        var deferred = Q.defer();
        jQuery.ajax({
            url: serviceUrl,
            type: "DELETE",
            dataType: "json",
          
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (err) {
                deferred.reject(err);
            }
        });

        return deferred.promise;
    }

    return {
        getJSON: getJSON,
        postJSON: postJSON,
        putJSON: putJSON,
        deleteJSON: deleteJSON
    }
}());