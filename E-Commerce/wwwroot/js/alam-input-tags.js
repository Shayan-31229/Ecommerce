;(function ( $ ) {
    $.fn.alamInputTags = function( options ) {
        var elem = this;
        var ajxReq=null;


        var opt = $.extend({ 
            url:null, 
            maxItems:0, 
            maxSuggessions:5, 
            data:[], 
            fieldMappings:null
        }, options );
        
        elem.addClass( "txt-alam-input-tags" );
        elem.wrap( "<div class='alam-input-tags-wrapper'></div>" );
        $('.alam-input-tags-wrapper').prepend('<div class="tags-wrapper d-inline"></div>');
        elem.wrap( "<div class='alam-input-wrapper d-inline-block position-relative'></div>" );
        $('.alam-input-wrapper').append('<div class="drp-wrapper position-absolute"></div>');
        var inputName = elem.attr('name')?elem.attr('name'):'unnamed';
        var inputValue = elem.attr('value')?elem.attr('value'):'';
        elem.after('<input type="text" name="'+inputName+'" value="'+inputValue+'" class="txtVals" style="display:none;">');
        
        elem.removeAttr('name');
        elem.val('');

        if(opt.url!=null){
            $.ajax({
                url:opt.url,
                beforeSend:function(){
                    if(ajxReq!=null){
                        ajxReq.abort();
                    }
                },success:function(resp){
                    try{
                        var obj = JSON.parse(resp);
                        opt.data = obj;
                        mapData();
                        createTags(inputValue);
                    }catch(err){
                        console.log(err);
                    }
                },error:function(){
                    console.log('ajax error');
                }
            });
        }else{
            mapData();
            createTags(inputValue); 
        }


            
        $('body').on('click','.alam-input-tags-wrapper',function(e){
            $(this).find('.txt-alam-input-tags').focus();
        });

        $('body').on('click','.alam-input-tags a.dropdown-item',function(e){
            e.preventDefault();
            var id = $(this).attr('data-id');
            var txt = $(this).text(); 
            var thisWrapper = $(this).parents('.alam-input-tags-wrapper');
            var thisTagsWrapper = thisWrapper.find('.tags-wrapper');

            var totalBadges = thisTagsWrapper.find('.badge').length;
            if(opt.maxItems>0 &&  totalBadges >= opt.maxItems){
                alert('Max '+opt.maxItems+' items are allowed');
                return false;
            }

            var thisTxt = thisWrapper.find('.txt-alam-input-tags');
            var thisTxtVals = thisWrapper.find('.txtVals'); 
            var thisClass = $(this).attr('data-class');
            thisTagsWrapper.append('<span class="badge badge-'+thisClass+' m-1"><span class="txt" data-id="'+id+'">'+txt+'</span> <span class="btn-remove">&times;</span> </span>');
            thisTxt.val('').trigger('keyup').focus();
            thisTxtVals.val(getSelectedIdsCSV());
        });

        $('body').click(function(){ 
            var thisWrapper = $('.alam-input-tags-wrapper'); 
            var thisdrpWrapper = thisWrapper.find('.drp-wrapper');
            thisdrpWrapper.hide();
        });
        $('body').keydown(function(e){ 
            if(e.which==27){
                var thisWrapper = $('.alam-input-tags-wrapper'); 
                var thisdrpWrapper = thisWrapper.find('.drp-wrapper');
                thisdrpWrapper.hide();
            }
        });

        $('body').on('click','.btn-remove',function(e){
            e.preventDefault();
            var thisWrapper = $(this).parents('.alam-input-tags-wrapper'); 
            var thisTxt = thisWrapper.find('.txt-alam-input-tags');
            var thisTxtVals = thisWrapper.find('.txtVals');
            $(this).parents('.badge').remove();
            thisTxtVals.val(getSelectedIdsCSV());
            thisTxt.focus();
        });

        function mapData(){
            if(opt.fieldMappings!=null){
                var data = opt.data.map(function(item){
                    return {
                        id      : item[opt.fieldMappings.id],
                        name    : item[opt.fieldMappings.name],
                        class   : item[opt.fieldMappings.class],
                    }
                });
                opt.data = data;
            }
        }

        function createTags(inputValue){
            var arr = inputValue.split(","); 
            var selectedItems = opt.data.filter(function(i){
                return $.inArray(i.id+"" ,arr )!= -1;
            });
            
            var thisWrapper = $('.alam-input-tags-wrapper');
            var thisTagsWrapper = thisWrapper.find('.tags-wrapper'); 
            $.each(selectedItems,function(i,item){
                thisTagsWrapper.append('<span class="badge badge-'+item.class+' m-1"><span class="txt" data-id="'+item.id+'">'+item.name+'</span> <span class="btn-remove">&times;</span> </span>');
            });
        }

        function getSelectedIdsArr(){
            var ids = [];
            $('.alam-input-tags-wrapper .tags-wrapper .txt').each(function(){  
                ids.push($(this).attr('data-id'));
            }); 
            return ids;
        }
        function getSelectedIdsCSV(){
            var ids = getSelectedIdsArr(); 
            var idz = ids.join(',');
            return idz;
        }
        
        function removeAlreadySelected(obj){
            var selectedIds = getSelectedIdsArr(); 
            for (var i = obj.length - 1; i >= 0; --i) {  
                if ($.inArray(obj[i].id+"" ,selectedIds )!= -1 ) { 
                    obj.splice(i,1);
                }
            }
            return obj;
        }

        elem.bind("keydown.alamInputTags", function (e) {
             
            var thisInput = $(this);//elem[0];
            var thisWrapper = thisInput.parents('.alam-input-tags-wrapper');
            var thisTagsWrapper = thisWrapper.find('.tags-wrapper');
            var thisdrpWrapper = thisWrapper.find('.drp-wrapper');
            var thisVal = thisInput.val().trim();
            var lastBadge = thisTagsWrapper.find('.badge:last-child');
            var drpMenuItem = thisdrpWrapper.find('a.dropdown-item');  

            if(thisVal =="" && e.which==8 && lastBadge.length==1){
                lastBadge.remove();
            } 
            else if(thisVal !="" && e.which==13 && drpMenuItem.length==1){ 
                drpMenuItem.trigger('click');
            } else if(thisVal !="" && e.which==40 && drpMenuItem.length>=1){ 
                thisdrpWrapper.find('a.dropdown-item:first-child').focus();
            } 
        });

        $('body').on('keydown','.alam-input-tags-wrapper a.dropdown-item',function(e){
            //e.preventDefault();
            var menuItms = $('.alam-input-tags-wrapper a.dropdown-item');
            var indx = $(this).index();
            if(indx == menuItms.length-1){
                indx=-1;
            }
            if(e.which==40){ 
                menuItms.eq(indx+1).focus();
            }else if(e.which==38){
                menuItms.eq(indx-1).focus();
            }
        });
         
        return elem.bind("keyup.alamInputTags", function (e) {
             
            var thisInput = $(this);//elem[0];
            var thisWrapper = thisInput.parents('.alam-input-tags-wrapper');
            var thisTagsWrapper = thisWrapper.find('.tags-wrapper');
            var thisdrpWrapper = thisWrapper.find('.drp-wrapper');
            var thisVal = thisInput.val().trim();
            if(e.which==27){
                var thisWrapper = $('.alam-input-tags-wrapper'); 
                var thisdrpWrapper = thisWrapper.find('.drp-wrapper');
                thisdrpWrapper.hide();
            }
            else if(thisVal!=""){
                thisdrpWrapper.show();
                thisdrpWrapper.html('<div class="text-center"><span><i class="fa fa-spin fa-spinner"></i> Loading</span></div>');
                 
                var obj = removeAlreadySelected(opt.data);
                 
                var filteredList = obj.filter(i=> i.name.toLowerCase().includes(thisVal.toLowerCase()));

                if(filteredList.length > 0){ 
                    var str = '<div class="alam-input-tags dropdown-menu show">';
                    filteredList = filteredList.slice(0, opt.maxSuggessions);
                    $.each(filteredList,function(i,v){
                        str+='<a class="dropdown-item  border-'+v.class+'" data-class="'+v.class+'" data-id="'+v.id+'" href="#">'+v.name+'</a>';
                    });
                    str+='</div>';
                    thisdrpWrapper.html(str); 
                }else{
                    thisdrpWrapper.html('');
                    thisdrpWrapper.hide();
                }
                
            }else{
                thisdrpWrapper.hide();
            } 
        }); 
    };
}( jQuery ));