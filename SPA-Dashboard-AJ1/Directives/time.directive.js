
//-------------------------------------------------------------------------------------------------------------------------------------|
//  - At a high level, directives are markers on a DOM element (such as an attribute, element name, comment or CSS class) 
//    that tell AngularJS's HTML compiler ($compile) to attach a specified behavior to that DOM element (e.g. via event listeners), 
//    or even to transform the DOM element and its children.
//-------------------------------------------------------------------------------------------------------------------------------------|

var fnTimeDir = function (interval, dateFilter) {

    function _getCurrentTime(scope, element, attrs) {
        var format,
            timeoutId;

        function updateTime() {
            element.text(dateFilter(new Date(), format));
        }

        scope.$watch(attrs.currentTime, function (value) {
            format = value;
            updateTime();
        });

        element.on('$destroy', function () {
            interval.cancel(timeoutId);
        });

        // start the UI update process; save the timeoutId for canceling
        timeoutId = interval(function () {
            updateTime(); // update DOM
        }, 1000);
    };

    return {
        //-------------------------------------------------------------------------------------------------------------------------------------|
        //  - Directives that want to modify the DOM typically use the link option to register DOM listeners as well as update the DOM. 
        //  - It is executed after the template has been cloned and is where directive logic will be put.
        //  - link takes a function with the following signature, function link(scope, element, attrs, controller, transcludeFn) { ... }, 
        //-------------------------------------------------------------------------------------------------------------------------------------|
        link: _getCurrentTime
    };
};