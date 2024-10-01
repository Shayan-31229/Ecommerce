var $image;
var cropBoxData;
var canvasData;

function Init() {
    
    $image = $('#img');
    $image.cropper({
        built: function () {
            $image.cropper("setCropBoxData", { width: "400", height: "400" });
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
        minCropBoxHeight: 60,
        minCropBoxWidth: 60,
        aspectRatio: 1,
        viewMode:0
    });
    $(".btn-dp").attr("disabled", true);
}//end init
function resetToDefault() {
    $image.attr("src", "");
    $(".preview").html("");
    $("#uploadFile").val("");
    $(".btn-dp").attr("disabled", true);
}//reset
$(function(){
    //Init();
    $("#uploadFile").change(function (event) { 
        var val = $(this).val();
        switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
            case 'gif': case 'jpg': case 'png': case 'jpeg':
                $(".tohide").removeClass('hideit');
                var tmppath = (window.URL ? URL : webkitURL).createObjectURL(event.target.files[0]);//URL.createObjectURL(event.target.files[0]);
                 
                $image.cropper('replace', tmppath);
                $(".btn-dp").attr("disabled", false);
                break;
            default:
                $(this).val(''); 
                $("#lblError").html("Invalid image format.").fadeIn();
                setTimeout(function() {
                    $("#lblError").fadeOut();
                },5000);
                break;
        }
    });//file change
    
    $("#btnUpload").click(function (e) {
        e.preventDefault();
        $(this).toggleClass('active');
        $('#uploadFile, .btn-dp').attr('disabled',true);
        var convas = $image.cropper("getCroppedCanvas", { width: 400, height: 400 });
        var image = convas.toDataURL("image/png");
        image = image.replace('data:image/png;base64,', '');
        
        var convas2 = $image.cropper("getCroppedCanvas", { width: 160, height: 160 });
        var image2 = convas2.toDataURL("image/png");
        image2 = image2.replace('data:image/png;base64,', '');
        
        var convas3 = $image.cropper("getCroppedCanvas", { width: 32, height: 32 });
        var image3 = convas3.toDataURL("image/png");
        image3 = image3.replace('data:image/png;base64,', '');
        
        var imageURL = domainWithLang+'admin/users/updatedp/'+$("#txtProfile").val();
        console.log(imageURL);
        $.post(imageURL, { i1: image, i2: image2,i3: image3 }, function (response) {
            if (response.toString().indexOf("Error") < 0) {
                $('#dpImg').attr('src',domain+'/img/users/dp/'+response+'?r='+Date.now());
                $('#msg').html('<div class="alert alert-success">Image uploaded successfully.</div>');
            }else{
                $('#msg').html('<div class="alert alert-danger">Could not upload image</div>');
            }
            $('#myModal2').modal('hide');
            setTimeout(function() {
                $('#msg .alert').fadeOut();
            },10000);
            resetToDefault();
        });
    });
    
    $('#myModal2').on('hidden.bs.modal', function () {
        $('.tohide').addClass('hideit');
        $image.cropper('destroy');
        resetToDefault();
    });
    
    $(".rotate-left").click(function(){
        $image.cropper('rotate', -90);
    });
    $(".rotate-right").click(function(){
        $image.cropper('rotate', 90);
    });
});