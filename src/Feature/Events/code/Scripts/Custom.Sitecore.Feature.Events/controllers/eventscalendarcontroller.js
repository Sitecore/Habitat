var app = angular.module('sitecore.feature.events.app');
app.controller('EventsCalendarController', ['moment', 'alert', '$http', function (moment, alert, $http) {


    var vm = this;
    vm.eventid = '';
    vm.events = [];
    vm.init = function (eventid) {
        vm.eventid = eventid;
        $http.get('/api/EventsApi/GetCalendarEventsJson?id=' + vm.eventid)
        .then(
        function (result) {
            if (result != null && result.data != "") {
                var events = result.data;
                for (var i = 0; i < result.data.length; i++) {
                    if (events[i].startsAtTxt != "") {
                        events[i].startsAt = new Date(events[i].startsAtTxt);
                    }
                    if (events[i].endsAtTxt != "") {
                        events[i].endsAt = new Date(events[i].endsAtTxt);
                    }
                   
                }

                vm.events = events;
            }

        });
    }

    vm.calendarView = 'month';
    vm.viewDate = moment().startOf('month').toDate();
    vm.isCellOpen = true;

    vm.eventClicked = function (event) {
        alert.show('Clicked', event);
    };

}]);

app.filter('unsafe', function($sce) { return $sce.trustAsHtml; });
