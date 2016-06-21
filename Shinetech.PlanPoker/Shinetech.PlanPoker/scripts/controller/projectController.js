appModule.controller('projectController', [
    '$scope','$location', 'projectService', 'loginService', 'retrievePasswordService', '$cookieStore', 'emailtemplate', 'mailtemplatecontent', function ($scope,$location, projectService, loginService, retrievePasswordService,$cookieStore, emailtemplate, mailtemplatecontent) {
        $scope.projects = {};
        $scope.LoginUserId = $cookieStore.get("LoginUserId");
        $scope.emailtemplate = emailtemplate;
        $scope.mailtemplatecontent = mailtemplatecontent.invitemail;

        $scope.currentPage = 1;
        $scope.loadProjects = function(isSearch) {
            if (isSearch) {
                $scope.currentPage = 1;
            }
            var command = {
                queryText: $scope.queryText,
                pageNumber: $scope.currentPage,
                pageCount: 12
            };
            projectService.queryProjects(command, function(data) {
                $scope.projects = data.ProjectViewModels;
                $scope.pages = range(1, data.Pages);
                $scope.$emit('dashboardParentReloadProject');
            }, function() {
            });

        }
        $scope.loadProjects();
        $scope.gotoPage = function(page) {
            if (page >= 1 && page <= $scope.pages.length) {
                $scope.currentPage = page;
                $scope.loadProjects();
            }
        }

        $scope.showModal = function() {
            $scope.clearForm();
            $scope.newProject = {};
            $("#projectCreateAndEdit").modal("show");
        };

        $scope.clearForm = function() {
            $scope.projectForm.$setPristine();
            $scope.newProject = {};
        };

        $scope.loadParticipates = function (projectId) {
            projectService.getProjectParticipates(projectId, function (data) {
                $scope.participates = data;
                $scope.isShowParticipates = data.length > 0;
            }, function () {

            });
        }

        $scope.showParticipateModal = function (projectId) {
            $scope.InviteEmail = '';
            $scope.currentProjectId = projectId;
            $scope.loadParticipates(projectId);
            $("#participatesProject").modal("show");
        };

        $scope.showDeleteParticipateModal = function (participate) {
            $scope.deleteParticipate = participate;
            $("#deleteParticipatesModal").modal("show");
        };

        $scope.participateDelete = function () {
            projectService.deleteParticipate($scope.deleteParticipate.Id, function () {
                $scope.loadParticipates($scope.deleteParticipate.ProjectId);
                $("#deleteParticipatesModal").modal("hide");
            }, function () {
            });
        }

        $scope.inviteUser = function() {
            var command = {
                ProjectId: $scope.currentProjectId,
                Email: $scope.InviteEmail
            }
            //begin  add by Jimbo 2016-06-16 09:48:33
            loginService.getUser(function (data) {
                if (data.Email.toLowerCase() == $scope.InviteEmail.toLowerCase())
                {
                    $scope.isInviteSelf = true;
                    return;
                }
            });

            projectService.getInvite(command.ProjectId,command.Email, function (data) {
                if (data === null) {
                    projectService.createInvite(command, function () {
                        $scope.loadParticipates($scope.currentProjectId);
                        $scope.sendEmail();
                    }, function () {

                    });
                } else if (data != null && data.IsRegister){
                    $scope.isInvted = true;
                } else if (data != null && !data.IsRegister) {
                    $scope.sendEmail();
                }

            }, function () {
            });
            //end  add by Jimbo 2016-06-16 09:48:33
        }

        $scope.changeEmail = function () {
            $scope.isSendEmail = false;
            $scope.isInviteSelf = false;
            $scope.isInvted = false;
        };

        $scope.sendEmail = function () {
            $scope.emailtemplate.absUrl = $location.absUrl();
            $scope.emailtemplate.emailto = $scope.InviteEmail;
            $scope.emailtemplate.emailcode = $scope.currentProjectId;
            retrievePasswordService.sendEmail($scope.emailtemplate, $scope.mailtemplatecontent).then(function (response) {
                $scope.isSendEmail = true;
            }, function () {

            });
        };

        $scope.closeDeleteParticipateModal= function() {
            $("#deleteParticipatesModal").modal("hide");
        }

        $scope.closeParticipatesModal = function() {
            $("#participatesProject").modal("hide");
        }

        $scope.showEditModal = function (project) {
            $scope.newProject = angular.copy(project);

            $("#projectCreateAndEdit").modal("show");
        };

        $scope.showDeleteModal = function(project, $index) {
            $scope.deleteProject = project;
            $scope.deleteProject.deleteIndex = $index;
            $("#deleteProject").modal("show");
        };

        $scope.projectDelete = function() {
            projectService.deleteProject($scope.deleteProject.Id, function() {
                $("#deleteProject").modal("hide");
                $scope.loadProjects();
            }, function() {
            });
        }

        $scope.saveProject = function () {
            var project = angular.copy($scope.newProject);
            if (project.Id) {
                projectService.editProject(project, function () {
                    $scope.loadProjects();
                    $("#projectCreateAndEdit").modal("hide");
                }, function (error) {
                });
            } else {
                projectService.createProject(project, function () {
                    $scope.loadProjects();
                    $("#projectCreateAndEdit").modal("hide");
                }, function (error) {
                });
            }

        };

        $scope.cancleProjectModal = function () {
            $(".cancle-project").click(function () {
                $("#projectCreateAndEdit").modal("hide");
            });
        };

        $scope.deleteProjectModal = function () {
            $(".delete-project").click(function () {
                $("#deleteProject").modal("hide");
            });
        };
    }
])