
'use strict';
module.factory('authInterceptor', ['$location', '$q', 'sessionStorage', function ($location, $q, sessionStorage) {

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = sessionStorage.GetAuthData();
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    };

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    return {
        request: _request,
        responseError: _responseError
    };

}]);