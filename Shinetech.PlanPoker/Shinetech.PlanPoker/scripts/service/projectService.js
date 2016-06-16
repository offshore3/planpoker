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

    this.deleteParticipate = function (participateId, successCallback, errorCallback) {
        httpProxy.Delete("api/participates?participateId=" + participateId).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }

    //begin  add by Jimbo 2016-06-16 09:48:33

    this.getInvite = function (projectId, email, successCallback, errorCallback) {
        httpProxy.get("api/get-invite-by-projectid-email?projectId=" + projectId + "&email=" + email).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    };

    //this.inviteUser = function (inviteParticipate, successCallback, errorCallback) {
    //    httpProxy.post("api/invite-participate", inviteParticipate).then(function (data) {
    //        successCallback(data);
    //    }, function (error) {
    //        errorCallback(error);
    //    });
    //}

    this.createInvite = function (invite, successCallback, errorCallback) {
        httpProxy.post("api/participate", invite).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    };
    //end  add by Jimbo 2016-06-16 09:48:33

    this.createProject = function (project, successCallback, errorCallback) {
        httpProxy.post("api/project", project).then(function (data) {
            successCallback(data);
        }, function (error) {
            errorCallback(error);
        });
    }
}]);

