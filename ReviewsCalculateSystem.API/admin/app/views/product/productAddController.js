(function () {

    'use strict';

    var controllerId = 'productAddController';
    angular.module('app').controller(controllerId, productAddController);
    productAddController.$inject = ['$routeParams', 'productService', 'notificationService', '$location', '$scope','$rootScope'];

    function productAddController(routeParams, productService, notificationService, location, $scope, $rootScope) {
        /***************************************************************************************************************
        *********************************************** Declaration ****************************************************
        ****************************************************************************************************************/
        var vm = this;
        vm.ProductId = 0;
        vm.loggedIn = {};
        vm.product = {};
        vm.productForm = {};
        vm.save = save;
        vm.addProduct = addProduct;
        vm.buttonText = "Save";
        vm.pageChanged = pageChanged;
        vm.searchText = "";
        vm.pageSize = 5;
        vm.onSearch = onSearch;
        vm.close = close;
        vm.pageNumber = 1;
        vm.total = 0;
        /************************* End *****************  Declaration  ****************** End ***********************/
       

      
        if (location.search().ps !== undefined && location.search().ps !== null && location.search().ps !== '') {
            vm.pageSize = location.search().ps;
        }

        if (location.search().pn !== undefined && location.search().pn !== null && location.search().pn !== '') {
            vm.pageNumber = location.search().pn;
        }
        if (location.search().q !== undefined && location.search().q !== null && location.search().q !== '') {
            vm.searchText = location.search().q;
        }

        /***************************************************************************************************************
        *********************************************** Fetch RouteParam Value *****************************************
        ****************************************************************************************************************/
        if (routeParams.ProductId !== undefined && routeParams.ProductId !== '') {
            vm.ProductId = routeParams.ProductId;
            vm.buttonText = "Update";
        }
        /************************* End ******************   Fetch RouteParam Value  ******* End **********************/

        /***************************************************************************************************************
        *********************************************** Primary Data Load **********************************************
        ****************************************************************************************************************/
        init();
        function init() {
            vm.loggedIn = $rootScope.admin.currentUser;

            productService.GetProductById(vm.ProductId).then(function (data) {
                vm.product = data.productInfo;
                vm.product.ReviewStartDate = new Date(data.productInfo.ReviewStartDate);
                vm.product.ReviewEndDate = new Date(data.productInfo.ReviewEndDate);
            });
           
        }
        /************************* End ******************   Primary Data Load  ************ End **********************/

        

        /***************************************************************************************************************
        *********************************************** DateTime Picker ************************************************
        ****************************************************************************************************************/

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
        function disabled(data) {
            var date = data.date,
                mode = data.mode;
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

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


        /************************* End ******************   DateTime Picker  ************** End **********************/


        /***************************************************************************************************************
        *********************************************** OnClick Function ***********************************************
        ****************************************************************************************************************/
        function save() {
            if (vm.ProductId !== 0 && vm.ProductId !== '') {
                ProductUpdate();
            } else {
                addProduct();
            }
        }

        function addProduct() {
            vm.product.AdminId = vm.loggedIn.AdminId;
            productService.AddProduct(vm.product).then(function (data) {
                close();
            });
        }

        function ProductUpdate() {
            productService.ProductUpdate(vm.product).then(function (data) {
                close();
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

       /************************* End ******************   OnClick Function  ************* End **********************/
    }

})();
