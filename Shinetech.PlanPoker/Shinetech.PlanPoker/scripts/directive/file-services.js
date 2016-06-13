appModule.service('fileServices', ["fileUploadQueue", function ( fileUploadQueue) {
        this.uploadFileToUrl = function (file, uploadUrl, progressCallback, successCallback, errorCallback) {
            var fd = new FormData();
            fd.append('file', file);

            var request = new window.XMLHttpRequest();

            var progressObj = fileUploadQueue.queueUpload(file);
            request.upload.addEventListener('progress', progressObj.progressFunc);
            if (progressCallback) request.upload.addEventListener('progress', progressCallback);

            if (errorCallback) request.upload.addEventListener('error', errorCallback);
            request.onreadystatechange = function () {
                if (request.readyState === 4 && request.status === 200) {
                    successCallback(request.response.substr(1, request.response.length - 2));
                    fileUploadQueue.completeUpload(progressObj);
                }
            };
            request.open("POST", uploadUrl);
            request.send(fd);
        };
    }
]).service('multipleFileServices', ["fileUploadQueue", function ( fileUploadQueue) {
        this.uploadFileToUrl = function (file, uploadUrl) {

            return new window.Promise(function (resolve, reject) {
                var fd = new FormData();
                fd.append('file', file);

                var request = new window.XMLHttpRequest();

                var progressObj = fileUploadQueue.queueUpload(file);
                request.upload.addEventListener('progress', progressObj.progressFunc);

                request.onload = function () {
                    if (request.status === 200) {
                        resolve(request.response.substr(1, request.response.length - 2));
                        fileUploadQueue.completeUpload(progressObj);
                    }
                    else {
                        reject(Error(request.statusText));
                    }
                };

                request.onerror = function () {
                    reject(Error(Arguments));
                };

                request.open("POST", uploadUrl);
                request.send(fd);
            });
        };
    }
]);

appModule.service("fileUploadQueue", ["$rootScope", "$timeout",
    function ($rootScope, $timeout) {
        this.queueUpload = function (file) {
            var progressObj = {
                fileName: file.name,
                fileSize: file.size
            };
            var progressFunc = function (progress) {
                $timeout(function () {
                    progressObj.uploadedPercent = (progress.loaded / progress.total).toFixed(2) * 100;
                    if (progressObj.uploadedPercent > 98) progressObj.uploadedPercent = 98;
                });
            };
            progressObj.progressFunc = progressFunc;
            if (!$rootScope.uploadQueue) $rootScope.uploadQueue = [];
            $rootScope.uploadQueue.push(progressObj);
            $rootScope.showingUploadQueue = true;

            return progressObj;
        }

        this.completeUpload = function (progressObj) {
            $timeout(function () {
                $rootScope.uploadQueue.splice($rootScope.uploadQueue.indexOf(progressObj), 1);
                if ($rootScope.uploadQueue.length === 0) {
                    $rootScope.showingUploadQueue = false;
                }
            });
        }
    }
]);