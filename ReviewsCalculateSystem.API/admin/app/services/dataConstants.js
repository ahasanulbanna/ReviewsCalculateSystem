
(function () {
    "use strict";

    angular.module("app").constant("dataConstants", {
        LOGIN: baseAPIUrl + 'token',
        REVIEWER_URL: baseAPIUrl + 'api/reviewer/',
        PRODUCT_URL: baseAPIUrl + 'api/products/',
        REVIEWERTASKASIGN_URL: baseAPIUrl + 'api/ReviewerTaskAsign/',
        REVIEW_URL: baseAPIUrl + 'api/reviews/',
        PAYMENT_URL: baseAPIUrl + 'api/payment/',
        ADMIN_URL: baseAPIUrl+'api/admin/'



    });
})();