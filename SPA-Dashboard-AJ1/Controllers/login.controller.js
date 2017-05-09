var fnLoginCtrl = function (scope, window, authentication, sessionStorage) {

    scope.Credentials = {};
    scope.Message = "";
    scope.IsProgressing = false;

    scope.login = function () {

        scope.IsProgressing = true;
        scope.Message = "Authenticating Credentials....";

        var isLoggedIn = sessionStorage.AuthDataStatus();

        //Check if already authenticated
        if (isLoggedIn) {
            window.location.href = "#!/index";
        }
        else {

            var authPromise = authentication.Login(scope.Credentials);

            //If not do it
            authPromise.then(function (result) {

                if (result.responseData.isAuthenticated) {

                    sessionStorage.SetAuthData({
                        token: result.responseData.access_token
                    });

                    scope.ShowDiv = true;
                    scope.Credentials = {};
                    window.location.href = "#!/index";
                }
                else
                {
                    scope.IsProgressing = false;
                    scope.Message = result.message;
                }

            }, function (reject) {

                scope.IsProgressing = false;
                scope.Message = reject;

            });
        }
    };

    scope.logout = function () {

        scope.IsProgressing = false;
        scope.ShowDiv = false;
        scope.Message = "";
        sessionStorage.RemoveAuthData();
        window.location.href = "#!/login";

    };

    scope.getTemplateUrl = function (type) {

        var isLoggedIn = sessionStorage.AuthDataStatus();

        if (isLoggedIn) {

            switch (type) {
                case 'H':
                    return 'Templates/Navigation/Header.html';
                case 'M':
                    return 'Templates/Navigation/Messages.html';
                case 'T':
                    return 'Templates/Navigation/Tasks.html';
                case 'AL':
                    return 'Templates/Navigation/Alerts.html';
                case 'ACT':
                    return 'Templates/Navigation/Account.html';
                case 'SB':
                    return 'Templates/Navigation/Sidebar.html';
                default:
                    return '';
            }
        }
        else
            return '';
    };
};