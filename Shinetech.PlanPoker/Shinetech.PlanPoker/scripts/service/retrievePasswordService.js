appModule.service("retrievePasswordService", ['$q', '$http', function ($q,$http) {
    
    this.sendEmail = function (emailtemplate, mailtemplatecontent) {
        var sendEmailViewModel = {
            mailModel: emailtemplate,
            mailContentModel: mailtemplatecontent
        };
        return $http({
            method: "POST",
            url: webAPI + "api/sendemail",
            data: sendEmailViewModel
        }).then(function (response) {
            return $q.when(response);
        });
    };

}]);