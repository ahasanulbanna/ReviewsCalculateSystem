
(function () {
    'use strict';

    var app = angular.module('app');
    app.constant('routes', getRoutes());
    app.config(['$routeProvider', 'routes', routeConfigurator]);

    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            setRoute(r.url, r.config);
        });

        $routeProvider.otherwise({ redirectTo: '/login' });
        function setRoute(url, definition) {
            definition.resolve = angular.extend(definition.resolve || {});
            $routeProvider.when(url, definition);
        }
    }

    function getRoutes() {

        return [
            {
                url: '/login',
                config: {
                    templateUrl: 'admin/app/views/authentication/views/login.html',
                    controller: 'loginController',
                    title: 'Login'
                }
            },
            {
                url: '/dashboard',
                config: {
                    templateUrl: 'admin/app/views/dashboard/dashboard.html',
                    controller: 'dashboardController',
                    controllerAs: 'vm',
                    title: 'Dashboard'
                }
            },         
            //-------------Reviewer Request---------
            {
                url: '/reviewer-request',
                config: {
                    templateUrl: 'admin/app/views/reviewerrequest/reviewerrequest.html',
                    controller: 'reviewerrequestController',
                    controllerAs: 'vm',
                    title: 'Reviewer Request'
                }
            },
            //-------------Product---------
            {
                url: '/products',
                config: {
                    templateUrl: 'admin/app/views/product/product.html',
                    controller: 'productController',
                    controllerAs: 'vm',
                    title: 'Product'
                }
            }, {
                url: '/product-add',
                config: {
                    templateUrl: 'admin/app/views/product/productAdd.html',
                    controller: 'productAddController',
                    controllerAs: 'vm',
                    title: 'Product'
                }
            },
            {
                url: '/product-update/:ProductId',
                config: {
                    templateUrl: 'admin/app/views/product/productAdd.html',
                    controller: 'productAddController',
                    controllerAs: 'vm',
                    title: 'Product'
                }
            },
            //-------------Task Asign---------
            {
                url: '/task-asign',
                config: {
                    templateUrl: 'admin/app/views/taskasign/currentproduct.html',
                    controller: 'currentproductController',
                    controllerAs: 'vm',
                    title: 'Task Asign'
                }
            },
            {
                url: '/task-asign/:ProductId',
                config: {
                    templateUrl: 'admin/app/views/taskasign/taskasign.html',
                    controller: 'taskasignController',
                    controllerAs: 'vm',
                    title: 'Task Asign'
                }
            },
            //-------------Review Check---------
            {
                url: '/product-review-check',
                config: {
                    templateUrl: 'admin/app/views/reviewcheck/reviewproduct.html',
                    controller: 'reviewproductController',
                    controllerAs: 'vm',
                    title: 'Review Check'
                }
            }, {
                url: '/review-check/Product/:ProductId',
                config: {
                    templateUrl: 'admin/app/views/reviewcheck/reviewcheck.html',
                    controller: 'reviewcheckController',
                    controllerAs: 'vm',
                    title: 'Review Check'
                }
            },
            //-------------Payment---------
            {
                url: '/payment',
                config: {
                    templateUrl: 'admin/app/views/payment/payment.html',
                    controller: 'paymentController',
                    controllerAs: 'vm',
                    title: 'Payment'

                }
            },
            {
                url: '/reviewer-payment-details/:ReviewerId',
                config: {
                    templateUrl: 'admin/app/views/payment/reviewerdetails.html',
                    controller: 'paymentController',
                    controllerAs: 'vm',
                    title: 'Payment'
                }
            }
        ];
    }


})();
