﻿appModule.service("dashboardService", ['$http', 'httpProxy', function ($http, httpProxy) {
    
    this.getProjectParticipates = function (projectId, successCallback, errorCallback) {
        httpProxy.get("api/participates?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    this.decryptProjectCode = function (projectCode, successCallback, errorCallback) {
        httpProxy.get("api/projectcode?projectCode=" + projectCode).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });

    };

    this.selectCard = function (command,successCallback,errorCallback) {
        httpProxy.post("api/estimate", command).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    };


}]);