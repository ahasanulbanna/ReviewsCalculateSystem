
(function () {
    'use strict';

    var app = angular.module('app');
    app.constant('routes', getRoutes());
    app.config(['$routeProvider', '$locationProvider', 'routes', routeConfigurator]);

    function routeConfigurator($routeProvider, $locationProvider, routes) {

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
            //-------------Authentication-------
            {
                url: '/login',
                config: {
                    templateUrl: 'app/views/authentication/views/login.html',
                    controller: 'LoginController',
                    title: 'Login'
                }
            },
            {
                url: '/dashboard',
                config: {
                    templateUrl: 'app/views/dashboard/dashboard.html',
                    controller: 'dashboardController',
                    controllerAs: 'vm',
                    title: 'Dashboard'
                }
            },
           //-------------Review---------          
            {
                url: '/review',
                config: {
                    templateUrl: 'app/views/review/review.html',
                    controller: 'reviewController',
                    controllerAs: 'vm',
                    title: 'Review'
                }
            }, {
                url: '/productreview',
                config: {
                    templateUrl: 'app/views/review/productreview.html',
                    controller: 'reviewController',
                    controllerAs: 'vm',
                    title: 'Review'
                }
            
            },
            {
                url: '/product-review-add/:reviewerId/:productId',
                config: {
                    templateUrl: 'app/views/reviewerAsignTask/productreviewadd.html',
                    controller: 'productreviewaddController',
                    controllerAs: 'vm',
                    title: 'Review'
                }
            },
            {
                url: '/review-update/:reviewerId/:productId',
                config: {
                    templateUrl: 'app/views/reviewerAsignTask/reviewupdate.html',
                    controller: 'reviewupdateController',
                    controllerAs:'vm',
                    title: 'Review Update'
                }

            },
            //-------------Product---------
            {
                url: '/products',
                config: {
                    templateUrl: 'app/views/review/product.html',
                    //controller: 'reviewController',
                    //controllerAs: 'vm',
                    title: 'Review'
                }
            }, {
                url: '/add-product',
                config: {
                    templateUrl: 'app/views/review/productAdd.html',
                    //controller: 'reviewController',
                    //controllerAs: 'vm',
                    title: 'Review'
                }
            },
            //-------------Review Asign Task---------
            {
                url: '/reviewer-asign-task',
                config: {
                    templateUrl: 'app/views/reviewerAsignTask/reviewerasigntask.html',
                    controller: 'reviewerasigntaskController',
                    controllerAs: 'vm',
                    title: 'Reviewer Asign Task'
                }
            },
            //-------------Reviewer---------
            {
                url: '/reviewer-registration',
                config: {
                    templateUrl: 'app/views/reviewer/reviewer-registration-form.html',
                    controller: 'reviewerregistrationController',
                    controllerAs: 'vm',
                    title: 'Reviewer'
                }
            },
             //-------------Payment Details---------
            {
                url: '/payment-details',
                config: {
                    templateUrl: 'app/views/payment/payment.html',
                    controller: 'paymentController',
                    controllerAs: 'vm',
                    title: 'Payment'
                }
            }
        ];
    }

    
})();
