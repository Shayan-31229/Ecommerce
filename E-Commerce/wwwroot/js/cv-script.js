var domain = window.location.host;
if(domain == "localhost"){
    var pathArray = window.location.pathname.split( '/' );
    domain = domain+"/"+pathArray[1];
}
domain = window.location.protocol+"//"+domain;

function isEmail(email) {
  var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
  return regex.test(email);
}

$(function(){
    $('div[data-type="background"]').each(function(){
        var obj = $(this);
        $(window).scroll(function(){
            var y = -($(window).scrollTop()/obj.data('speed'));
            var coords = '50% '+y+'px';
            obj.css({
                backgroundPosition:coords
            });  
        });
    });//parallex bg
    
    //add margin top after clicking some menu
    $(function(){ 
        $(".main-nav a").click(function(e){
            e.preventDefault();
            var href = e.target.href, id = "#" + href.substring(href.indexOf("#") + 1);
            $(window).scrollTop($(id).offset().top - 50);
         });
    });
    
    //tooltip on buttons
    $('[data-toggle="tooltip"]').tooltip();
    
    //collapse top menu dropdown after click on mobile view
    $(document).on('click','.navbar-collapse.in',function(e) {
        if( $(e.target).is('a') ) {
            $(this).collapse('hide');
        }
    });

    //copy contact infos
    $(".copyIt").click(function(){
        var str = $(this).parent().find('.copyThis').html();
        copyToClipboard(str);
        //window.prompt("Copy to clipboard: Ctrl+C, Enter", str);
        //alert($(this).attr('rel')+" copied to clipboard");
    });
    $(".copyLink").click(function(){
        var str = $(this).parent().find('.copyThis').attr('href');
        copyToClipboard(str);
        //window.prompt("Copy to clipboard: Ctrl+C, Enter", str);
        //alert($(this).attr('rel')+" copied to clipboard");
    });
    
     

    //contact form submission
    $("#btnSubmit").click(function(e){
        e.preventDefault();
        var name=$("#name").val().trim();
        if(name==''){
            alert('Please enter your Name');
            return false;
        }
        
        var email=$("#email").val().trim();
        if(email==''){
            alert('Please enter your Email');
            return false;
        }
        if(!isEmail(email)){
            alert('Please enter valid Email');
            return false;
        }
        
        var msg=$("#message").val().trim();
        if(msg==''){
            alert('Please enter your Message');
            return false;
        }
        // var captcha=$("#ContactCaptcha").val().trim();
        // if(captcha==''){
        //     alert('Please enter captcha');
        //     return false;
        // }
        var vars = {name:name,email:email,message:msg,captcha: grecaptcha.getResponse()};
        var postVars = {'Contacts':vars}
        $.ajax({
            url:domain+'/cv/contact',
            type:'post',
            data:postVars,
            headers: {
                'X-CSRF-Token': csrfToken
            },
            beforeSend:function(){
                $('#imgProcessing').show();
                $("#result").html('');
                grecaptcha.reset();
            },
            success:function(data){
                //$("#name,#email,#message").val('');
                $("#result").html(data);
                $('#imgProcessing').hide();
                if(data=='<div class="alert alert-success alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>Message sent successfully.</div>'){
                    $("#name,#email,#message,#captcha").val('');
                }else if(data=='wrong captcha.'){
                    $('#captcha').select();
                }
                $("#btn-refresh-captcha").trigger('click');
                $("html, body").animate({ scrollTop: $(document).height() }, 1000);
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                $("#result").html(XMLHttpRequest.responseText);
                $('#imgProcessing').hide();
            }
        });
    });
    
    $("#btn-refresh-captcha").click(function(){
        
        var src = domain+'/cv/captcha/'+(new Date).getTime();
        $("#img-captcha").attr('src',src);
    });

    function copyToClipboard(text) {
        var input = document.body.appendChild(document.createElement("input"));
        input.value = text;
        input.focus();
        input.select();
        document.execCommand('copy');
        input.parentNode.removeChild(input);
        toastr.success("Copied");
      }
});//end jq 
