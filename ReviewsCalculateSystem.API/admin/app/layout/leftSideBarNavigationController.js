(function () {
    'use strict';
    var controllerId = 'LeftSideBarNavigationController';
    angular
        .module('app')
        .controller(controllerId, ['$scope', 'notificationService', '$sce', '$state', '$location', leftSideBarNavigation]);

    function leftSideBarNavigation($scope, notificationService, $sce, $state, location) {
        var vm = this;
        vm.model = [];
        vm.reviewRequest = reviewRequest;
        vm.isCurrent = isCurrent;
        vm.isChild = isChild;
        vm.menuChange = menuChange;
        vm.upArrow = true;

        init();
        function init() {

        }


        var a = 'something';

        function reviewRequest() {
            var url = "/reviewer-request";
            location.path(url);
        }
        function isCurrent(route) {
            if (!route.title || !$state.current || !$state.current.title) {
                return '';
            }

            var menu = route.title;
            return $state.current.title.substr(0, menu.length) === menu ? 'active' : '';
        }

        function isChild(route) {
            if (route.isChild) {
                return 'child';
            }

            return '';
        }




        function menuChange() {
            $('.user').parent().parent().parent().toggleClass("collaspe");
            $('.navbar-header').toggleClass("collL");
            $('.col-md-offset-2').toggleClass("bodyLeft");

        }

        // Sidebar height
        var screen_height = window.innerHeight;
        var height = screen_height - 74;
        var kk = $("#sidebar-wrapper").height(height);

    }

})();