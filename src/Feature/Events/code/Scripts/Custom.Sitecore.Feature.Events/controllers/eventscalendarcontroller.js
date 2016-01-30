var app = angular.module('sitecore.feature.events.app');
app.controller('EventsCalendarController', function (moment, alert) {

    var vm = this;

    vm.events = [
      {
          title: 'angular js hackathon',
          type: 'info',
          startsAt: moment().startOf('month').toDate()
      },
      {
          title: 'plural sight hackathon',
          type: 'info',
          startsAt: moment().startOf('month').toDate()
      }
    ];

    vm.calendarView = 'month';
    vm.viewDate = moment().startOf('month').toDate();
    vm.isCellOpen = true;

    vm.eventClicked = function (event) {
        alert.show('Clicked', event);
    };

});