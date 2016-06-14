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

    this.deleteProject = function (projectId, successCallback, errorCallback) {
        httpProxy.Delete("api/project?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.editProject = function (project, successCallback, errorCallback) {
        httpProxy.put("api/project",project).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.createProject = function (project, successCallback, errorCallback) {
        httpProxy.post("api/project",project).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    this.getProjectParticipates = function (projectId, successCallback, errorCallback) {
        httpProxy.get("api/participates?projectId=" + projectId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
    
    

}]);

