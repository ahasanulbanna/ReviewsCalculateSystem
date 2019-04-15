'use strict';

angular.module('app')

    .controller('LoginController',
        ['$scope', '$rootScope', '$location', 'reviewerService', 'AuthenticationService',
            function ($scope, $rootScope, $location, reviewerService, AuthenticationService) {
                // reset login status
                AuthenticationService.ClearCredentials();

                $scope.login = function () {
                    $scope.dataLoading = true;
                    reviewerService.reviewerLogin($scope.username, $scope.password).then(function (response) {
                        console.log(response);
                        if (response.Role === "User") {
                            AuthenticationService.SetCredentials($scope.username, $scope.password, response);
                            $location.path('/dashboard');
                        } else {
                            $scope.dataLoading = false;
                            $scope.error = "Incorrect username && password";
                        }
                    },
                        function (errorMessage) {
                            $scope.dataLoading = false;
                            $scope.error = errorMessage.error_description;
                        });

                };
            }]);