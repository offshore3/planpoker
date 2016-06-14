appModule.controller('projectController', [
    '$scope', 'projectService', function($scope, projectService) {
        $scope.projects = {};

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

        $scope.showParticipateModal = function (projectId) {
            projectService.getProjectParticipates(projectId, function (data) {
                $scope.participates = data;
                $("#participatesProject").modal("show");
            }, function() {

            });
            
        };

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

        $scope.save = function () {
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