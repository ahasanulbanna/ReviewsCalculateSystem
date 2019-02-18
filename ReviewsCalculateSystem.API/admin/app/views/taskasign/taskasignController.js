(function () {

    'use strict';

    var controllerId = 'taskasignController';
    angular.module('app').controller(controllerId, taskasignController);
    taskasignController.$inject = ['$routeParams', 'taskasignService', 'notificationService', '$location','$scope'];

    function taskasignController(routeParams, taskasignService, notificationService, location, $scope) {

        /* jshint validthis:true */
        var vm = this;
        vm.AdminId = 3;
        vm.ProductId = 0;
        vm.productDetails = {};
        vm.currentReviewerInfo = [];
        vm.reviewerList = [];
        vm.selectedReviewer = [];
        vm.reviewerSelect = reviewerSelect;
        vm.taskAsign = taskAsign;
        vm.updateInvoice = updateInvoice;
        vm.deleteInvoice = deleteInvoice;
        vm.invoiceView = invoiceView;
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 5;
        vm.onSearch = onSearch;
        vm.close = close;
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


        if (routeParams.ProductId !== undefined && routeParams.ProductId !== '') {
            vm.ProductId = routeParams.ProductId;
        }

        init();
        function init() {
            taskasignService.reviewerDetailsInfoList(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.reviewerList = data.Result;
                vm.total = data.Total
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


            taskasignService.GetProductById(vm.ProductId).then(function (data) {
                vm.productDetails = data.ProductInfo;
                vm.currentReviewerInfo = data.asigningTaskInfo;
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });
        };

        vm.selectedReviewer = [];
        // selected on a given reviewer by name
        function reviewerSelect(reviewer) {
            reviewer.AdminId = vm.AdminId;
            reviewer.ProductId = vm.ProductId
            var idx = vm.selectedReviewer.indexOf(reviewer);
            // is currently selected
            if (idx > -1) {
                vm.selectedReviewer.splice(idx, 1);
            }
            // is newly selected
            else {
                //var tempObj = JSON.parse(JSON.stringify(reviewer));
                delete reviewer.TotalReviewMargin;
                delete reviewer.WorkingBookCount;
                delete reviewer.TotalReviewCollect;               
                this.selectedReviewer.push(reviewer);
                //vm.selectedReviewer.push(tempObj);
            }
         }













        function taskAsign() {
            taskasignService.taskAsign(vm.selectedReviewer).then(function (data) {
                close();
            });
        }

        function updateInvoice(invoice) {
            var url = location.url('/invoice-modify/' + invoice.invoiceId);
            location.path(url.$$url);
        }
        function invoiceView(invoice) {
            var url = location.url('/invoice-view/' + invoice.invoiceId);
            location.path(url.$$url);
        }
        function deleteInvoice(invoice) {
            invoiceService.taskAsign(invoice.invoiceId).then(function (data) {
                init();
            });
        }

        function close() {
            var url = "/task-asign";
            location.path(url);
        }

        function pageChanged() {
            var url = location.url('/task-asign/' + vm.ProductId);
            location.path(url.$$url).search('pn', vm.pageNumber).search('ps', vm.pageSize).search('q', vm.searchText);
        }

        function onSearch() {
            vm.pageNumber = 1;
            pageChanged();
        }
    }

})();
