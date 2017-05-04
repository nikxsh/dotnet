
var fnUserCtlr = function (scope, UserDataService, Constants) {

    scope.Name = "Users";
    scope.PagingRequest = { PageNumber: 1, PageSize: Constants.PageTabelRowsSize, SearchString: '' };
    scope.maxSize = 10;
    scope.BigTotalItems = 0;
    scope.BigCurrentPage = 1;
    scope.ItemsPerPage = Constants.PageTabelRowsSize;
    scope.PagedList = [{ id: 1, name: "5" }, { id: 2, name: "10" }, { id: 3, name: "50" }];
    scope.FirstTimeLoad = true;
    scope.UserData = [];

    if (scope.FirstTimeLoad) {
        
        _setUserCount();
        _setUserData(scope.PagingRequest);

        scope.FirstTimeLoad = false;
    }

    scope.$on("updateList", function (e, result) {
        scope.UserData = result.data;
        
        _setUserCount();

        scope.BigCurrentPage = 1;
    });

    scope.PageChanged = function () {
        //what to do on page change
        scope.PagingRequest.PageNumber = scope.BigCurrentPage;
        _setUserData(scope.PagingRequest);
    };

    scope.CustomRowsSelected = function () {

        scope.PagingRequest.PageSize = parseInt(scope.PagedList.name.name);
        scope.PagingRequest.PageNumber = 1;
        scope.ItemsPerPage = scope.PagingRequest.PageSize;
        _setUserData(scope.PagingRequest);
    };

    scope.Search = function () {
        if (scope.keyword != '' && scope.keyword != undefined) {

            scope.PagingRequest.SearchString = scope.keyword;
            UserDataService.GetUsers(scope.PagingRequest)
            .then(function (result) {
                //Succes
                if (result.length > 0) {
                    scope.BigTotalItems = result.length;
                    scope.UserData = result;
                }
                else {
                    _setUserCount();
                    scope.UserData = {};
                }
            },
            function () {
                //error
            });
        }
        else {

            scope.PagingRequest.SearchString = '';
            _setUserData(scope.PagingRequest);
            _setUserCount();

        }
    };

    function _setUserCount() {

        UserDataService.UserCount()
        .then(function (data) {
            //success
            scope.BigTotalItems = data;
        },
        function () {
            //error
        });
    };

    function _setUserData(request) {

        UserDataService.GetUsers(request)
        .then(function (result) {
            //Success
            scope.UserData = result;
        },
        function () {
            //error
        });
    }
};