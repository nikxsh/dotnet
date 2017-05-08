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

    var _getToken = function () {

        var data = "grant_type=password&username=" + credentials.username + "&password=" + credentials.password;

        var deferred = q.defer();

        http.post(_servicebase + 'oauth/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
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
        Login: _validateUser,
        GetToken: _getToken
    };
};