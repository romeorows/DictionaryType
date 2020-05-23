angular.module("umbraco").controller("dictionarytype.controller", function ($scope, $http) {

    $scope.cfg = {
        parentDictionaryKey: ''
    };

    //check if property type config is available
    if ($scope.model.config)
    { 
        $scope.cfg = angular.extend($scope.cfg, $scope.model.config);

        //get the list from the umbraco dictionary
        $http({ method: 'GET', url: '/umbraco/backoffice/api/DictionaryType/GetDictionaryList?parentKey=' + $scope.cfg.parentDictionaryKey })
            .then(function successCallback(response) {

                let dictionaryItems = response.data;

                if (dictionaryItems) {

                    //check the selected values
                    angular.forEach(dictionaryItems, function (data) {
                        if ($scope.model.value && data && $scope.model.value.indexOf(data.Value) != -1) {
                            data.Selected = true;
                        }
                    });

                    $scope.list = dictionaryItems;
                }
               
            }, function errorCallback(response) {
                console.log(response);
                $scope.error = "dictionarytype.controller Error: An error has occured while getting the list.";
            });
    }

    $scope.checkBoxUpdate = function () {
        $scope.itemSelected = [];
        angular.forEach($scope.list, function (listItem) {
            if (!!listItem.Selected) $scope.itemSelected.push(listItem.Value);
        })
        $scope.model.value = $scope.itemSelected;
    }
});