var app = angular.module('sitecore.feature.events.app');
app.controller('EventsCalendarController', ['moment', 'alert', '$http', function (moment, alert, $http) {

    
    var vm = this;
    vm.eventid = '';
    vm.init = function (eventid) {
        vm.eventid = eventid;
    }
   
    $http.get('api/EventsApi/GetEventsListJson?id=' + vm.eventid)
      .then(
      function (result) {
          var events = result.data;
          for (var i = 0; i < result.data.length; i++) {
              events[i].startsAt = new Date(events[i].startsAtTxt);
              events[i].endsAt = new Date(events[i].endsAtTxt);
              
          }
          
          vm.events = events;
      });

  
    vm.calendarView = 'month';
    vm.viewDate = moment().startOf('month').toDate();
    vm.isCellOpen = true;

    vm.eventClicked = function (event) {
        alert.show('Clicked', event);
    };

}]);