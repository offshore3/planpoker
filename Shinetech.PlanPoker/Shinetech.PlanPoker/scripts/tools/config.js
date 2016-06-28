function range(start, end) {
    var arr = [];
    for (var i = start; i <= end; i++) {
        arr.push(i);
    }
    return arr;
}


function getQueryVariable(name) {
    var query = window.location.href.substring(window.location.href.lastIndexOf('?')+1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] === name) { return pair[1]; }
    }
    return null;
}

E = function (array) {
    return Enumerable.From(array);
};

function onLinkedInLoad() {
    IN.Event.on(IN, "auth", getProfileData);
}

// Handle the successful return from the API call
function onSuccess(data) {
    var rootScope = angular.element("html").scope();
    rootScope.$broadcast("socialLoginSuccess", data);
    //console.log(data);
}

// Handle an error response from the API call
function onError(error) {
    console.log(error);
}

// Use the API call wrapper to request the member's basic profile data
function getProfileData() {
    IN.API.Raw("/people/~").result(onSuccess).error(onError);
}

function LinkedInSignOut() {
    IN.User.logout(function() {
        console.log("LinkedIn user sign out.");
    });
}
