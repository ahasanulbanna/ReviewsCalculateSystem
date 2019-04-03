(function () {

    'use strict';

    var controllerId = 'reviewupdateController';
    angular.module('app').controller(controllerId, reviewupdateController);
    reviewupdateController.$inject = ['$routeParams', 'reviewerasigntaskService', 'notificationService', '$location', '$rootScope'];

    function reviewupdateController(routeParams, reviewerasigntaskService, notificationService, location, $rootScope) {

        /* jshint validthis:true */
        var vm = this;
        vm.productId = 0;
        vm.loggedIn = {};
        vm.reviews = [];
        vm.review = {};
        vm.UpdateReview = UpdateReview;
        vm.updateCourse = updateCourse;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 3;
        vm.onSearch = onSearch;
        vm.pageNumber = 1;
        vm.total = 0;

        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }

        if (routeParams.productId !== undefined && routeParams.productId !== '') {
            vm.productId = routeParams.productId;
        }
        init();
        function init() {
            vm.loggedIn = $rootScope.globals.currentUser;
            reviewerasigntaskService.GetReviewByProductIdAndReviewerId
                (vm.loggedIn.ReviewerId, vm.productId).then(function (data) {
                    vm.reviews = data;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
        }


        function UpdateReview(review) {
            reviewerasigntaskService.updateReview
                (review.ReviewId, review).then(function (data) {
                    notificationService.displaySuccess("Review update " + data);
                    init();
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });


        }
        function updateCourse(course) {
            var url = location.url('/course-modify/' + course.courseId);
            location.path(url.$$url);
        }

        function pageChanged() {
            var url = location.url('/courses');
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }

    }

})();
