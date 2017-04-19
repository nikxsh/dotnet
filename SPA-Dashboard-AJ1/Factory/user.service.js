module.factory("UserDataService", ["$http", "$q", function ($http, $q) {

    var _users = [];
    var _user = {};

    var _getUsers = function (pagingRequest) {

        var promise = $http.post("http://localhost:5658/api/users", pagingRequest)
                .then(function (result) {
                    //Succes
                    return result.data.responseData;
                },
                function () {
                    //Error
                });
        return promise;
    };

    var _getUserCount = function () {
        var promise = $http.get("http://localhost:5658/api/users/count")
                        .then(function (result) {
                            //Succes
                            return result.data.responseData;
                        },
                        function () {
                            //Error
                        });
        return promise;
    };

    var _getUserSearch = function (keyword) {
        var promise = $http.get("http://localhost:5658/api/users/" + keyword + "/search")
                        .then(function (result) {
                            //Succes
                            return result.data;
                        },
                        function () {
                            //Error
                        });
        return promise;
    };

    var _getUserById = function (id) {

        var deferred = $q.defer();

        $http.get("http://localhost:5658/api/users/" + id)
        .then(function (result) {
            //Succes
            angular.copy(result.data, _user);
            deferred.resolve(result);
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    };

    var _addUser = function (newUser) {
        var deferred = $q.defer();

        $http.post("http://localhost:5658/api/users/add", newUser)
        .then(function (result) {
            //success
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    };

    var _editUser = function (user) {
        var deferred = $q.defer();

        $http.post("http://localhost:5658/api/users/edit", user)
        .then(function (result) {
            //success
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    }

    var _deleteUser = function (userId) {
        var deferred = $q.defer();

        $http.post("http://localhost:5658/api/users/" + userId + "/delete")
        .then(function (result) {
            //success
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    }

    return {
        Users: _users,
        User: _user,
        UserById: _getUserById,
        GetUsers: _getUsers,
        AddUser: _addUser,
        EditUser: _editUser,
        DeleteUser: _deleteUser,
        UserCount: _getUserCount,
        UserSearch: _getUserSearch
    };
}]);