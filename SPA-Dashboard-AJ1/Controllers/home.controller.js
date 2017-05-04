
//-------------------------------------------------------------------------------------------------------------------------------------|
// - When a Controller is attached to the DOM via the ng-controller directive, AngularJS will instantiate a new Controller object, 
//   using the specified Controller's constructor function. A new child scope will be created and made available as an injectable 
//   parameter to the Controller's constructor function as $scope.
//
//  - Use controllers to:
//      Set up the initial state of the $scope object.
//      Add behavior to the $scope object.
//
//  - Do not use controllers to:
//     Manipulate DOM — Controllers should contain only business logic. Putting any presentation logic into Controllers significantly 
//                      affects its testability. AngularJS has databinding for most cases and directives to encapsulate manual 
//                      DOM manipulation.
//     Format input — Use AngularJS form controls instead.
//     Filter output — Use AngularJS filters instead.
//     Share code or state across controllers — Use AngularJS services instead.
//     Manage the life-cycle of other components (for example, to create service instances).
//-------------------------------------------------------------------------------------------------------------------------------------|

module.controller('HomeController', ['$scope', '$window', function ($scope, $window) {

    //-------------------------------------------------------------------------------------------------------------------------------------|
    // - Scope is an object that refers to the application model. 
    // - It is an execution context for expressions. Scopes are arranged in hierarchical structure which mimic the DOM structure of 
    //   the application. 
    // - Scopes can watch expressions and propagate events.
    // - Scope is the glue between application controller and the view. During the template linking phase the directives set up 
    //   $watch expressions on the scope. The $watch allows the directives to be notified of property changes, which allows the directive 
    //   to render the updated value to the DOM.
    // - Both controllers and directives have reference to the scope, but not to each other. This arrangement isolates the controller from
    //   the directive as well as from the DOM
    //-------------------------------------------------------------------------------------------------------------------------------------|
    $scope.title = 'I am Blank';

    $scope.messages = [
                       { id: 1, name: 'Nikhilesh Shinde', date: 'Today', message: 'Hello there!!!' },
                       { id: 1, name: 'Asawari lol', date: 'Yesterday', message: 'Why this kolawari' },
                       { id: 1, name: 'John Smith', date: 'Yesterday', message: 'Sky full of lighters!' }
    ];

    $scope.tasks = [
                   { id: 1, name: 'Task 1', status: '40%', progress: 'success', value: 40 },
                   { id: 1, name: 'Task 2', status: '60%', progress: 'warning', value: 60 },
                   { id: 1, name: 'Task 3', status: '80%', progress: 'danger', value: 80 }
    ];
}]);

//-------------------------------------------------------------------------------------------------------------------------------------|
//  Scope Life Cycle
//  - The normal flow of a browser receiving an event is that it executes a corresponding JavaScript callback. Once the callback 
//    completes the browser re-renders the DOM and   returns to waiting for more events.
//  - When the browser calls into JavaScript the code executes outside the AngularJS execution context, which means that AngularJS is 
//    unaware of model modifications. To properly   process model modifications the execution has to enter the AngularJS execution 
//    context using the $apply method. Only model modifications which execute inside the $apply  method will be properly accounted for 
//    by AngularJS. For example if a directive listens on DOM events, such as ng-click it must evaluate the expression inside the $apply
//    method.
//  - After evaluating the expression, the $apply method performs a $digest. In the $digest phase the scope examines all of the $watch 
//    expressions and compares them with the   previous value. This dirty checking is done asynchronously. This means that assignment such
//    as $scope.username="angular" will not immediately cause a $watch to be notified,  instead the $watch notification is delayed until 
//    the $digest phase. This delay is desirable, since it coalesces multiple model updates into one $watch notification as well as    
//    guarantees that during the $watch notification no other $watches are running. If a $watch changes the value of the model, it will 
//    force additional $digest cycle.
//
//   1 => Creation: The root scope is created during the application bootstrap by the $injector. During template linking, some directives 
//        create new child scopes.
//
//   2 => Watcher registration:  During template linking, directives register watches on the scope. These watches will be used to propagate 
//        model values to the DOM.
//
//   3 => Model mutation: For mutations to be properly observed, you should make them only within the scope.$apply(). AngularJS APIs do this
//        implicitly, so no extra $apply call is needed when doing   synchronous work in controllers, or asynchronous work with $http, $timeout 
//        or $interval services.
//
//   4 => Mutation observation: At the end of $apply, AngularJS performs a $digest cycle on the root scope, which then propagates throughout 
//        all child scopes. During the $digest cycle, all $watched expressions or functions are checked for model mutation and if a mutation 
//        is detected, the $watch listener is called.
//
//   5 => Scope destruction: When child scopes are no longer needed, it is the responsibility of the child scope creator to destroy them via 
//        scope.$destroy() API. This will stop propagation of $digest   calls into the child scope and allow for memory used by the child scope
//        models to be reclaimed by the garbage collector.
//
//  Here is the explanation of how the Hello world example achieves the data-binding effect when the user enters text into the text field.
//
//      During the compilation phase:
//       - the ng-model and input directive set up a keydown listener on the <input> control.
//       - the interpolation sets up a $watch to be notified of name changes.
//      
//      During the runtime phase:
//       - Pressing an 'X' key causes the browser to emit a keydown event on the input control.
//       - The input directive captures the change to the input's value and calls $apply("name = 'X';") to update the application 
//         model inside the AngularJS execution context.
//       - AngularJS applies the name = 'X'; to the model.
//       - The $digest loop begins
//       - The $watch list detects a change on the name property and notifies the interpolation, which in turn updates the DOM.
//       - AngularJS exits the execution context, which in turn exits the keydown event and with it the JavaScript execution context.
//       - The browser re-renders the view with the updated text.
//-------------------------------------------------------------------------------------------------------------------------------------|
