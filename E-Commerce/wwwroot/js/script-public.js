$(function () {
    //check if already full screen toggle classes accordingly
    if (!window.screenTop && !window.screenY) {
        $("#btnFullscreen").removeClass("fs-enabled").addClass("fs-disabled");
    } else {
        $("#btnFullscreen").removeClass("fs-disabled").addClass("fs-enabled");
    }

    //toggle fullscreen
    $("#btnFullscreen").click(function () {
        var el = document.documentElement;
        if ($(this).hasClass("fs-disabled")) {
            if (el.requestFullscreen) {
                el.requestFullscreen();
            }
            $("#btnFullscreen")
                .removeClass("fs-disabled")
                .addClass("fs-enabled");
        } else {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            }
            $("#btnFullscreen")
                .removeClass("fs-enabled")
                .addClass("fs-disabled");
        }
    });

    $(".button-collapse").sideNav({
        edge: "left", // Choose the horizontal origin
        breakpoint: 1700, // Breakpoint for button collapse
        menuWidth: 240, // Width for sidenav
        slim: true,
    });
    var container = document.querySelector(".custom-scrollbar");
    var ps = new PerfectScrollbar(container, {
        wheelSpeed: 2,
        wheelPropagation: true,
        minScrollbarLength: 20,
    });

    new WOW().init();

    $(".formLoader").submit(function () {
        $(".formLoader").find('[type="submit"]').prop("disabled", true);
        $("#myModal").attr("data-backdrop", "static");
        $("#myModal").attr("data-keyboard", "false");
        $("#myModal .modal-header").addClass("d-block");
        $("#myModal .modal-title")
            .addClass("text-center text-adcs")
            .html(__("Wait! It is being submitted..."));
        $("#myModal .modal-header button.close").remove();
        $("#myModal .modal-body").html(
            '<div class="text-center p-3"> <i class="fa fa-5x fa-spinner fa-spin text-adcs"></i> </div>'
        );
        $("#myModal .modal-footer").hide();
        $("#myModal").modal("show");
        return true;
    });

    $(".btnPrint").click(function () {
        window.print();
    });

    if ($(".select2").length > 0) {
        $(".select2").select2();
    }
    if ($(".txtDate").length > 0) {
        $(".txtDate").datepicker({
            format: "yyyy-mm-dd",
        });
    }
    if ($(".dt2").length > 0) {
        $(".dt2").DataTable();
    }
});



function getCookie(name) {
    var cookies = document.cookie.split(';');
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf(name + '=') === 0) {
        return cookie.substring(name.length + 1);
        }
    }
    return null;
}