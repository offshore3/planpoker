appModule.service("retrievePasswordService", ['$q', '$http', function ($q,$http) {
    
    this.sendEmail = function (emailtemplate, mailtemplatecontent) {
        return $http({
            method: "POST",
            url: webAPI + "api/sendemail?mailModel=" + emailtemplate + "&mailContentModel="+mailtemplatecontent,
        }).then(function (response) {
            return $q.when(response);
        });
    };

}]);