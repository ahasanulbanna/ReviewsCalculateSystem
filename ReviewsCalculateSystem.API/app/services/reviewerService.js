(function () {
    'use strict';
    angular.module('app').service('reviewerService', ['dataConstants', 'apiHttpService', reviewerService]);

    function reviewerService(dataConstants, apiHttpService) {
        var service = {
            CreateReviewer: CreateReviewer,
            reviewerReviewAndProductInfoById: reviewerReviewAndProductInfoById,
            reviewerLogin: reviewerLogin
        };

        return service;

        function reviewerLogin(username, password) {
            var url = dataConstants.LOGIN;
            return apiHttpService.LOGIN(url, username, password);
        }

        function CreateReviewer(data) {
            var url = dataConstants.REVIEWER + 'CreateReviewer';
            return apiHttpService.POST(url,data);
        }

        function reviewerReviewAndProductInfoById(reviewerId, productId) {
            var url = dataConstants.REVIEWERTASKASIGN_URL + 'reviewerReviewForEachProductById?reviewerId=' + reviewerId + '&productId=' + productId;
            return apiHttpService.GET(url);
        }
    }
})();