
module.controller('LoginController', ['$scope', '$rootScope', '$window', '$routeParams', 'authentication', 'sessionStorage', function ($scope, $rootScope, $window, $routeParams, authentication, sessionStorage) {

    $scope.Credentials = {};
    $scope.ShowDiv = sessionStorage.AuthDataStatus();
    $scope.Message = "";
    $scope.IsProgressing = false;

    $scope.login = function () {
        
        $scope.IsProgressing = true;
        $scope.Message = "Authenticating Credentials....";

        var isLoggedIn = sessionStorage.AuthDataStatus();

        //Check if already authenticated
        if (isLoggedIn) {
            $window.location.href = "#!/index";
        }
        else {

            var authPromise = authentication.login($scope.Credentials);

            //If not do it
            authPromise.then(function (result) {

                sessionStorage.SetAuthData({
                    token: result.access_token
                });

                $scope.ShowDiv = true;
                $scope.Credentials = {};
                $window.location.href = "#!/index";

            }, function (reject) {

                $scope.IsProgressing = false;
                $scope.Message = reject;

            });
        }
    };

    $scope.logout = function () {

        $scope.IsProgressing = false;
        $scope.ShowDiv = false;
        $scope.Message = "";
        sessionStorage.RemoveAuthData();
        $window.location.href = "#!/login";

    };

}]);