//Filters format the value of an expression for display to the user. They can be used in view templates, controllers or services.
var fnUpperFltr = function () {

    return function (input) {
        var output = '';

        output = input.toUpperCase();

        return output;
    }
};