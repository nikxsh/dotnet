
module.factory('localStorage', ['$window', '$rootScope', function ($window, $rootScope) {

    var _setAuthData = function (val) {
        $window.localStorage.setItem('authorization_data', JSON.stringify(val));
        return this;
    };

    var _getAuthData = function () {
        var data = JSON.parse($window.localStorage.getItem('authorization_data'));
        return data;
    };

    var _getAuthDataStatus = function () {
        return $window.localStorage.getItem('authorization_data') !== null;
    };

    var _removeAuthData = function () {
        $window.localStorage.removeItem('authorization_data');
    };




    return {
        SetAuthData: _setAuthData,
        GetAuthData: _getAuthData,
        RemoveAuthData: _removeAuthData,
        AuthDataStatus : _getAuthDataStatus
    }
}]);


module.factory('sessionStorage', ['$window', '$rootScope', function ($window, $rootScope) {

    var _setAuthData = function (val) {
        $window.sessionStorage.setItem('authorization_data', JSON.stringify(val));
        return this;
    };

    var _getAuthData = function () {
        var data = JSON.parse($window.sessionStorage.getItem('authorization_data'));
        return data;
    };

    var _getAuthDataStatus = function () {
        return $window.sessionStorage.getItem('authorization_data') !== null;
    };

    var _removeAuthData = function () {
        $window.sessionStorage.removeItem('authorization_data');
    };




    return {
        SetAuthData: _setAuthData,
        GetAuthData: _getAuthData,
        RemoveAuthData: _removeAuthData,
        AuthDataStatus: _getAuthDataStatus
    }
}]);