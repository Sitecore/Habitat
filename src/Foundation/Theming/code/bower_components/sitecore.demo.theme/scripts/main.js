if(!window.console){
  console={};
  console.log = function(){};
}

(function($) {
  $(function() {
    $('.code').each(function(){
      var encoded = document.createTextNode($.trim($(this)[0].innerHTML));
      $(this).html(encoded);
      var editor = ace.edit($(this).attr('id'));
      editor.setFontSize(13);
      editor.getSession().setMode("ace/mode/html");
      //$(this).html(encoded);
    });
  });
})(jQuery);
