var app = angular.module('sitecore.feature.events.app');
app.factory('alert', function ($uibModal) {

      function show(action, event) {
          return $uibModal.open({
              templateUrl: '/Scripts/Custom.Sitecore.Feature.Events/views/modalContent.html',
              controller: function () {
                  var vm = this;
                  vm.action = action;
                  vm.event = event;
              },
              controllerAs: 'vm'
          });
      }

      return {
          show: show
      };

  });
