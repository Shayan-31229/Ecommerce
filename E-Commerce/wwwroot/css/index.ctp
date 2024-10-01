<!DOCTYPE html>
<html>
    <head>
        <title>Alam CV</title>
        <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="Fakhr-e-Alam cakephp webdeveper at Peshawar Pakistan having 4+ years experience. visit my online resume and download a copy in MS word of pdf format or directly contact via email, skype or phone. I have good knowledge and experience of HTML5, PHP, CakePHP, CSS3, Javascript, JQuery, Ajax, JSON, Web services, Bootstrap, Responsive designing, MySql, MS Sql Server, Oracle 19g and many more">
        <meta name="keywords" content="alam cv, fakhr, resume, cv, cakephp, php, web developer, Peshawar">
        <meta property="og:title" content="Alam CV" />
        <meta property="og:description" content="Fakhr-e-Alam cakephp webdeveper at Peshawar Pakistan having 4+ years experience. visit my online resume and download a copy in MS word of pdf format or directly contact via email, skype or phone. I have good knowledge and experience of HTML5, PHP, CakePHP, CSS3, Javascript, JQuery, Ajax, JSON, Web services, Bootstrap, Responsive designing, MySql, MS Sql Server, Oracle 19g and many more">
        <meta property="og:type" content="website" />
        <meta property="og:url" content="http://alampk.com/" />
        <meta property="og:image" content="http://alampk.com/images/alam.png" />
        <meta name="author" content="Fakhr-e-Alam">
        <link rel="icon" href="images/favicon.png">
        <?php
            echo $this->Html->meta('favicon.ico','img/favicon.png',array('type' => 'icon'));
        ?>
        <?php 
            echo '<!--styles-->';
            echo $this->Html->css('bootstrap-min');
            echo $this->Html->css('Font-Awesome/css/font-awesome.min');
            echo $this->Html->css('http://fonts.googleapis.com/css?family=Raleway:400,700');
            echo $this->Html->css('cv-style');
            echo '<!--scripts-->';
            echo $this->Html->script('jquery-2.1.4.min');
            echo $this->Html->script('jquery-ui.min');
            echo $this->Html->script('bootstrap.min');
            echo $this->Html->script('cv-script');
        ?>
        
        <!--end scripts-->
        <script>
            $(function(){ 
                $(".nav a").click(function(e){
                    e.preventDefault();
                    var href = e.target.href, id = "#" + href.substring(href.indexOf("#") + 1);
                    $(window).scrollTop($(id).offset().top - 50);
                 });
            });
        </script>
        <!-- HTML5 shiv and Respond.js IE8 support for HTML5 elements and media queries -->
        <!--[if IE]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
        
    </head>
    <body data-spy="scroll" data-target=".navbar" data-offset="60">
        <!--header-->
        <header class="site-banner" role="banner">
            <div class="navbar-wrapper">
                <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                    <div class="container">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                <span class="sr-only">Toggle Navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <?php echo $this->Html->link('ALAM CV','/',array('class'=>'navbar-brand')); ?>
                        </div><!--.navbar-header-->
                        <div class="navbar-collapse collapse">
                            <ul class="nav navbar-nav navbar-right" id="topMenu">
                                <li class="active"><a href="#slider-section">Intro</a></li>
                                <li><a href="#edu">Education</a></li>
                                <li><a href="#skills">Skills</a></li>
                                <li><a href="#exp">Exp</a></li>
                                <li><a href="#projects">Portfolio</a></li>
                                <li><a href="#testimonial">Testimonial</a></li>
                                <li><a href="#contact">Contact</a></li>
                            </ul><!--.nav-->
                        </div><!--.navbar-collapse-->
                    </div><!--.container-->
                </div><!--navbar-->
            </div><!--.navbar-wrapper-->
            
        </header>
        <!--end header-->
        
        <!--slider-->
        <section id="slider-section" data-type="background" data-speed="4">
            <article>
                <div class="container clearfix">
                    <div class="row">
                        <div class="col-sm-5 text-center">
                            <?php
                                echo $this->Html->image($infos['cv_image'],array('class'=>'img-circle','alt'=>'cv_image','style'=>'width:80%;'));
                            ?>
                        </div>
                        <div class="col-sm-7 slider-text text-center">
                            <h1><?php echo $infos['name']; ?></h1>
                            <h2><?php echo $infos['post']; ?></h2>
                            <p class="introBox"><?php echo nl2br($infos['intro']); ?></p>
                        </div>
                    </div><!--.row-->
                </div><!--.container-->
            </article>
        </section>
        <!--end slider-->
        
        <!--icons-->
        <section id="downloads">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <p> View my 
                            
                            <?php 
                                echo $this->Html->link('<i class="icon-print"></i> Printable',array(
                                    'action'=>'download'
                                ),array(
                                    'class'=>'btn btn-primary',
                                    'target'=>'_blank',
                                    'data-toggle'=>'tooltip',
                                    'title'=>'View my CV in print ready format here without downloading.',
                                    'escape'=>false
                                )); 
                            ?>
                            CV or download in
                            <?php 
                                echo $this->Html->link('<i class="icon-file-text"></i> WORD format',array(
                                    'action'=>'download','word'
                                ),array(
                                    'class'=>'btn btn-info',
                                    'data-toggle'=>'tooltip',
                                    'title'=>'Download my CV in MS WORD format.',
                                    'escape'=>false
                                )); 
                            ?>
                            &nbsp;
                            <?php 
                                echo $this->Html->link('<i class="icon-file"></i> PDF format',array(
                                    'action'=>'download','pdf'
                                ),array(
                                    'class'=>'btn btn-success',
                                    'data-toggle'=>'tooltip',
                                    'title'=>'Download my CV in PDF format.',
                                    'escape'=>false
                                )); 
                            ?>
                        </p>
                    </div>
                </div>
                
            </div><!--.container-->
        </section>
        <!--end icons-->
        
        <!--edu-->
        <section id="edu">
            <div class="container">
                <h3>Academic Qualification</h3>
                    <?php 
                        foreach($edus as $eduKey=>$edu): 
                            $offset = $eduKey +($eduKey + 1);
                    ?>
                        <div class="row masters">
                            <div class="col-sm-offset-<?php echo $offset; ?> col-xs-12 col-sm-6 col-lg-4">    
                                <?php if($eduKey==0): ?>
                                <i class="icon-trophy text-center" style="display:block;font-size:42px;color:#F9FF07;"></i>
                                <?php endif; ?>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <i class="icon-certificate"></i>
                                        <?php echo $edu['Education']['degree']; ?>
                                    </div><!--.panel-heading-->
                                    <div class="panel-body">
                                        <p><small><?php echo $edu['Education']['from']; ?> - <?php echo $edu['Education']['to']; ?></small></p>
                                        <p><?php echo $edu['Education']['institute']; ?></p>
                                        <?php if($edu['Education']['award']!=null): ?>
                                        <small class="label label-success"> <i class="icon-star"></i> <?php echo $edu['Education']['award']; ?> </small>
                                        <?php endif; ?>
                                    </div><!--.panel-body-->
                                </div><!--.panel-->
                            </div>
                        </div><!--.row-->
                    <?php endforeach; ?>
            </div><!--.container-->
        </section>
        <!--end edu-->
        
        <!--skills-->
        <section id="skills" data-type="background" data-speed="3">
            <div class="container">
                <h3>Skills</h3>
                <div class="row">
                    <div class="col-sm-4">
                        <h4>Expert level</h4>
                        <p><small>Working with the following currently on daily basis since 2011</small></p>
                        <?php foreach ($skills1 as $skill1): ?>
                            <span class="label label-success"><?php echo $skill1['Skill']['skill'] ?></span>
                        <?php endforeach; ?>
                    </div><!--expert-->
                    <div class="col-sm-4">
                        <h4>Intermediate level</h4>
                        <p><small>Have worked with the following till 2 years back and didn't touch after that.</small></p>
                        <?php foreach ($skills2 as $skill2): ?>
                            <span class="label label-primary"><?php echo $skill2['Skill']['skill'] ?></span>
                        <?php endforeach; ?>
                    </div><!--intermediate-->     
                    <div class="col-sm-4">
                        <h4>Beginner level</h4>
                        <p><small>Also got chance to work with the following at beginner level</small></p>
                        <?php foreach ($skills3 as $skill3): ?>
                            <span class="label label-warning"><?php echo $skill3['Skill']['skill'] ?></span>
                        <?php endforeach; ?>
                    </div><!--beginner-->
                </div><!--.row-->
            </div><!--.container-->
        </section>
        <!--end skills-->
        
        <!--exp-->
        <section id="exp">
            <div class="container">
                <h3>Experience</h3>
                <?php foreach ($exps as $exp): ?>
                <?php
                    $eto= (($exp['Experience']['to']=='-' || $exp['Experience']['to']=='')?'Present':$exp['Experience']['to']);
                ?>
                <div class="row exp-item">
                    <div class="col-lg-12">
                        <h4><?php echo $exp['Experience']['company']; ?></h4>
                        <h5><?php echo $exp['Experience']['post']; ?></h5>
                        <p><small><?php echo $exp['Experience']['from']; ?> - <?php echo $eto; ?></small></p>
                        <p><?php echo nl2br($exp['Experience']['descr']); ?></p>
                    </div><!--.col-->
                </div><!--.exp-item-->
                <?php endforeach; ?>
            </div><!--.container-->
        </section>
        <!--end exp-->
        
        <!--projects-->
        <section id="projects" data-type="background" data-speed="2">
            <div class="container">
                <h3>Portfolio</h3>
                <div class="row">
                    <div class="col-xs-12 text-center alert alert-success">
                        <p>I have developed number of projects in my career using different frameworks and languages. to see all click <?php echo $this->Html->link('View All',array('controller'=>'portfolios','action'=>'index'),array('class'=>'btn btn-success btn-xs')); ?></p>
                    </div><!--.col-->
                </div><!--.row-->
                <div class="row">
                    <?php foreach($pfolios as $pfolio): ?>
                        <div class="col-xs-12 col-sm-4 col-lg-3 ">
                            <div class="p-item">
                                <?php
                                    $ptitle  = array();
                                    foreach($pfolio['PortfolioTag'] as $tag){
                                        $ptitle[]=$tag['Tag']['tag'];
                                    }
                                ?>
                                <div class="p-img">
                                    <?php $snap = isset($pfolio['PSnap'][0]['snap'])?$pfolio['PSnap'][0]['snap']:'Loading'; ?>
                                    <?php echo $this->Html->image('portfolios/thumbs/400x_'.$snap,array('alt'=>$snap,'title'=>implode(', ', $ptitle))); ?>
                                </div><!--.p-img-->
                                <div class="p-detail" title="click to view detail">
                                    <?php 
                                        echo $this->Html->link($pfolio['Portfolio']['client'],array('controller'=>'portfolios','action'=>'view',$pfolio['Portfolio']['id']));
                                    ?>
                                </div>
                            </div><!--.pitem-->
                        </div><!--.col-->
                    <?php endforeach;?>
                </div><!--.row-->
                
            </div><!--container-->
        </section>
        <!--end projects-->
        
        <!--testimonial-->
        <section id="testimonial">
            <div class="container">
                <h3>Testimonials</h3>
                <p><small>What my clients say about me</small></p>
                <div class="row">
                    <?php foreach($testimonials as $testimonial): ?>
                        <div class="col-sm-6">
                            <div class="t-item">
                                    <?php echo $this->Html->image('clients/'.$testimonial['Client']['image'],array(
                                        'class'=>'img-circle clientImg',
                                        'alt'=>$testimonial['Client']['name']
                                        )); ?>
                                    <hr>
                                    <strong><?php echo $testimonial['Client']['name']; ?></strong><br>
                                    <small><?php echo $testimonial['Client']['designation']; ?></small>
                                    <hr>
                                <div class="quote">
                                    <i class="icon-quote-left" id="qleft"></i>
                                    <p><?php echo $testimonial['Testimonial']['testimonial']; ?></p>
                                    <i class="icon-quote-right" id="qright"></i>
                                </div><!--.quote-->
                            </div><!--.t-item-->
                        </div><!--.col-->
                    <?php endforeach; ?>
                    
                </div><!--.row-->
            </div><!--.container-->
        </section>
        <!--end testimonial-->
        
        <!--contact us-->
        <section id="contact">
            <div class="contactHeading bg-primary">
                <div class="container">
                    <h3>Contact me</h3>
                </div><!--.container-->
            </div><!--.contactHeader-->
            <div style="height:380px; overflow: hidden;">
                    <div id="map1" style="width:100%;height:450px;"></div><!--#map-->
                    <script src="https://maps.googleapis.com/maps/api/js"></script>
                    <script>
                      function initialize() {
                        var mapCanvas = document.getElementById('map1');
                        var myCenter = new google.maps.LatLng(<?php echo isset($infos['latitude'])?$infos['latitude']:'34.008554'; ?>, <?php echo isset($infos['latitude'])?$infos['longitude']:'71.580837'; ?>);
                        var mapOptions = {
                          center: myCenter,
                          zoom: 8,
                          disableDefaultUI: true,
                          mapTypeId: google.maps.MapTypeId.ROADMAP //SATELLITE ROADMAP HYBRID TERRAIN 
                        }
                        
                        var contentString = '<div id="content">'+
                            '<div id="siteNotice">'+
                            '</div>'+
                            '<h4 id="firstHeading" class="firstHeading">Current Location : PESHAWAR</h4>'+
                            '<div id="bodyContent">'+
                            '<p>Currently at hometown Peshawar.</p>'+
                            '</div>'+
                            '</div>';

                        var infowindow = new google.maps.InfoWindow({
                          content: contentString
                        });

                        var marker=new google.maps.Marker({
                            position:myCenter,
                            title:'Peshawar, Pakistan',
                            animation:google.maps.Animation.BOUNCE
                        });


                        var map = new google.maps.Map(mapCanvas, mapOptions);            
                        marker.setMap(map);
                        
                        marker.addListener('click', function() {
                            infowindow.open(map, marker);
                          });
                      }
                      google.maps.event.addDomListener(window, 'load', initialize);

                    </script>
            </div>
            
            <div class="container-fluid bg-primary">
            <div class="row">
                <div class="col-xs-12 col-sm-6 contactBoxes">
                    <h3>Contact Info</h3>
                    <p class="small">Know more about me</p>
                    <div class="row contactLinks">
                        <div class="col-sm-4">
                            <label>
                                <i class="icon-linkedin-sign"></i>
                                Linkedin
                            </label>
                        </div><!--.col-->
                        <div class="col-sm-8">
                            <a  class="copyThis" href="https://pk.linkedin.com/in/alamnaryab" target="_blank"><i class="icon-linkedin-sign"></i>/alamnaryab</a>
                            <i rel="LinkedIn Link" class="icon-copy copyLink" title="copy link address"></i>
                        </div><!--.col-->
                    </div><!--.contact links-->
                    <div class="row contactLinks">
                        <div class="col-sm-4">
                            <label>
                                <i class="icon-stackexchange"></i>
                                Stackoverflow
                            </label>
                        </div><!--.col-->
                        <div class="col-sm-8">
                            <a class="copyThis" href="http://careers.stackoverflow.com/alamnaryab" target="_blank"><i class="icon-stackexchange"></i>/alamnaryab</a>
                            <i class="icon-copy copyLink" rel="Stackoverflow Link" title="copy link address"></i>
                        </div><!--.col-->
                    </div><!--.contact links-->
                    <div class="row contactLinks">
                        <div class="col-sm-4">
                            <label>
                                <i class="icon-envelope"></i>
                                Email
                            </label>
                        </div><!--.col-->
                        <div class="col-sm-8">
                            <span class="copyThis">alamnaryab@gmail.com</span>
                            <i class="icon-copy copyIt" rel="Email Address" title="copy email ID"></i>
                        </div><!--.col-->
                    </div><!--.contact links-->
                    <div class="row contactLinks">
                        <div class="col-sm-4">
                            <label>
                                <i class="icon-skype"></i>
                                Skype
                            </label>
                        </div><!--.col-->
                        <div class="col-sm-8">
                            <span class="copyThis">alamnaryab</span>
                            <i class="icon-copy copyIt" rel="skype Address" title="copy skype address"></i>
                        </div><!--.col-->
                    </div><!--.contact links-->
                    <div class="row contactLinks">
                        <div class="col-sm-4">
                            <label>
                                <i class="icon-mobile-phone"></i>
                                Mobile
                            </label>
                        </div><!--.col-->
                        <div class="col-sm-8">
                            <span class="copyThis">+923005662558</span>
                            <i class="icon-copy copyIt" rel="Mobile Number" title="copy mobile number"></i>
                        </div><!--.col-->
                    </div><!--.contact links-->
                    <div class="row contactLinks">
                        <div class="col-sm-4">
                            <label>
                                <i class="icon-whatsapp"></i>
                                Whats App
                            </label>
                        </div><!--.col-->
                        <div class="col-sm-8">
                            <span class="copyThis">+923005662558</span>
                            <i class="icon-copy copyIt" rel="WhatsApp Number" title="copy WhatsApp number"></i>
                        </div><!--.col-->
                    </div><!--.contact links-->
                    
                </div><!--.col-->
                <div class="col-xs-12 col-sm-6 contactBoxes">
                    <h3>Write Me</h3>
                    <p class="small">I won't bite you for writing me. <i class="icon-smile"></i></p>
                    <div id="formContainer1">
                        <?php echo $this->Form->create('Contact', array(
    'url' => array('controller' => 'cv', 'action' => 'contact')
)); ?>                      
                        <div class="row">
                                <?php 
                                    echo $this->Form->input('name',array(
                                        'div'=>array('class'=>'form-group col-xs-12 col-sm-6'),
                                        'label'=>false,
                                        'class'=>'form-control',
                                        'placeholder'=>'Enter your full name',
                                        'after'=>'<small class="errMsg">Please enter your full name</small>'
                                    )); 
                                ?>
                                <?php 
                                    echo $this->Form->input('email',array(
                                        'div'=>array('class'=>'form-group col-xs-12 col-sm-6'),
                                        'label'=>false,
                                        'class'=>'form-control',
                                        'placeholder'=>'Enter your Email',
                                        'after'=>'<small class="errMsg">Please enter your email ID</small>'
                                    )); 
                                ?>
                        </div>
                        <div class="row">
                                <?php 
                                    echo $this->Form->input('message',array(
                                        'div'=>array('class'=>'form-group col-lg-12'),
                                        'label'=>false,
                                        'type'=>'textarea',
                                        'class'=>'form-control',
                                        'placeholder'=>'Enter your message',
                                        'after'=>'<small class="errMsg">Please type valid message</small>'
                                    )); 
                                ?>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6">
                                <?php echo $this->Html->image('processing.gif',array('id'=>'imgProcessing','style'=>'background:transparent;width:20px;display:none;')); ?>
                                <div id="result"></div>
                            </div><!--.col-->
                            <div class="col-xs-12 col-sm-6 text-right">
                                <input type="reset" class="btn btn-danger" value="Cancel"/>
                                <?php echo $this->Form->submit('Send message',array('div'=>false,'class'=>'btn btn-success','id'=>'btnSubmit')); ?>                  
                            </div><!--.col-->
                        </div><!--.row-->
                                
                                <?php echo $this->Form->end(); ?>
                                <script>
                                var domain = window.location.host;
                                if(domain == "localhost"){
                                    var pathArray = window.location.pathname.split( '/' );
                                    domain = domain+"/"+pathArray[1];
                                }
                                domain = window.location.protocol+"//"+domain;
                                $(function(){
                                    $("#btnSubmit").click(function(e){
                                        e.preventDefault();
                                        var name=$("#ContactName").val().trim();
                                        if(name==''){
                                            alert('Please enter your Name');
                                            return false;
                                        }
                                        var email=$("#ContactEmail").val().trim();
                                        if(email==''){
                                            alert('Please enter your Email');
                                            return false;
                                        }
                                        var msg=$("#ContactMessage").val().trim();
                                        if(msg==''){
                                            alert('Please enter your Message');
                                            return false;
                                        }
                                        var vars = {name:name,email:email,message:msg};
                                        var postVars = {'Contact':vars}
                                        $.ajax({
                                            url:domain+'/cv/contact',
                                            type:'post',
                                            data:postVars,
                                            beforeSend:function(){
                                                $('#imgProcessing').show();
                                                $("#result").html('');
                                            },
                                            success:function(data){
                                                $("#name,#email,#msg").val('');
                                                $("#result").html(data);
                                                $('#imgProcessing').hide();
                                                $("#ContactName,#ContactEmail,#ContactMessage").val('');
                                            },
                                            error: function(XMLHttpRequest, textStatus, errorThrown) {
                                                $("#result").html(XMLHttpRequest.responseText);
                                                $('#imgProcessing').hide();
                                            }
                                        });
                                    });
                                    $.ajax({url:domain+'/visitors/hit/<?php echo (($this->request->referer() == '/')?"Direct":$this->request->referer()); ?>',type:'post'});
                                });
                                </script>
                    </div><!--.formContainer1--> 
                </div><!--.col-->
            </div><!--.row-->
            </div>
        </section>
        <script>
            $(function(){
                $('[data-toggle="tooltip"]').tooltip();
            });
            $(".copyIt").click(function(){
                var str = $(this).parent().find('.copyThis').html();
                window.prompt("Copy to clipboard: Ctrl+C, Enter", str);
                //alert($(this).attr('rel')+" copied to clipboard");
            });
            $(".copyLink").click(function(){
                var str = $(this).parent().find('.copyThis').attr('href');
                window.prompt("Copy to clipboard: Ctrl+C, Enter", str);
                //alert($(this).attr('rel')+" copied to clipboard");
            });
        </script>
        
        <!--google analytics-->
        <script>
            (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
            (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
            m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
            })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

            ga('create', 'UA-68135527-1', 'auto');
            ga('send', 'pageview');

        </script>
        
    </body>
</html>
