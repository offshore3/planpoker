﻿appModule.service("loginService", ['$http', '$cookieStore', 'httpProxy', function ($http, $cookieStore, httpProxy) {
    
    this.login = function (email,password,successCallback, errorCallback) {
        $http.get(webAPI + "api/login?email=" + email + "&password=" + password).then(function (token) {
            if (token.data.length > 0) {
                var resultArray = token.data.split('&');
                $cookieStore.put('Authorization', resultArray[0]);
                $cookieStore.put('LoginUserId', resultArray[1]);
            }
            successCallback();
        }, function (error) {
            errorCallback(error);
        });
    };

    this.testAuthorize = function (successCallback, errorCallback) {
        httpProxy.get("api/test-authorize").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    this.testGetUser = function (successCallback, errorCallback) {
        httpProxy.get("api/user").then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.testUpdateUser = function (command, successCallback, errorCallback) {
        httpProxy({
            method: "Put",
            url: "api/user",
            data: command
        }).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    
}])