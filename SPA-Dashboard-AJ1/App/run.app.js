
var fnAppRun = function (rootScope, window, location, sessionStorage) {

    rootScope.$watch(function () {

        var isLoggedIn = sessionStorage.AuthDataStatus();

        //if not logged in
        if (!isLoggedIn)
            window.location.href = "#!/login";

        var currentPath = location.path().split("/")[1] || "Unknown";

        //If state is login page but you're logged in already
        if (isLoggedIn && currentPath === 'login')
            location.path('index');

        return;
    });

    var isLoggedIn = sessionStorage.AuthDataStatus();

    //NOT authenticated 
    if (!isLoggedIn) {

        window.location.href = "#!/login";
        return;
    }

    //authenticated already
    if (isLoggedIn) {

        window.location.href = "#!/index";
        return;
    }

};