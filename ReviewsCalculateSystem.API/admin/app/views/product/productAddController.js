(function () {

    'use strict';

    var controllerId = 'productAddController';
    angular.module('app').controller(controllerId, productAddController);
    productAddController.$inject = ['$routeParams', 'productService', 'notificationService', '$location', '$scope'];

    function productAddController(routeParams, productService, notificationService, location, $scope) {

        /* jshint validthis:true */
        var vm = this;
        vm.AdminId = 3;
        vm.ProductId = 0;
        vm.totalMargin = 0;
        vm.product = {};
        vm.productForm = {};
        vm.currentReviewerInfo = [];
        vm.productList = [];
        vm.selectedReviewer = [];
        vm.addProduct = addProduct;
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
            productService.GetAllProductList(vm.pageSize, vm.pageNumber, vm.searchText).then(function (data) {
                vm.productList = data.Result;
                vm.total = data.Total
            },
                function (errorMessage) {
                    notificationService.displayError(errorMessage.message);
                });


        };

        //================================DateTime Picker Start========================================================

        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.dt = null;
        };

        $scope.inlineOptions = {
            //customClass: getDayClass,
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            //dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2050, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };

        // Disable weekend selection
        //function disabled(data) {
        //    var date = data.date,
        //        mode = data.mode;
        //    return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        //}

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        $scope.open2 = function () {
            $scope.popup2.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        //$scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = 'dd-MMMM-yyyy';
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popup1 = {
            opened: false
        };
        $scope.popup2 = {
            opened: false
        };


        //================================DateTime Picker End========================================================



        function addProduct() {
            productService.AddProduct(vm.product).then(function (data) {
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
