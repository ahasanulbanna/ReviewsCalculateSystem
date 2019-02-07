(function () {

    'use strict';

    var controllerId = 'productreviewaddController';
    angular.module('app').controller(controllerId, productreviewaddController);
    productreviewaddController.$inject = ['$routeParams', 'reviewerasigntaskService', 'notificationService', '$location'];

    function productreviewaddController(routeParams, reviewerasigntaskService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.reviewerId = 1;
        vm.productId = 0;
        vm.productInfo = [];
        vm.courses = [];
        vm.AddReview = AddReview;
        vm.updateCourse = updateCourse;
        vm.deleteCourse = deleteCourse;
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
            reviewerasigntaskService.reviewerReviewAndProductInfoById(vm.reviewerId, vm.productId).then(function (data) {
                vm.productInfo = data;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        }

        function AddReview(Id) {
            var url = location.url('/product-review-add/' + Id);
            location.path(url.$$url);
        }
        function updateCourse(course) {
            var url = location.url('/course-modify/' + course.courseId);
            location.path(url.$$url);
        }

        function deleteCourse(course) {
            courseService.deleteCourse(course.courseId).then(function (data) {
                init();
            });
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
