appModule.directive("fileReader", function ($q, $parse) {
    var slice = Array.prototype.slice;
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, elements, attrs, ngModel) {

            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            if (!ngModel) {
                return;
            }

            ngModel.$render = function () { };
            elements.bind("change", function (element) {

                scope.$apply(function () {
                    modelSetter(scope, elements[0].files[0]);
                });

                var elementFile = element.target;
                $q.all(slice.call(elementFile.files, 0).map(readFile))
                    .then(function (values) {
                        if (elementFile.multiple) ngModel.$setViewValue(values);
                        else ngModel.$setViewValue(values.length ? values[0] : null);
                    });

                

                function readFile(file) {
                    var deferred = $q.defer();

                    var reader = new FileReader();
                    reader.onload = function (element) {
                        deferred.resolve(element.target.result);
                    };
                    reader.onerror = function (element) {
                        deferred.reject(element);
                    };
                    reader.readAsDataURL(file);
                    return deferred.promise;
                }
            });
        }
    };
});

appModule.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);