var fnModalInstanceCtlr = function (uibModalInstance, scope, rootScope, UserDataService, Constants) {
    var $ctrl = this;
    var pagingRequest = { PageNumber: 1, PageSize: Constants.PageTabelRowsSize };

    $ctrl.ok = function (isNewUser) {

        if (isNewUser) {

            UserDataService.AddUser(scope.user)
                .then(function (result) {
                    //Success
                    uibModalInstance.close("Done");
                    //Update Grid
                    UserDataService.GetUsers(pagingRequest)
                        .then(function (result) {
                            //Succes
                            rootScope.$broadcast('updateList', { data: result });
                        },
                        function () {
                            //error
                        });
                },
                function () {
                    //error
                    uibModalInstance.close("Error");
                });
        }
        else if (!isNewUser) {

            UserDataService.EditUser(scope.user)
                .then(function (result) {
                    //Success
                    uibModalInstance.close("Done");
                    //Update Grid
                    UserDataService.GetUsers(pagingRequest)
                        .then(function (result) {
                            //Succes
                            rootScope.$broadcast('updateList', { data: result });
                        },
                        function () {
                            //error
                        });
                },
                function () {
                    //error
                    uibModalInstance.close("Error");
                });
        }
        $ctrl.Reset();
    };

    $ctrl.Delete = function (id) {

        UserDataService.DeleteUser(id)
            .then(function (result) {
                //Success
                uibModalInstance.close("Done");
                //Update Grid
                UserDataService.GetUsers(pagingRequest)
                .then(function (result) {
                    //Succes
                    rootScope.$broadcast('updateList', { data: result });
                },
                function () {
                    //error
                });
            },
            function () {
                //error
                uibModalInstance.close("Error");
            });

    }

    $ctrl.Reset = function () {
        scope.user = angular.copy({
            email: '',
            dob: ''
        });
    }

    $ctrl.cancel = function () {
        uibModalInstance.dismiss('cancel');
    };
};