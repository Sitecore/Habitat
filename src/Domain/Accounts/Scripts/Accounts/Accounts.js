function login(componentid) {
  var logincontrol = $("#" + componentid);
  var usernameField = logincontrol.find("#loginEmail");
  var passwordField = logincontrol.find("#loginPassword");
  $.ajax(
  {
    url: "/api/AccountsApi/Login",
    method: "POST",
    data: {
                email: usernameField.val(),
      password: passwordField.val()
    },
    success: function(data) {
      if (data.IsAuthenticated) {
        window.location.href = window.location.href;
      } else {
        var group = usernameField.parent();
        var isValidationPresent = group.find("#validationHelp");
        if (isValidationPresent) {
          isValidationPresent.remove();
        }
        var help = $("<span id='validationHelp' class='help-block'></span>");
        help.text(data.ValidationMessage);
        usernameField.parent().append(help);
        passwordField.val("");
      }
    }
  });
}