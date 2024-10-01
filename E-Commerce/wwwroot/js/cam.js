

$(function(){
    var vp = false;
    
    var video = document.getElementById("video");
    var photo = document.getElementById("photo");
    var btnDownload = document.getElementById("btn-download");
    var vendorUrl = window.URl || window.webkitURL;
    var canvas = document.getElementById("canvas");
    var context = canvas.getContext('2d');

    $(".btn-preview").click(function(e){
        e.preventDefault();
         
        navigator.getMedia = navigator.getUserMedia ||
                            navigator.webkitGetUserMedia ||
                            navigator.mozGetUserMedia ||
                            navigator.msGetUserMedia;
         
        navigator.getMedia({
            video: { width: video.width, height: video.height  },
            audio:false            
        },function(stream){
            video.srcObject = stream;
            video.play();
            $(".btn-preview").hide();
            $(".btn-stop").show();
            $('.filters,.btn-quick-filter').removeAttr('disabled');
        },function(error){
            alert('Error Previewing, Please check camera connection.');
        }); 
        
    });//preview.click()

    $('.btn-stop').click(function(e){
        e.preventDefault();
        var stream = video.srcObject;
        var tracks = stream.getTracks();
        for (var i = 0; i < tracks.length; i++) {
            var track = tracks[i];
            //console.log(track.getCapabilities());
            track.stop();
        }
        video.srcObject = null;
        $(".btn-preview").show();
        $(".btn-stop").hide();
        $('.filters,.btn-quick-filter').attr('disabled','disabled');
    });

    $('.btn-capture').click(function(e){
        e.preventDefault();
        context.filter = video.style.filter;
        
        context.drawImage(video,0,0,video.width,video.width);
        photo.setAttribute('src',canvas.toDataURL('image/png'));
        // btnDownload.setAttribute('href',canvas.toDataURL('image/png'));
        $('.card-front .sh-image,#myModal #img').attr('src',canvas.toDataURL('image/png'));
        $('.btn-stop').trigger('click');
        $("#btn-download,#btn-upload").show();
        $('a[href="#tab-img"]').trigger('click');
        return false;
    });

    $('.filters').change(function(){
        var computedFilters = '';
        $('.filters').each(function(index,item){
            $(this).parent().find('span').html($(this).val()+$(this).attr('data-scale'));
            computedFilters += item.getAttribute('data-filter')+'('+item.value+item.getAttribute('data-scale')+') ';
        });
        video.style.filter = computedFilters;
        context.filter = video.style.filter;
    });

    $('.filters').each(function(index,item){
        $(this).parent().find('span').html($(this).val()+$(this).attr('data-scale'));
    });
     
    $("#btn-upload").click(function(e){
        e.preventDefault();
        $.ajax({
            url:'../upload',
            type:'POST',
            data:{'id':$("#frm_id").val(),'image':$("#photo").attr('src')},
            beforeSend:function(){
                $("#btn-upload").disabled=true;
                $("#btn-upload .fa-save").hide();
                $("#btn-upload .fa-spinner").show();
            },success:function(resp){
                $("#btn-upload").disabled=false;
                $("#btn-upload .fa-save").show();
                $("#btn-upload .fa-spinner").hide();
                alert(resp);
            },error:function(jqXHR, exception){
                $("#btn-upload").disabled=false;
                $("#btn-upload .fa-save").show();
                $("#btn-upload .fa-spinner").hide();
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connected.\n Verify Network.';
                } else if (jqXHR.status == 403) {
                    msg = 'You are logged out, Please get login first.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                alert(msg);
                
            }//end error
            
        });
        return false;
    });

    var prv = setInterval(function(){
        
        //context.drawImage(video,0,0,canvas.width,canvas.height);
        // var zoomval = $("#zoom").val();
        // var vw =  video.offsetWidth;
        // var vh =  video.offsetHeight;
         
        // var sx =   270 + parseInt(zoomval) *0.8;
        // var sy =   parseInt(zoomval) * 1.2;
        // var sw = vw - zoomval;
        // var sh = vh - (zoomval*1.2); 
        // var nw = vw + parseInt(zoomval);
        // var nh = vh + (zoomval*1.2); 
        
         
          
        //context.clearRect(video,0, 0, canvas.width, canvas.height);
        //context.drawImage(video, sx,sy, sw,sh,0,0,nw/2 ,nh/2 );
        context.drawImage(video,0,0);
        //context.scale(-1, 1);
        
    }, 100);

    $(".btn-quick-filter").click(function(){
        var ths = $(this);
        
        var mode = ths.attr('data-mode');
        $('.filters').each(function(i,v){    
            var thisFilter = $(this);
            var attr_default    = thisFilter.attr('data-default');
            var attr_decent     = thisFilter.attr('data-decent');
            var attr_extra      = thisFilter.attr('data-extra');
            console.log(attr_default);
            if(mode=='default'){
                if(typeof attr_default !== typeof undefined && attr_default !== false){
                    thisFilter.val(thisFilter.attr('data-default'));
                }
            }else if(mode=='decent'){
                if(typeof attr_decent !== typeof undefined && attr_decent !== false){
                    thisFilter.val(thisFilter.attr('data-decent'));
                }
            }else if(mode=='extra'){
                if(typeof attr_extra !== typeof undefined && attr_extra !== false){
                    thisFilter.val(thisFilter.attr('data-extra'));
                }
            }
        });
        $('.filters').eq(0).trigger('change');
        $(".btn-quick-filter").removeClass('active btn-success').addClass('btn-info');
        ths.addClass('active btn-success').removeClass('btn-info');
        $('.filters').eq(0).focus();
    });


/***Cropper */
    var $image;
    var cropBoxData;
    var canvasData;
    $(".btn-goto-crop").click(function(e){
        e.preventDefault();
        $("#myModal").modal('show');

        $("#myModal").modal('show');
        $image = $('#img');
        $image.cropper({
            built: function () {
                $image.cropper("setCropBoxData", { width: "100", height: "120" });
                $image.cropper('setCanvasData', canvasData);
                $image.cropper('setCropBoxData', cropBoxData);
            },
            cropend: function (e) {
                var imageData = $(this).cropper('getCanvasData()');
                $(e.target).cropper('setCanvasData', imageData);
            },
            movable: true,
            zoomable: true,
            checkCrossOrigin: true,
            preview: '.preview',
            minCropBoxHeight: 100,
            minCropBoxWidth: 120,
            aspectRatio: 10/12,
            viewMode:0
        });
    });

    $("#btn-cropit").click(function(e){
        e.preventDefault();
        var croppedImgData = $image.cropper("getCroppedCanvas", { width: 600, height: 720 });
        $('#photo,.sh-image,#myModal #img').attr('src',croppedImgData.toDataURL("image/png"));
        $image.cropper('destroy');
        $('#myModal #img').attr('src',croppedImgData.toDataURL("image/png"));
        $("#myModal").modal('hide');
    });

    $(".btn-proxy-upload").click(function(e) {
        e.preventDefault();
        $("#uploadFile").click();
    });
    $("#uploadFile").change(function (event) {
        var val = $(this).val();
        switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
            case 'gif': case 'jpg': case 'png': case 'jpeg':
                 
                var tmppath = (window.URL ? URL : webkitURL).createObjectURL(event.target.files[0]);//URL.createObjectURL(event.target.files[0]);
                console.log(tmppath);
                $("#photo,.sh-image,#myModal #img").attr('src', tmppath);
                $(".btn-goto-crop").trigger('click');
                break;
            default:
                $(this).val(''); 
                alert("Invalid image format.");
                break;
        }
    });//file change

    $('#myModal').on('hidden.bs.modal', function () {
        $image.cropper('destroy');
    });
    
    $(".rotate-left").click(function(){
        $image.cropper('rotate', -90);
    });
    $(".rotate-right").click(function(){
        $image.cropper('rotate', 90);
    });
    

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href") // activated tab
        if(target=='#tab-cam'){
            $(".btn-preview").trigger('click');
        }else{
            $(".btn-stop").trigger('click');
        }
      });
});




