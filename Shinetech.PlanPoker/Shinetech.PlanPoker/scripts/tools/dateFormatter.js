appModule.factory("dateFormatter", [
    function () {
        var formatDateFields = function (obj) {
            if (obj instanceof Date) return moment(obj).format("YYYY-MM-DD HH:mm");
            if (obj == null) return undefined;
            for (var prop in obj) {
                if (obj.hasOwnProperty(prop)) {
                    if (obj[prop] instanceof Date) {
                        obj[prop] = moment(obj[prop]).format("YYYY-MM-DD HH:mm");
                    } else if (typeof obj[prop] === 'object') {
                        obj[prop] = formatDateFields(obj[prop]);
                    }
                }
            }
            return obj;
        };

        return {
            formatDateFields: formatDateFields
        }
    }
]);