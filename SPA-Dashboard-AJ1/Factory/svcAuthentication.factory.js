
module.factory('svcAuthentication', ['$http', function ($http) {

    return {

        validateUser: function (credentials) {

            $http.post("http://localhost:5658/api/Account/Login", credentials)
            .then(function (result) {
                //success
                if (result.responseData.isAuthenticated)
                    return true;
                else
                    return false;
            },
            function () {
                return false;
            });
        }
    };
}]);