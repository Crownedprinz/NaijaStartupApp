$(document).ready(function () {


    $(".lgtime").click(function () {
        var isUsername = $("#id_username").val();
        var isPassword = $("#id_password").val();
        if (isPassword !== '' && isUsername !== '')
        lgshow();
    });
    function lgshow1() {
        // spinner for forms after validation is done and okay
        var result2 = $(".valshow").closest('form');
        var result = result2.valid();
        if (result) {
            $("#loading").fadeIn();
            var opts = {
                lines: 12, // The number of lines to draw
                length: 12, // The length of each line
                width: 4, // The line thickness
                radius: 10, // The radius of the inner circle
                color: '#000', // #rgb or #rrggbb
                speed: 1, // Rounds per second
                trail: 60, // Afterglow percentage
                shadow: true, // Whether to render a shadow
                hwaccel: false // Whether to use hardware acceleration
            };
            var target = document.getElementById('loading');
            var spinner = new Spinner(opts).spin(target);
            $(target).data('spinner', spinner);
        }
    }
    //spin js
    function lgshow() {
        $("#spinner").fadeIn();
        let opts = {
            lines: 13,
            length: 28,
            width: 15,
            radius: 42,
            scale: 0.4,
            corners: 1,
            color: '#000',
            opacity: 0.25,
            rotate: 0,
            direction: 1,
            speed: 1,
            trail: 60,
            fps: 20,
            zIndex: 2e9,
            className: 'spinner',
            top: '50%',
            left: '50%',
            shadow: true,
            hwaccel: false,
            position: 'absolute',
        },

            target = document.getElementById('spinner'),
            spinner = new Spinner(opts).spin(target);
    }
    function lgstop() {
        $('#spinner').data('spinner').stop();
        $("#spinner").fadeOut();

    }
    $(document).ajaxStart(function () {
        lgshow();
    });
    $(document).ajaxStop(function () {
        $('#spinner').stop();
        $("#spinner").fadeOut();
    });
});