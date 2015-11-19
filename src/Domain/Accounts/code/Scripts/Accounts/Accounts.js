function login(componentid) {
  var logincontrol = $("#" + componentid);
  var usernameField = logincontrol.find("#loginEmail");
  var passwordField = logincontrol.find("#loginPassword");
  $.ajax(
  {
    url: "/api/Accounts/LoginDialog",
    method: "POST",
    data: {
                email: usernameField.val(),
      password: passwordField.val()
    },
    success: function (data) {
        if (data.RedirectUrl != null && data.RedirectUrl != undefined) {
            window.location.href = window.location.href;
        } else {
            var body = logincontrol.find(".modal-body");
            var parent = body.parent();
            body.remove();
            parent.html(data);
        }
    }
  });
}