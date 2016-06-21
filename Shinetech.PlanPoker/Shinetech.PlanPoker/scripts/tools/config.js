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