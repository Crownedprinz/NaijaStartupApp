/**
 * @file
 * Placeholder file for custom sub-theme behaviors.
 *
 */
(function ($, Drupal) {

  /**
   * Use this behavior as a template for custom Javascript.
   */
  Drupal.behaviors.exampleBehavior = {
    attach: function (context, settings) {
      $('.checklogin-btn').css('display', function () {
        if ($('a.login-popup')[0]) {

        } else {
          return 'none';
        }
      });
      $('.checklogin-btn').click(function () {
        $('a.login-popup').trigger('click');
      });
      $('.login-and-continue').click(function () {
        if ($('a.login-popup')[0]) {
          $('a.login-popup').trigger('click');
          $('input.edit-inc-redirect').val($(this).attr('data-href'));
        } else {
          $(location).attr('href', $(this).attr('data-href'));
        }

      });
      $('.faq-button').click(function () {
        changeURL('key', 'value');
      });
      function changeURL(key, value) {
        key = 'key';
        value = document.getElementById("searchFAQ").value;
        var kvp = document.location.search.substr(1).split('&');
        var i = kvp.length;
        var x;
        while (i--) {
          x = kvp[i].split('=');
          if (x[0] == key) {
            x[1] = value;
            kvp[i] = x.join('=');
            break;
          }
        }

        if (i < 0) {
          kvp[kvp.length] = [key, value].join('=');
        }
        document.location.search = kvp.join('&');
      }
      $('#searchFAQ').keyup(function (e) {
        if (e.keyCode == 13)
        {
          $('.input-group-button input').trigger("click");
        }
      });
      $('.forgot-password').click(function () {
        $('.user-forgot-pass').show();
      });
      $('li.tabs-title').click(function () {
        $(this).prevAll().addClass('pre-active');
        $(this).nextAll().removeClass('pre-active');

      });

//      function animatethis(targetElement, speed) {
//        var scrollWidth = $(targetElement).get(0).scrollWidth;
//        var clientWidth = $(targetElement).get(0).clientWidth;
//        $(targetElement).animate({scrollLeft: scrollWidth - clientWidth},
//                {
//                  duration: speed,
//                  complete: function () {
//                    targetElement.animate({scrollLeft: 0},
//                            {
//                              duration: speed,
//                              complete: function () {
//                                animatethis(targetElement, speed);
//                              }
//                            });
//                  }
//                });
//      }
//      ;
//      animatethis($('#q1'), 10000);

      $('.order-this-service').click(function () {

      });
      $('.inputfile-wrapper label').click(function () {
        $('.field_people_document_input input').trigger('click');
//        $('.field_people_document_input input').click();
      });
      $('#home-view-package-scrooldown-btn').click(function () {
        window.scrollTo(0, $(window).height());
      });

      $("a.open-file-in-new-window").click(function (e) {
        e.preventDefault();
        window.open($(this).attr('href'), "myWindowName", "width=500, height=600");
        return false;
      });
      $('img.payment-vertical-callback', context).once('myCustomBehavior').click(function (e) {
        e.preventDefault();
        $('.incorporate-charge button[type=submit]').trigger('click');
      });
      $('#printMeButton', context).once('myCustomBehavior').click(function(e){
        e.preventDefault();
        var backup_body = $('body').html();
        $('body').html($('#printMe').html());
        window.print();
        $('body').html(backup_body);
      });

      $('#mainMenu .call-center').click(function() {
        fbq('track', 'Lead');
      });
    }

  };

  $(window).on('load', function(){
    $("#menu-user-main-menu .top-bar-right > ul.menu").append($('.block-sgf-notification-alert li.position-relative').clone());

    $(".bell-button").click(function() {
      event.stopPropagation();
      $(".noti-panel").toggle("slow", function() {

      });
    });
    $(document).click(function() {
      $(".noti-panel").hide("slow");
    });
  });



})(jQuery, Drupal);
