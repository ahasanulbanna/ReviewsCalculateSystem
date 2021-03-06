﻿(function () {

    'use strict';

    var controllerId = 'reviewerasigntaskController';
    angular.module('app').controller(controllerId, reviewerasigntaskController);
    reviewerasigntaskController.$inject = ['$routeParams', 'reviewerasigntaskService', 'notificationService', '$location', '$rootScope'];

    function reviewerasigntaskController($routeParams, reviewerasigntaskService, notificationService, location, $rootScope) {

        /* jshint validthis:true */
        var vm = this;
        vm.loggedIn = {};
        vm.TaskList = [];
        vm.courses = [];
        vm.AddReview = AddReview;
        vm.UpdateReview = UpdateReview;
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

        init();
        function init() {
            vm.loggedIn = $rootScope.globals.currentUser;
            reviewerasigntaskService.getCurrentAsingTaskById
                (vm.loggedIn.ReviewerId).then(function (data) {
                    vm.TaskList = data;
                },
                    function (errorMessage) {
                        notificationService.displayError(errorMessage.message);
                    });
        }

        function AddReview(productId) {
            vm.reviewerId = vm.loggedIn.ReviewerId;
            var url = location.url('/product-review-add/' + vm.reviewerId + '/' + productId);
            location.path(url.$$url);
        }
        function UpdateReview(productId) {
            vm.reviewerId = vm.loggedIn.ReviewerId;
            var url = location.url('/review-update/' + vm.reviewerId + '/' + productId);
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
