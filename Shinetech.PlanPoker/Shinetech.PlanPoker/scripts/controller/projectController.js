appModule.controller('projectController', ['$scope', 'projectService', function ($scope, projectService) {
    $scope.projects = {};

    $scope.currentPage = 1;
    $scope.loadProjects = function (isSearch) {
        $scope.dataLoading = true;
        if (isSearch) {
            $scope.currentPage = 1;
        }
        var command = {
            queryText: $scope.queryText,
            pageNumber: $scope.currentPage,
            pageCount: 12
        };
        projectService.queryProjects(command, function (data) {
            $scope.dataLoading = false;
            $scope.projects = data.ProjectViewModels;
            $scope.pages = range(1, data.Pages);
        }, function () {
        });
    }
    $scope.loadProjects();
    $scope.gotoPage = function (page) {
        if (page >= 1 && page <= $scope.pages.length) {
            $scope.currentPage = page;
            $scope.loadProjects();
        }
    }

}]);