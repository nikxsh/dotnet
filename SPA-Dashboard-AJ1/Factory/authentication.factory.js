var fnAuthSvc = function (http, q) {

    var _servicebase = 'http://localhost:5658/';

    _validateUser = function (credentials) {

        var deferred = q.defer();

        http.post(_servicebase + 'api/Account/Login', credentials)
        .then(function (result) {
            //success
            deferred.resolve(result.data);
        },
        function (error) {
            //Error
            deferred.reject(error.data.error_description);
        });

        return deferred.promise;

    };    

    return {
        Login: _validateUser
    };
};