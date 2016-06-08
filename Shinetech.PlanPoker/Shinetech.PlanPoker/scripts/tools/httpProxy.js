appModule.factory("httpProxy", ["$http", "$q", "$cookieStore", "dateFormatter", function ($http, $q, $cookieStore, dateFormatter) {
    var baseUrl = webAPI;
    $http.defaults.headers.common['Authorization'] = $cookieStore.get('Authorization');
    $http.defaults.headers.common['LoginUserId'] = $cookieStore.get('LoginUserId');

    function servicesError(deferred, error, errorCode) {
        deferred.reject(error);
    }

    return {
        get: function (action, option) {
            var deferred = $q.defer();
            var canceler = $q.defer();
            var defaultOption = {
                method: "Get",
                url: baseUrl + action,
                cache: false,
                timeout: canceler.promise
            };
            option = $.extend(defaultOption, option);
            $http(option).success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                servicesError(deferred, data);
            });
            deferred.promise.canceler = canceler;
            return deferred.promise;
        },
        post: function (action, data, option) {
          
            var deferred = $q.defer();
            var defaultOption = {
                method: "Post",
                data: dateFormatter.formatDateFields(data),
                url: baseUrl + action,
                cache: false
            };
            option = $.extend(defaultOption, option);
            $http(option).success(function (successResult) {
                deferred.resolve(successResult);
            }).error(function (errorResult, errorCode) {
                servicesError(deferred, errorResult, errorCode);
            });

            return deferred.promise;
        },

        put: function (action, data, option) {
            var deferred = $q.defer();
            var defaultOption = {
                method: "Put",
                data: dateFormatter.formatDateFields(data),
                url: baseUrl + action,
                cache: false
            };
            option = $.extend(defaultOption, option);
            $http(option).success(function (successResult) {
                deferred.resolve(successResult);
            }).error(function (errorResult) {
                servicesError(deferred, errorResult);
            });

            return deferred.promise;
        },

        Delete: function (action, data, option) {
            var deferred = $q.defer();
            var defaultOption = {
                method: "Delete",
                data: data,
                url: baseUrl + action,
                cache: false
            };
            option = $.extend(defaultOption, option);
            $http(option).success(function (successResult) {
                deferred.resolve(successResult);
            }).error(function (errorResult) {
                servicesError(deferred, errorResult);
            });

            return deferred.promise;
        }
    }
}]);