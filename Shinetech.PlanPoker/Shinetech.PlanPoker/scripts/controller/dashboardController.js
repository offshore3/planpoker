appModule.controller('dashboardController', ['$scope', 'dashboardService', function ($scope, dashboardService) {
    $scope.pokers = {
        poker: [
            { data: 1, myStyle: { "left": "0px" } },
            { data: 2, myStyle: { "left": "70px" } },
            { data: 3, myStyle: { "left": "140px" } },
            { data: 5, myStyle: { "left": "210px" } },
            { data: 8, myStyle: { "left": "280px" } },
            { data: 13, myStyle: { "left": "350px" } },
            { data: 20, myStyle: { "left": "420px" } },
            { data: 40, myStyle: { "left": "490px" } },
            { data: 100, myStyle: { "left": "560px" } },
            { data: 's', myStyle: { "left": "630px" } },
            { data: 'm', myStyle: { "left": "700px" } },
            { data: 'l', myStyle: { "left": "770px" } },
            { data: 'xs', myStyle: { "left": "840px" } },
            { data: 'coffee', myStyle: { "left": "910px" } },
            { data: 'yes', myStyle: { "left": "980px" } },
            { data: 'no', myStyle: { "left": "1050px" } }
        ]
    }
    $scope.selectedPoker = {
        text: "?",
        data: ""
    };
    $scope.cardSelect = function (poker) {
        $scope.isFloat = poker.data;
        $scope.selectedPoker.data = poker.data;
        $scope.selectedPoker.text = "√";
    }
}]);


