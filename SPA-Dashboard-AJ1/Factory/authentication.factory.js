
module.factory('authentication', ['$http', '$q', function ($http, $q) {

    return {

        validateUser: function (credentials) {

            var deferred = $q.defer();

            $http.post("http://localhost:5658/api/Account/Login", credentials)
            .then(function (result) {
                //success
                deferred.resolve(result.data);
            },
            function () {
                //Error
                deferred.reject('Service call failed. Error Code: 23');
            });

            return deferred.promise;
        }
    };
}]);