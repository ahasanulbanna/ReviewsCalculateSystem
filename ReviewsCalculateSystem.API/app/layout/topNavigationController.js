
(function () {
    "use strict";
    var controllerId = 'TopNavigationController';

    angular
        .module("app")
        .controller(controllerId, TopNavigationController);
    TopNavigationController.$inject = ['$rootScope'];
    function TopNavigationController($rootScope) {
        var vm = this;
        vm.loggedIn = $rootScope.globals.currentUser;
    }

}());
