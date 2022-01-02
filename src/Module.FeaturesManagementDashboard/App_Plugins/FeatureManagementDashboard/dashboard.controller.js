(function () {
    "use strict";

    function controller($http) {
        var vm = this;
        vm.features = [];
        vm.changeFeatureStatus = changeFeatureStatus;
        vm.getFeatures = getFeatures;
        vm.featureStatusChange = featureStatusChange;

        function featureStatusChange(feature) {
            console.log("feature change click", feature);
            feature.Status = !feature.Status;
            vm.changeFeatureStatus(feature.Id, feature.Status);
        }

        function getFeatures() {
            $http.get('/umbraco/backoffice/api/FeaturesManagementDashboard/features').then(function (res) {
                vm.features = res.data;
            });
        }

        function changeFeatureStatus(featureId, featureStatus) {
            $http.put(`/umbraco/backoffice/api/FeaturesManagementDashboard/features/${featureId}/status`, { status: featureStatus }).then(function (res) {
            });
        }

        vm.getFeatures();

        return vm;
    }

    angular.module("umbraco").controller("Our.Umbraco.FeatureManagementDashboard.Controller", ['$http', controller]);
})();