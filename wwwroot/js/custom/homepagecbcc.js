/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 * created by thi
 * at 08/12/2018 10:36AM
 */

/**
 * @file
 * Placeholder file for custom sub-theme behaviors.
 *
 */
(function ($, Drupal) {

  /**
   * Use this behavior as a template for custom Javascript.
   */
  Drupal.behaviors.homepage = {
    attach: function (context, settings) {
      checkscroll();
      function checkscroll() {
        var countscrool = $(window).scrollTop();
        if (countscrool > 50) {
          jQuery('#backToTop').attr('style', 'display:block;');
          jQuery('#mainMenu').attr('style', 'background-color:black;');
          jQuery('#mainMenu .submenu.is-dropdown-submenu').css('background-color', 'black');

        } else {
          jQuery('#backToTop').attr('style', 'display:none;');
          jQuery('#mainMenu').attr('style', 'background-color:transparent;');
          jQuery('#mainMenu .submenu.is-dropdown-submenu').css('background-color', 'transparent');
        }
      }
      $(window).on('scroll', checkscroll);
      if (window.location.hash == '#signin')
        $('a.login-popup').trigger('click');


      $('input#mc-embedded-subscribe').click(function() {
        fbq('track', 'Lead');
      });

    }
  };

})(jQuery, Drupal);
