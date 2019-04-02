'use strict';

angular.module('app')

    .controller('loginController',
        ['$scope', '$rootScope', '$location', 'AuthenticationService',
            function ($scope, $rootScope, $location, AuthenticationService) {
                // reset login status
                AuthenticationService.ClearCredentials();

                $scope.login = function () {
                    $scope.dataLoading = true;
                    AuthenticationService.login($scope.username, $scope.password).then(function (response) {
                        console.log(response);
                        AuthenticationService.SetCredentials($scope.username, $scope.password, response);
                        $location.path('/dashboard');
                    },
                        function (errorMessage) {
                            $scope.dataLoading = false;
                            $scope.error = errorMessage.error_description;
                        });

                };
            }]);