$(function () {
    
    $("#addxht").on("click", function () { selectbtnxall(); });
    // user options

    $("#moduleID").change(function () {
        var URLext = $('#moduleID').val();
        var items = get_items("M", URLext);
        $("#menuID").html(items);
        $(".transID").html("");
    });

    $("#menuID").change(function () {
        var URLext = $('#menuID').val();
        var items = get_items("T", URLext);
        $(".transID").html(items);
    });

    $("#selbtn").click(function (e) {
        e.preventDefault();
        $("#img_select").val("S");
        var snumb1 = $(".transID").val();
        var snumb2 = $(".basket1").val();
        var snumb3 = snumb1 + "[]]" + snumb2;
        var items = get_items("U", snumb3);
        $(".basket1").html(items);
        $(".transID").html("");
    })

    //transfer definition    
    $("#screen_name").change(function () {
        var URL = $('#loader1').data('request-daily');
        URL = $.trim(URL);
        var URLext = $('#screen_name').val();
        URLext = $.trim(URLext);
        URL = URL + '04' + URLext;
        $.ajax({
            type: "Post",
            url: URL,
            error: function (xhr, status, error) {
                alert("Error: " + xhr.status + " - " + error + " - " + URL);
            },
            success: function (data) {
                var items = "";
                $.each(data, function (i, state) {
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                    // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
                });
                $('#list1').html(items);
            }
        });

    });

    $("#sidearrow").click(function () {
        $("#img_select").val("S");
    })

    $("#uparrow").click(function () {
        $("#img_select").val("U");
    })

    $("#downarrow").click(function () {
        $("#img_select").val("D");
    })

    $("#addxht1").click(function () {
        $("#img_select").val("X");
    })

    $("#deselect").click(function () {
        $("#list1 > option").prop("selected", false);
        $("#list2 > option").prop("selected", false);
    })

    $("#sidearrow1").click(function () {
        $("#img_select").val("T");
    })

    $("#deselect1").click(function () {
        $("#list3 > option").prop("selected", false);
        $("#list4 > option").prop("selected", false);
    })

    $("#btnx-right").click(function () {
        $("#list1 > option:selected").each(function () {
            $(this).remove().appendTo("#list2");
        });
    });

    $("#btnx-left").click(function () {
        $("#list2 > option:selected").each(function () {
            $(this).remove().appendTo("#list1");
        });
    });

    $("#btnx-right1").click(function () {
        $("#list3 > option:selected").each(function () {
            $(this).remove().appendTo("#list4");
        });
    });

    $("#btnx-left1").click(function () {
        $("#list4 > option:selected").each(function () {
            $(this).remove().appendTo("#list3");
        });
    });

    $("#btnx-up").click(function () {
        var $op = $('#list2 option:selected');
        if ($op.length) {
            $op.first().prev().before($op);
        };
    });

    $("#btnx-down").click(function () {
        var $op = $('#list2 option:selected');
        if ($op.length) {
            $op.last().next().after($op);
        };
    });


});

    function get_items(optid, valueid) {
        var items;
        var URL = $('#loader1').data('request-daily');
        URL = $.trim(URL);
        URL = URL + optid + "[]" + valueid;
        $.ajax({
            type: "Post",
            url: URL,
            async: false,
            error: function (xhr, status, error) {
                alert("Error: " + xhr.status + " - " + error + " - " + URL);
            },
            success: function (data) {
                items = '';
                $.each(data, function (i, state) {
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                });
            }
        });
        return items;
    };

    function optselect() {
        var items;
        var URL = '@Url.Action("populate_query") ';
        URL = $.trim(URL);
        //URL = URL + optid + "[]" + valueid;
        $.ajax({
            type: "Post",
            url: URL,
            async: false,
            error: function (xhr, status, error) {
                alert("Error: " + xhr.status + " - " + error + " - " + URL);
            },
            success: function (data) {
                items = '<option></option>';
                $.each(data, function (i, state) {
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                });
            }
        });
        $('.optselect').html(items);
    };


    function selectbtnxall() {
        $("#list2 > option").prop("selected", true);
        $("#list4 > option").prop("selected", true);
    }


