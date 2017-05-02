
module.controller('LoginController', ['$scope', '$rootScope', '$window', '$routeParams', 'authentication', 'localStorage', function ($scope, $rootScope, $window, $routeParams, authentication, localStorage) {

    $scope.Credentials = {};
    $scope.ShowDiv = localStorage.AuthDataStatus();
    $scope.Message = "";
    $scope.IsProgressing = false;

    $scope.login = function () {
        
        $scope.IsProgressing = true;
        $scope.Message = "Authenticating Credentials....";

        var isLoggedIn = localStorage.AuthDataStatus();

        //Check if already authenticated
        if (isLoggedIn) {
            $window.location.href = "#!/index";
        }
        else {

            var authPromise = authentication.login($scope.Credentials);

            //If not do it
            authPromise.then(function (result) {

                localStorage.SetAuthData({
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
        localStorage.RemoveAuthData();
        $window.location.href = "#!/login";

    };

}]);