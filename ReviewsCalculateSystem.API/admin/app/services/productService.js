(function () {
    'use strict';
    angular.module('app').service('productService', ['dataConstants', 'apiHttpService', productService]);

    function productService(dataConstants, apiHttpService) {
        var service = {
            GetAllProductList: GetAllProductList,
            reviewerDetailsInfoList: reviewerDetailsInfoList,
            GetProductById: GetProductById,
            AddProduct: AddProduct,
            ProductUpdate: ProductUpdate,

            deleteStudent: deleteStudent
        };

        return service;
        function GetAllProductList(pageSize, pageNumber, searchText) {
            var url = dataConstants.PRODUCT_URL + 'GetAllProductList?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchText=" + searchText;
            return apiHttpService.GET(url);
        }

        function reviewerDetailsInfoList(pageSize, pageNumber, searchText) {
            var url = dataConstants.REVIEWERTASKASIGN_URL + 'reviewerDetailsInfoList?pageSize=' + pageSize + "&pageNumber=" + pageNumber + "&searchText=" + searchText;
            return apiHttpService.GET(url);
        }

        function GetProductById(ProductId) {
            var url = dataConstants.PRODUCT_URL + 'GetProductById/' + ProductId;
            return apiHttpService.GET(url);
        }

        function AddProduct(data) {
            var url = dataConstants.PRODUCT_URL + 'AddProduct';
            return apiHttpService.POST(url, data);
        }

        function ProductUpdate(data) {
            var url = dataConstants.PRODUCT_URL + 'product-update/' + data.ProductId;
            return apiHttpService.PUT(url, data);
        }

        function deleteStudent(studentId) {
            var url = dataConstants.STUDENT_URL + 'delete-student/' + studentId;
            return apiHttpService.DELETE(url);
        }


    }
})();