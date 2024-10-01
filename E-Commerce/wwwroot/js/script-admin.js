$(function () {
    // SideNav Initialization
    $(".button-collapse").sideNav({
        edge: _isRtl ? "right" : "left", // Choose the horizontal origin
        // breakpoint: 1700,   // Breakpoint for button collapse
        // menuWidth: 240,     // Width for sidenav
        //slim: true,
    });
    var container = document.querySelector(".custom-scrollbar");
    var ps = new PerfectScrollbar(container, {
        wheelSpeed: 2,
        wheelPropagation: true,
        minScrollbarLength: 20,
    });

    // let container = document.querySelector('.custom-scrollbar');
    // if(container.length>0){
    //     var ps = new PerfectScrollbar(container, {
    //         wheelSpeed: 2,
    //         wheelPropagation: true,
    //         minScrollbarLength: 20
    //     });
    // }

    if ($(".mdb-select").length > 0) {
        $(".mdb-select").materialSelect();
    }
    if ($(".txtDate").length > 0) {
        $(".txtDate").datepicker({
            format: "yyyy-mm-dd",
            autoclose: true,
            todayBtn: true,
            todayHighlight: true,
            language: _lang,
        });
    }

    if ($(".select2").length > 0) {
        $(".select2").select2();
    }
    if ($(".txtLang").length > 0) {
        $(".txtLang").alamBiLangInput();
    }

    // setTimeout(function () {
    //     $.ajax({
    //         url: domainWithLang+"admin/contacts/newcontacts",
    //         success: function (resp) {
    //             var obj = JSON.parse(resp);
    //             var str = '';
    //             $.each(obj,function(i,c){
    //                 str+='<a class="dropdown-item newcontact-item" href="'+domain+'/admin/contacts/view/'+c.id+'">';
    //                 str+= '<span class="email">'+c.email+'</span><span class="date">'+c.created+'</span>';
    //                 str+='</a>';
    //             });
    //             $('.newcontacts').html(str);
    //             $('.notifications-nav>a>span.badge.red').html(obj.length);
    //         }, error: function (a, b) {
    //             $('.newcontacts').html('error geting new contacts');
    //         }
    //     });
    // }, 100);

    new WOW().init();

    $("body").on("submit", ".formLoader", function () {
        var frm = $(this);
        frm.find('[type="submit"]').prop("disabled", true);
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

    $("body").on("change", ".txtFile", function () {
        var allowedMBs = 10;
        var txt = $(this);
        if (txt[0].files.length > 0) {
            var size = txt[0].files[0].size;
            if (size > allowedMBs * 1024 * 1024) {
                txt.val("");
                toastr.error(
                    "File size is too big, max " + allowedMBs + "MB allowed."
                );
                return false;
            }
        }
    });

    if ($(".dt2").length > 0) {
        $(".dt2").DataTable();
    }

    $(".btn-maxy").click(function (e) {
        e.preventDefault();
        var isMaximized = $(this).parents(".maxy").hasClass("maximized");
        console.log(isMaximized);
        if (isMaximized) {
            $(this)
                .parents(".maxy")
                .removeClass("maximized")
                .addClass("normal");
        } else {
            $(this)
                .parents(".maxy")
                .addClass("maximized")
                .removeClass("normal");
        }
    });
});

function __(str) {
    return str;
}


function nl2br (str, is_xhtml) {   
    var breakTag = (is_xhtml || typeof is_xhtml === 'undefined') ? '<br />' : '<br>';    
    return (str + '').replace(/([^>\r\n]?)(\r\n|\n\r|\r|\n)/g, '$1'+ breakTag +'$2');
}

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