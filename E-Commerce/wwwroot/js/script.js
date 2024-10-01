$(function(){

    //check if already full screen toggle classes accordingly
    if (!window.screenTop && !window.screenY) {
        $('#btnFullscreen').removeClass('fs-enabled').addClass('fs-disabled');
    }else{
        $('#btnFullscreen').removeClass('fs-disabled').addClass('fs-enabled');
    }

    //toggle fullscreen
    $('#btnFullscreen').click(function(){
        var el = document.documentElement;
        if($(this).hasClass('fs-disabled')){
            if(el.requestFullscreen){
                el.requestFullscreen();
            }
            $('#btnFullscreen').removeClass('fs-disabled').addClass('fs-enabled');
        }else{
            if(document.exitFullscreen){
                document.exitFullscreen();
            }
            $('#btnFullscreen').removeClass('fs-enabled').addClass('fs-disabled');
        }
    });

});