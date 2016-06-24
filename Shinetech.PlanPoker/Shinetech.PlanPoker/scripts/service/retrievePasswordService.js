appModule.service("retrievePasswordService", ['$q', '$http', 'httpProxy', function ($q, $http, httpProxy) {
    
    this.sendEmail = function (emailtemplate, mailtemplatecontent) {
        var sendEmailViewModel = {
            mailViewModel: emailtemplate,
            mailContentViewModel: mailtemplatecontent
        };
        return $http({
            method: "POST",
            url: webAPI + "api/sendforgetpasswordmail",
            data: sendEmailViewModel
        }).then(function (response) {
            return $q.when(response);
        });
    };

    this.decryptResetPasswordCode = function (resetPasswordToken, successCallback, errorCallback) {
        httpProxy.get("api/resetpassworddecrypt?resetPasswordToken=" + resetPasswordToken).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });

    };

}]);