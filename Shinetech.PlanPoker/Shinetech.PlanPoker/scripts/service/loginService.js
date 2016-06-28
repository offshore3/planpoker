﻿appModule.service("loginService", ['$http', '$cookieStore', 'httpProxy', function ($http, $cookieStore, httpProxy) {
    
    this.login = function (email,password,successCallback, errorCallback) {
        $http.get(webAPI + "api/login?email=" + email + "&password=" + password).then(function (token) {
            if (token.data.length > 0) {
                var resultArray = token.data.split('&');
                $cookieStore.put('Authorization', resultArray[0]);
                $cookieStore.put('LoginUserId', resultArray[1]);
                $http.defaults.headers.common['Authorization'] = $cookieStore.get('Authorization');
                $http.defaults.headers.common['LoginUserId'] = $cookieStore.get('LoginUserId');
                successCallback();
            } else {
                errorCallback();
            }
        });
    };

    this.getUser = function (successCallback, errorCallback) {
        httpProxy.get("api/user").then(function (data) {
            successCallback(data);
        }, function () {

        });
    }

    this.getOrCreateUser = function (user,successCallback, errorCallback) {
        $http.post(webAPI + "api/unionlogin", user).then(function (token) {
            var resultArray = token.data.split('&');
            $cookieStore.put('Authorization', resultArray[0]);
            $cookieStore.put('LoginUserId', resultArray[1]);
            $http.defaults.headers.common['Authorization'] = $cookieStore.get('Authorization');
            $http.defaults.headers.common['LoginUserId'] = $cookieStore.get('LoginUserId');
            successCallback();
        }, function () {

        });
    }

}])