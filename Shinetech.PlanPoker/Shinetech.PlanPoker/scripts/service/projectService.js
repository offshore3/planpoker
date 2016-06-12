appModule.service("projectService", ['httpProxy', function (httpProxy) {
    this.queryProjects = function (command,successCallback, errorCallback) {
        httpProxy.get("api/projects", {
            params: {
                pageNumber: command.pageNumber,
                pageCount: command.pageCount,
                queryText: command.queryText ? command.queryText : undefined
            }
        }).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
}]);

