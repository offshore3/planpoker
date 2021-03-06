﻿appModule.service("dashboardService", ['$http', 'httpProxy', function ($http, httpProxy) {
    
    this.getProjectParticipates = function (projectId, successCallback, errorCallback) {
        httpProxy.get("api/participates?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    this.decryptProjectCode = function (projectCode, successCallback, errorCallback) {
        httpProxy.get("api/projectdecrypt?projectCode=" + projectCode).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });

    };

    this.encryptProjectCode = function (projectId, successCallback, errorCallback) {
        httpProxy.get("api/projectencrypt?projectId=" + projectId).then(function (data) {
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

    this.getEstimateUsers = function (projectId, successCallback, errorCallback) {
        httpProxy.get("api/estimates?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    };

    this.showEstimate = function (projectId,successCallback, errorCallback) {
        httpProxy.get("api/showestimate?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback();
        });
    }

    this.getMonitorUrl = function (projectId, successCallback, errorCallback) {
        httpProxy.get("api/monitor?projectId=" + projectId).then(function () {
        }, function (error) {
            errorCallback();
        });
    }

    this.removeEstimate = function (projectId, successCallback, errorCallback) {

        httpProxy.Delete("api/estimate?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

}]);