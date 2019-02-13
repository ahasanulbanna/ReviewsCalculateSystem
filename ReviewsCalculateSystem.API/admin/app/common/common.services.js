(function () {
    "use strict";
    window.baseAPIUrl = 'http://localhost:49786/';//API URL


    var commonServie = angular.module("common.services", []);

    commonServie.constant("appSettings",
        {
            scerpAPI: window.baseAPIUrl,
          
        });


}());
