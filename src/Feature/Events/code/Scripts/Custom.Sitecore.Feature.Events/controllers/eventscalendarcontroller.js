var app = angular.module('sitecore.feature.events.app');
app.controller('EventsCalendarController', ['moment', 'alert', '$http', function (moment, alert, $http) {

    var vm = this;


    $http.get('api/EventsApi/GetEventsListJson')
      .then(
      function (result) {
          var events = result.data;
          for (var i = 0; i < result.data.length; i++) {
              events[i].startsAt = new Date(events[i].startsAtTxt);
          }
          
          vm.events = events;
      });

    //vm.events = [
    //  {
    //      title: 'angular js hackathon',
    //      description:"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lo",
    //      type: 'info',
    //      startsAt: moment().startOf('month').toDate()
    //  },
    //  {
    //      title: 'plural sight hackathon',
    //      description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printe",
    //      type: 'info',
    //      startsAt: moment().startOf('month').toDate()
    //  }
    //];

    vm.calendarView = 'month';
    vm.viewDate = moment().startOf('month').toDate();
    vm.isCellOpen = true;

    vm.eventClicked = function (event) {
        alert.show('Clicked', event);
    };

}]);