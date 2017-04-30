
module.controller('LoginController', ['$scope', '$rootScope', '$window', '$routeParams', 'authentication', 'localStorage', function ($scope, $rootScope, $window, $routeParams, authentication, localStorage) {

    $scope.Credentials = {};
    $scope.ShowDiv = localStorage.GetAuthData();
    $scope.Message = "";
    $scope.IsProgressing = false;

    $scope.login = function () {


        $scope.IsProgressing = true;
        $scope.Message = "Authenticating Credentials....";

        var isLoggedIn = localStorage.GetAuthData();

        //Check if already authenticated
        if (isLoggedIn) {
            $window.location.href = "#!/index";
        }
        else {

            var authPromise = authentication.validateUser($scope.Credentials);

            //If not do it
            authPromise.then(function (result) {
                if (result.responseData.isAuthenticated) {

                    $scope.ShowDiv = true;
                    localStorage.SetAuthData(true);
                    $scope.Credentials = {};
                    $window.location.href = "#!/index";

                }
                else {

                    $scope.IsProgressing = false;
                    localStorage.SetAuthData(false);
                    $scope.Credentials = {};
                    $scope.Message = "Invalid Credentials!";

                }
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
        localStorage.SetAuthData(false);
        $window.location.href = "#!/login";

    };

}]);