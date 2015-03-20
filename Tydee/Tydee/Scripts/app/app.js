

var Tydee = new TydeeViewModel();

function TydeeViewModel() {
    var self = this;


}

/**
 * Safely return a deep innested property avoiding null exceptions
*/
ko.observable.fn.getSafe = function (/*...*/) {
    var actual = this();
    for (var i = 0; i < arguments.length; i++) {

        //run until a value is null or undefined
        if (actual && actual[arguments[i]]) {
            actual = actual[arguments[i]];
            if (_.isFunction(actual)) actual = actual();
        } else {
            return undefined;
        }
    }

    return actual;
};

ko.applyBindings(Tydee);