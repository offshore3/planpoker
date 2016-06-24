appModule.controller('projectController', [
    '$scope','$location', 'projectService', 'loginService', '$cookieStore', 'emailtemplate', 'mailtemplatecontent', function ($scope,$location, projectService, loginService,$cookieStore, emailtemplate, mailtemplatecontent) {
        $scope.projects = {};
        $scope.LoginUserId = $cookieStore.get("LoginUserId");
        $scope.emailtemplate = emailtemplate;
        $scope.mailtemplatecontent = mailtemplatecontent.invitemail;

        $scope.currentPage = 1;
        $scope.isProjectSearchBusy = false;
        $scope.loadProjects = function (isSearch) {
            $scope.isProjectSearchBusy = true;
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
                $scope.isProjectSearchBusy = false;
            }, function () {
                $scope.isProjectSearchBusy = false;
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

        $scope.isParticipateDeleteBusy = false;
        $scope.participateDelete = function () {
            $scope.isParticipateDeleteBusy = true;
            projectService.deleteParticipate($scope.deleteParticipate.Id, function () {
                $scope.loadParticipates($scope.deleteParticipate.ProjectId);
                $("#deleteParticipatesModal").modal("hide");
                $scope.isParticipateDeleteBusy = false;
            }, function () {
                $scope.isParticipateDeleteBusy = false;
            });
        }

        $scope.isParticipateInviteBusy = false;
        $scope.inviteUser = function () {
            $scope.isParticipateInviteBusy = true;
            var command = {
                ProjectId: $scope.currentProjectId,
                Email: $scope.InviteEmail
            }

            loginService.getUser(function (data) {
                if (data.Email.toLowerCase() === $scope.InviteEmail.toLowerCase()) {
                    $scope.isInviteSelf = true;
                    $scope.isParticipateInviteBusy = false;
                }
                else
                {
                    projectService.getInvite(command.ProjectId, command.Email, function (data) {
                        if (data === null) {
                            projectService.createInvite(command, function () {
                                $scope.loadParticipates($scope.currentProjectId);
                                $scope.sendEmail();
                            }, function () {

                            });
                        } else if (data != null && data.IsRegister) {
                            $scope.isInvted = true;
                        } else if (data != null && !data.IsRegister) {
                            $scope.sendEmail();
                        }
                        $scope.isParticipateInviteBusy = false;
                    }, function () {
                        $scope.isParticipateInviteBusy = false;
                    });
                }
            });

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
            projectService.sendEmail($scope.emailtemplate, $scope.mailtemplatecontent,function (response) {
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

        $scope.isProjectDeleteBusy = false;
        $scope.projectDelete = function () {
            $scope.isProjectDeleteBusy = true;
            projectService.deleteProject($scope.deleteProject.Id, function() {
                $("#deleteProject").modal("hide");
                $scope.loadProjects();
                $scope.isProjectDeleteBusy = false;
            }, function () {
                $scope.isProjectDeleteBusy = false;
            });
        }

        $scope.isProjectSaveBusy = false;
        $scope.saveProject = function () {
            $scope.isProjectSaveBusy = true;
            var project = angular.copy($scope.newProject);
            if (project.Id) {
                projectService.editProject(project, function () {
                    $scope.loadProjects();
                    $("#projectCreateAndEdit").modal("hide");
                    $scope.isProjectSaveBusy = false;
                }, function (error) {
                });
            } else {
                projectService.createProject(project, function () {
                    $scope.loadProjects();
                    $("#projectCreateAndEdit").modal("hide");
                    $scope.isProjectSaveBusy = false;
                }, function (error) {
                    $scope.isProjectSaveBusy = false;
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