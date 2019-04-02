(function () {

    'use strict';

    var controllerId = 'reviewerregistrationController';
    angular.module('app').controller(controllerId, reviewerregistrationController);
    reviewerregistrationController.$inject = ['$routeParams', 'reviewerService', 'notificationService', '$location'];

    function reviewerregistrationController($routeParams, reviewerService, notificationService, location) {

        /* jshint validthis:true */
        var vm = this;
        vm.reviewer = {};
        vm.ReviewerRegistrationForm = {};
        vm.save = save;
        vm.addReview = addReview;
        vm.addProductReview = addProductReview;
        vm.updateInvoice = updateInvoice;
        vm.invoiceView = invoiceView;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 10;
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

        }
        function save() {

            reviewerService.CreateReviewer(vm.reviewer).then(function (data) {
                location.path("/login");
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        } 

        function addReview() {
            var url = "/productreview";
            location.path(url);
        }

        function addProductReview() {
            var url = "/productreviewadd";
            location.path(url);
        }

        function updateInvoice(invoice) {
            var url = location.url('/invoice-modify/' + invoice.invoiceId);
            location.path(url.$$url);
        }
        function invoiceView(invoice) {
            var url = location.url('/invoice-view/' + invoice.invoiceId);
            location.path(url.$$url);
        }
        //function deleteDepartment(department) {
        //    departmentService.deleteDepartment(department.departmentId).then(function (data) {
        //        init();
        //    });
        //}

        function pageChanged() {
            var url = location.url('/invoices');
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
