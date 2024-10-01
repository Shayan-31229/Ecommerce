$(function(){
    //calcTotal();
    localStorage.setItem('Quota','[]');
    var getCardsAJR = null;

    /**Fetch Municpility cards on basis of National ID starts here */
    $("#btnFetchCards").click(function(e){
        e.preventDefault();
        
        if($("#txtNationalNumber").val().length!=15){
            $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> Invalid National Number. </div>');
            $("#drpCards").html("<option value>--Filter National ID--</option>"); 
            return false;
        }
        getCardsAJR = $.ajax({
            url:domain+'/api/CoopsGetCommodityCards/'+$("#txtNationalNumber").val(),
            beforeSend:function(){
                if(getCardsAJR!=null){
                    getCardsAJR.abort();
                }
                $("#drpCards").html("<option value>--LOADING...--</option>");  
                $("#btnFetchQuota").attr("disabled","disabled");
                $(".tbl-quota-wrapper").html('<div class="alert alert-info text-center"> <i class="fa fa-spinner fa-spin"></i> Featching Customer Details. </div>');
                $("#lblMID,#lblCardType,#lblFMember,#lblStts").html("<i class='fa fa-spinner fa-spin'></i>");
            },success:function(resp){
                try {
                    data = JSON.parse(resp); 
                    if(data.success == true){
                        if(data["result"].length == 0){
                            $("#drpCards").html("<option value>--No Abu Dhabi Municipality Card Found--</option>");
                            $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-exclamation-triangle"></i> No Card found for this National number ['+ $("#txtNationalNumber").val() +']. </div>');
                            $("#lblMID,#lblCardType,#lblFMember,#lblStts").html("");
                        }else{
                            $("#drpCards").html("");
                            // $("#lblMID").html(r.municipalityId);                        
                            // $("#lblCardType").html(r.cardType);                        
                            // $("#lblFMember").html(r.familyMembersCount);          
                            // $("#lblStts").html(r.status=='Active'?'<span class="badge badge-success">Active</span>':'<span class="badge badge-danger">'+r.status+'</span>');
                            $.each(data["result"],function(i,r){
                                
                                if(r.status == "Active"){
                                    if(r.municipalityId==1001){
                                        //$("#drpCards").append("<option value="+r.cardNo+" disabled>"+r.cardNo+" [Al-Ain Municipality card not allowed]"+"</option>");
                                    }else{
                                        $("#drpCards").append("<option data-status='"+r.status+"' data-fmember='"+r.familyMembersCount+"' data-cType='"+r.cardType+"' data-mId='"+r.municipalityId+"' value="+r.cardNo+">"+r.cardNo+" ["+r.status+"]"+"</option>");
                                    }
                                }else{
                                    $("#drpCards").append("<option value="+r.cardNo+" disabled>"+r.cardNo+" ["+r.status+"]"+"</option>");
                                }                       
                            });
                            $("#drpCards").trigger("change");
                            $("#btnFetchQuota").removeAttr("disabled");
                            if($("#drpCards option:not(:disabled)").length>=1){
                                $(".tbl-quota-wrapper").html('<div class="alert alert-info text-center"> <i class="fa fa-spinner fa-spin"></i> Featching Customer Quota. </div>');
                            }else{
                                $(".tbl-quota-wrapper").html('<div class="alert alert-success text-center"> <i class="fa fa-warning"></i> Please select an active card and click Get-Quota</div>');
                            } 

                            if($("#drpCards option").length==0){
                                $("#drpCards").html("<option value>--No Abu Dhabi Municipality Card Found--</option>");
                                $("#lblMID,#lblCardType,#lblFMember,#lblStts").html("-");
                            }
                        }
                        
                    }else{
                        $("#drpCards").html("<option value>--No Muncipility Card Found--</option>");
                        $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-exclamation-triangle"></i> No municipality card found for this National number ['+ $("#txtNationalNumber").val() +'].<br>MSG from ADM: '+data.error.message+' </div>');
                        $("#lblMID,#lblCardType,#lblFMember,#lblStts").html("");
                    }
                } catch(e) {
                    $("#drpCards").html("<option value>--Municipality server not reachable...--</option>");
                    $("#btnFetchQuota").removeAttr("disabled");
                    $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> Invalid Customer.<br>'+e.message+'<br>Call IT at ext# 674</div>');
                }
            },error:function(e, x, settings, exception) {
                $("#drpCards").html("<option value>--ERROR (1): Contact IT...--</option>");
                $("#btnFetchQuota").removeAttr("disabled");                                  
                    var message;
                    var statusErrorMap = {
                        '400' : "Server understood the request, but request content was invalid.",
                        '401' : "Unauthorized access.",
                        '403' : "Forbidden resource can't be accessed.",
                        '500' : "Internal server error.",
                        '503' : "Service unavailable."
                    };
                    if (x.status) {
                        message =statusErrorMap[x.status];
                                if(!message){
                                        message="Unknown Error <br>.";
                                    }
                    }else if(exception=='parsererror'){
                        message="Error.<br>Parsing JSON Request failed.";
                    }else if(exception=='timeout'){
                        message="Request Time out.";
                    }else if(exception=='abort'){
                        message="Request was aborted by the server";
                    }else {
                        message="Unknown Error <br>.";
                    }
                    $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> '+message+'</div>');
                }
        });
        return false;
    });
    /**Fetch Municpility cards on basis of National ID ends here */


    /**Fetch Quota on basis of National ID  and Municpility cards starts here */
    $("#btnFetchQuota").click(function(e){
        e.preventDefault();
        var nid = $("#txtNationalNumber").val();
        var cid = $("#drpCards").val();
        if(nid.length!=15){
            $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> Invalid National Number. </div>');
            return false;
        }
        if(cid==""){
            $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> No Active Muncipility card is selected. </div>');
            return false;
        }
        getUserQuota(nid,cid);
        return false;
    });
   /**Fetch Quota on basis of National ID  and Municpility cards ends here */

    /** On DRP Cards change clear quota starts here*/
    $("#drpCards").change(function(){
        localStorage.setItem('Quota','[]');
        $(".table-items tbody").html('');
        calcTotal();
        //$(".tbl-quota-wrapper").html('<div class="alert alert-success text-center"> <i class="fa fa-warning"></i> Please select an active card and click Get-Quota</div>');
        $(".tbl-quota-wrapper").html('<div class="alert alert-info text-center"> <i class="fa fa-spinner fa-spin"></i> Featching Customer Quota. </div>');
        var selectedOpt = $("#drpCards").find(':selected').attr('data-id');
        $("#lblMID").html($("#drpCards").find(':selected').attr('data-mId'));                        
        $("#lblCardType").html($("#drpCards").find(':selected').attr('data-cType'));                        
        $("#lblFMember").html($("#drpCards").find(':selected').attr('data-fMember'));  
        var stts = $("#drpCards").find(':selected').attr('data-status');        
        $("#lblStts").html(stts=='Active'?'<span class="badge badge-success">Active</span>':'<span class="badge badge-danger">'+stts+'</span>');
        $('#txtCashGiven').val('0');
        $('#txtChange').val('0');
        $('#btnFetchQuota').trigger('click');
    });
    /** On DRP Cards change clear quota endds here*/

    
    $("#itemID").on("keyup",function(){
        
        var term = $(this).val().trim().toLowerCase();
        
        if(term!=''){
            $('.ac-dd').html(`<div class='text-center py-3'>
                <div class="preloader-wrapper active">
                    <div class="spinner-layer spinner-yellow-only">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div>
                        <div class="gap-patch">
                            <div class="circle"></div>
                        </div>
                        <div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>
                </div>
                <p>Fetching Items</p>
            </div>`);
            $('.ac-dd').show(); 
            var obj = JSON.parse(localStorage.getItem('Quota'));
            var mapped_items = JSON.parse(localStorage.getItem('mapped_items'));
            if(obj.length==0){
                toastr.error("Please fetch quota for a person.");
                $('.ac-dd').html('').hide(); 
                return false;
            }
            
            var o = obj.filter(function (n,i){
                
                if( mapped_items.hasOwnProperty(n.itemId) &&
                    (n.itemNameE.toLowerCase().includes(term) || 
                    n.itemNameA.toLowerCase().includes(term)  ||
                    (n.itemId+'').toLowerCase().includes(term)  ||
                    (n.nav_barcode+'').toLowerCase().includes(term)  ||
                    (n.itemPrice+'').toLowerCase().includes(term))
                    ){
                    return true
                }else{
                    return false;
                }
            });
            $('.ac-dd').html('');
            $.each(o,function(i,v){
                
                $('.ac-dd').append(`
                <a href="#" class="waves-effect waves-light" data-name="`+v.itemNameE+`" data-id="`+v.itemId+`" data-barcode="`+(v.nav_barcode.trim())+`" data-cat="`+v.itemCategory+`" data-price="`+v.itemPrice+`" data-vat="`+(v.hasVat==true?'A':'B')+`">
                    <div>
                        <span>`+v.itemNameE+`</span>
                    </div>
                    <div>
                        <span class="float-right">`+v.itemId+`</span>
                        <strong>AED `+v.itemPrice+`</strong>  <span>`+(v.hasVat==true?'A':'B')+`</span>                              
                    </div>
                </a>`);
            });
            if(o.length==0){
                $('.ac-dd').append(`<div class="alert alert-danger text-center">No such item found</div>`);
            }
            if(o.length==1){
                selectOnScan(term);
            }
            $(".btn-clearFilter").removeClass("d-none");
        }else{
            $(".btn-clearFilter").addClass("d-none");
            $('.ac-dd').html("").hide();
        }
    });


    $("#txtNationalNumber").on('keyup',function(){
        //reset every thing
        $("#drpCards").html('<option value="" class="disabled">--Filter National ID--</option>');
        $(".tbl-quota-wrapper").html('<div class="alert alert-warning text-center">Please type-in valid National_ID_Number and click Fetch-Cards button.</div>');
        $(".table-items tbody").html('');
        $('#txtCashGiven').val('0');
        $('#txtChange').val('0');
        localStorage.setItem('Quota','[]');
        calcTotal();
    });

    $(".btn-clearFilter").click(function(e){
        e.preventDefault();        
        $(".btn-clearFilter").addClass("d-none");
        $("#itemID").val("").focus();
        $('.ac-dd').hide();
    });

    $('body').on('click','.tbl-quota tbody a.addItem',function(e){
        e.preventDefault();
        ItemID = $(this).parents("tr").find('.itemId').html();
        ItemName = $(this).parents("tr").find('.itemName').html();
        ItemPrice = $(this).parents("tr").find('.price').html();
        vat = $(this).parents("tr").find('.vat').html();
        cat = $(this).parents("tr").find('.category').html();
         
        catAlreadyAdded = $(".table-items tbody tr[data-cat='"+cat+"']").length;
        itemAlreadyAdded = $(".table-items tbody tr[data-id='"+ItemID+"']").length;
        availedCatQuota = 0;//
        $(".table-items tbody tr[data-cat='"+cat+"'] .iQty").each(function(){
            availedCatQuota += parseInt($(this).val());
        });
        
        var chkItmQuota = checkItemQuota(cat);
        if(chkItmQuota==0){
            toastr.error('Item not found in Quota.');
            $("#itemID").val("").focus();
            $('.ac-dd').hide();
            $(".btn-clearFilter").addClass("d-none");
            return false;
        }else{
            if(availedCatQuota>=chkItmQuota){
                toastr.error('Max Quota limit for this itemCategory is '+ chkItmQuota);
            }else if(itemAlreadyAdded==0){
                $(".table-items tbody").append(`<tr data-id="`+ItemID+`" data-cat="`+cat+`" data-vat="`+vat+`">
                            <td class="iItemName minlheight py-0 align-middle">`+ItemName+`</td>
                            <td class="iPrice py-0 align-middle">`+ItemPrice+`</td>
                            <td class="iItemName py-0 align-middle"><input type="number" min="1" max="999" value="1" class="iQty txtFluid border border-light"></td>
                            <td class="iSubTotal py-0 align-middle">`+ (ItemPrice * 1).toFixed(2) +`</td>
                            <td class="vat py-0 align-middle">`+vat+`</td>
                            <td class="iRemove py-0 align-middle"><span class="btn btn-sm btn-danger px-2 btn-removeItem waves-effect waves-light m-0"><i class="fa fa-trash"></i></span></td>
                        </tr>`);
                calcSubTotal( $(".table-items tbody tr:last-child iQty"));
    
                $(".tbl-wrapper").animate({
                    scrollTop: $('.tbl-wrapper')[0].scrollHeight - $('.tbl-wrapper')[0].clientHeight
                  }, 500);
    
            }else{
                var oldQty = $(".table-items tbody tr[data-id='"+ItemID+"'] .iQty").val();
                $(".table-items tbody tr[data-id='"+ItemID+"'] .iQty").val(parseInt(oldQty)+1); 
    
                calcSubTotal( $(".table-items tbody tr[data-id='"+ItemID+"'] .iQty") );
            }
        } 
        $("#itemID").val("");//.focus();
        $('.ac-dd').hide();
        $(".btn-clearFilter").addClass("d-none");
        return false;
    });

    //autocomplete click
    $('body').on('click','.ac-dd a',function(e){        
        e.preventDefault();        
        ItemID      = $(this).attr('data-id');
        ItemName    = $(this).attr('data-name');
        ItemPrice   = $(this).attr('data-price');
        vat         = $(this).attr('data-vat');
        cat         = $(this).attr('data-cat');
        
         
        catAlreadyAdded = $(".table-items tbody tr[data-cat='"+cat+"']").length;
        itemAlreadyAdded = $(".table-items tbody tr[data-id='"+ItemID+"']").length;
        availedCatQuota = 0;//
        
        $(".table-items tbody tr[data-cat='"+cat+"'] .iQty").each(function(){
            availedCatQuota += parseInt($(this).val());
        });
        var chkItmQuota = checkItemQuota(cat);
        if(chkItmQuota==0){
            toastr.error('Item not found in Quota.');
            $("#itemID").val("").focus();
            $('.ac-dd').hide();
            $(".btn-clearFilter").addClass("d-none");
            return false;
        }else{
            if(availedCatQuota>=chkItmQuota){
                toastr.error('Max Quota limit for this itemCategory is '+ chkItmQuota);
            }else if(itemAlreadyAdded==0){
                $(".table-items tbody").append(`<tr data-id="`+ItemID+`" data-cat="`+cat+`" data-vat="`+vat+`">
                            <td class="iItemName minlheight py-0 align-middle">`+ItemName+`</td>
                            <td class="iPrice py-0 align-middle">`+ItemPrice+`</td>
                            <td class="iItemName py-0 align-middle"><input type="number" min="1" max="999" value="1" class="iQty txtFluid border border-light"></td>
                            <td class="iSubTotal py-0 align-middle">`+ (ItemPrice * 1).toFixed(2) +`</td>
                            <td class="vat py-0 align-middle">`+vat+`</td>
                            <td class="iRemove py-0 align-middle"><span class="btn btn-sm btn-danger px-2 btn-removeItem waves-effect waves-light m-0"><i class="fa fa-trash"></i></span></td>
                        </tr>`);
                calcSubTotal( $(".table-items tbody tr:last-child iQty"));
    
                $(".tbl-wrapper").animate({
                    scrollTop: $('.tbl-wrapper')[0].scrollHeight - $('.tbl-wrapper')[0].clientHeight
                  }, 500);
    
            }else{
                var oldQty = $(".table-items tbody tr[data-id='"+ItemID+"'] .iQty").val();
                $(".table-items tbody tr[data-id='"+ItemID+"'] .iQty").val(parseInt(oldQty)+1); 
    
                calcSubTotal( $(".table-items tbody tr[data-id='"+ItemID+"'] .iQty") );
            }
        } 

        $("#itemID").val("").focus();
        $('.ac-dd').hide();
        $(".btn-clearFilter").addClass("d-none");
    });

    $("body").on("click",".btn-removeItem",function(){
        $(this).parents("tr").remove();
        calcTotal();
    });


    $('body').on('keydown','.iQty',function(e){
        console.log(e.which);
        if(e.which!=38 && e.which!=40){
            return false;
        }
    });
    $('body').on('change','.iQty',function(){
        $(this).attr('readonly','readonly');
        var cat = $(this).parents('tr').attr('data-cat');
         
        var ttlItems = 0; 
        $(this).parents('tbody').find('tr[data-cat="'+cat+'"]').each(function(){
            ttlItems += parseInt($(this).find('.iQty').val());
            
        });
         
        var itemQuota = checkItemQuota(cat);
        console.log('q:'+itemQuota); 
        console.log('t:'+ttlItems); 
        if(itemQuota < ttlItems){
            toastr.error("You have already selected quot limit for items in category: "+cat);
            var otherItemsSelectedinThisCat =  ttlItems - $(this).val();
            var validValForThisItem = itemQuota - otherItemsSelectedinThisCat;
            $(this).val(validValForThisItem);
        }
        calcSubTotal($(this));
        $(this).removeAttr('readonly');
    });


    //ceil floor
    // $('.btn-reset-total').click(function(e){
    //     var total = parseFloat($('.totalPayable').html());
    //     $('#txtPayed').val(total.toFixed(2));
    //     $('#txtPayed').trigger('change').focus();
    // });
    // $('.btn-ceil').click(function(e){
    //     var total = parseFloat($('#txtPayed').val());
    //     var ctotal = c(total);
    //     ctotal = (ctotal>0)?ctotal:0;
    //     $('#txtPayed').val(ctotal.toFixed(2));
    //     $('#txtPayed').trigger('change').focus();
    // });
    // $('.btn-floor').click(function(e){
    //     var total = parseFloat($('#txtPayed').val());
    //     var ftotal = f(total);
    //     ftotal = (ftotal>0)?ftotal:0;
    //     $('#txtPayed').val(ftotal.toFixed(2));
    //     $('#txtPayed').trigger('change').focus();
    // });


    //calculate change
    $('#txtPayed, #txtCashGiven').on('keyup change',function(e){
        if(e.target.id=='txtPayed'){
            e.preventDefault();
            //ceilFloorByKeys(e);
        }
        var total = parseFloat($('#txtPayed').val());
        var cash = parseFloat($('#txtCashGiven').val());
        var change = cash - total;
        
        $("#txtChange").val(change.toFixed(2));
    });

//submit order
//toastr.info('Hi! I am info message.');
    $('.btn-PayByCard').click(function(e){
        e.preventDefault();
        $('#ccModal').modal('show');
    });

    $('.btn-SubmitOrder').click(function(e){
        e.preventDefault(); 
        $('.btn-SubmitOrder').prop("disabled",true);

        var paymentMethod = $(this).attr('data-payment-method');
        if(paymentMethod=='card'){
            last4digits = $('#txtCCLast4Digits').val();
            if(last4digits.length<4){
                $('.btn-SubmitOrder').prop("disabled",false);
                toastr.error('Please enter last 4 digits of credit card');
                return false;
            }
            ccExpiryMonth = $('#txtCCExpiryMonth').val();
            if(ccExpiryMonth.length==0){
                $('.btn-SubmitOrder').prop("disabled",false);
                toastr.error('Please select expiry month');
                return false;
            }
            ccExpiryYear = $('#txtCCExpiryYear').val();
            if(ccExpiryYear.length==0){
                $('.btn-SubmitOrder').prop("disabled",false);
                toastr.error('Please select expiry year');
                return false;
            }
            ccExpiry = ccExpiryMonth+'/'+ccExpiryYear;
            $('#ccModal').modal('hide');
        }
        
        var f = buildForm(paymentMethod);
        if(f.items.length<1){
            $('.btn-SubmitOrder').prop("disabled",false);
            toastr.error('No Item selected...');
            return false;
        }

        if(f.total_payable<1){
            $('.btn-SubmitOrder').prop("disabled",false);
            toastr.error('Invalid payable amount...');
            return false;
        }

        var disc = 5;
        if(Math.abs(f.total_payable-f.payed)>disc){
            $('.btn-SubmitOrder').prop("disabled",false);
            toastr.error('Too much discount, max allowed AED: '+disc);
            return false;
        }

        var postReq = null;
        postReq = $.ajax({
            url:domain+'/receipts/createReceipt/',
            method:'post',
            data:f,
            beforeSend:function(){
                if(postReq != null) {
                    postReq.abort();
                }
            },success:function(resp){
                var obj = JSON.parse(resp);
                if(obj.status==1){ 
                    $('#drpCards').trigger('change');//rset quota and clear selected items                  
                    //$('#btnFetchQuota').trigger('click'); //it already triggers on drp change

                    $("#myModal .modal-body").html("<iframe src='"+domain+"/receipts/receiptpreview/"+obj.data+"' style='height:80vh;width:100%;border:0px;'></iframe>");
                    $("#myModal").modal("show");
                    toastr.success(obj.message);

                    //post to ADM
                    var rp = registerPurchase(obj.data);

                    //save in NAV
                    var sn = saveInNav(obj.data);
                    
                }else{
                    toastr.error(obj.message+' contact IT Section at ext# 674');
                }
                $('.btn-SubmitOrder').prop("disabled",false);
            },error:function(){
                $('.btn-SubmitOrder').prop("disabled",false);
                toastr.error('Ajax Error at receipt creation, contact IT Section at ext# 674');
            }
        });//end ajax

    });

    $('.btn-removeAllItems').click(function(){
        $('.btn-removeItem').each(function(){
            $(this).trigger('click');
        });
        if($('.btn-removeItem').length==0){
            $('#txtCashGiven').val('0');
            $('#txtChange').val('0');
        }
    });


    //eid card reader
    $('.btn-readCard').click(function(){
        //reset every thing
        $("#drpCards").html('<option value="" class="disabled">--Filter National ID--</option>');
        $(".tbl-quota-wrapper").html('<div class="alert alert-warning text-center">Please type-in valid National_ID_Number and click Fetch-Cards button.</div>');
        $(".table-items tbody").html('');
        $('#txtCashGiven').val('0');
        $('#txtChange').val('0');
        localStorage.setItem('Quota','[]');
        calcTotal();

        //scan card
        $('#txtNationalNumber').val('Scanning card...');
        $('.eid-wrapper').addClass('loading');        
        readeid();
        
        setTimeout(function () {
            //$('.simulateEidResponse').html("Could not read card.");
        }, 1000);
    });

    $('.simulateEidResponse').click(function(){
        var response = $(this).html();
        try {
            //console.log(msg.response);
            if(response=="NO DATA"){
                throw "Could not read card.";
            }
            var json = $.parseJSON(response);
            //console.log(json);
            if(json.Error!=""){
				throw json.Error;
			} 
            $(json).each(function (i, val) {
                $.each(val, function (key, value) {
                    if (key == 'EIDNumber'){                        
                        $('.eid-wrapper').removeClass('loading');
                        $("#txtNationalNumber").val(value);
                        toastr.success('Card scanned successfully.');
                        //overwrite clipboard with id
                        clearClipboard("no data");
                        //$('#btnFetchCards').trigger('click');
                    }                        
                });
            });
        }
        catch (e) {
            toastr.error(e);
            $("#txtNationalNumber").val('');
            $('.eid-wrapper').removeClass('loading');
        }
    });

    
});

function clearClipboard(text) {
    var textarea = document.createElement("textarea");
    textarea.textContent = text;
    textarea.style.position = "fixed";
    document.body.appendChild(textarea);
    textarea.select();
    try {
        return document.execCommand("cut");
    } catch (ex) {
        console.warn("Copy to clipboard failed.", ex);
        return false;
    } finally {
        document.body.removeChild(textarea);
    }
}

function readeid() {
    var event = document.createEvent('Event');
    event.initEvent('EID_EVENT');
    document.dispatchEvent(event);
}

function buildForm(paymentMethod){
    var last4digits = '';
    var ccExpiry = '';
    if(paymentMethod=='card'){
        last4digits = $('#txtCCLast4Digits').val(),
        ccExpiry = $('#txtCCExpiryMonth').val()+''+$('#txtCCExpiryYear').val();
    }
    var f = {
        em_id           : $('#txtNationalNumber').val(),
        store_id        : $('#drpStore').val(),
        card_id         : $('#drpCards').val(),
        total_items     : $('.tItems').html(),
        total_item_types: $('.tItemTypes').html(),
        a_vat_percent   : $('.aVatPercent').html(),
        a_net_amt       : $('.aNetAmt').html(),
        a_vat           : $('.aVat').html(),
        a_amt           : $('.aAmt').html(),
        b_vat_percent   : $('.bVatPercent').html(),
        b_net_amt       : $('.bNetAmt').html(),
        b_vat           : $('.bVat').html(),
        b_amt           : $('.bAmt').html(),
        total_payable   : $('.totalPayable').html(),
        payed           : $('#txtPayed').val(),
        chash           : $('#txtCashGiven').val(),
        change_aed      : $('#txtChange').val(),
        
        payment_method  : paymentMethod,
        cc_last4digits  : last4digits,
        cc_expiry       : ccExpiry,
    };
    f.items = prepareReceiptItems();
    return f;
}

function prepareReceiptItems(){
    var items = [];
    $('.table-items tbody tr').each(function(){
        var item = {
            item_id : $(this).attr('data-id'),
            price   : $(this).find('.iPrice').html(),
            qty     : $(this).find('.iQty').val(),
            subtotal: $(this).find('.iSubTotal').html(),
            vat     : $(this).find('.vat').html(),
        };
        items.push(item);
    });
    return items;
}


function c(v){
    var t = 0.25;
    var r = Math.ceil((v+0.01) / t) * t;
    return r;
}

function f(v){
    var t = 0.25;
    var r =Math.floor((v-0.01) / t) * t; 
    return r;
}

// function ceilFloorByKeys(e){
    
//     if(e.target.id=='txtPayed' && e.which == 38){
//         $('.btn-ceil').trigger('click');
//     }
//     if(e.target.id=='txtPayed' && e.which == 40){
//         $('.btn-floor').trigger('click');
//     }
//     if(e.target.id=='txtPayed' && e.which == 27){
//         $('.btn-reset-total').trigger('click');
//     }
// }


function checkItemQuota(cat){
    var resp=0;
    var data = JSON.parse(localStorage.getItem('Quota'));
    $.each(data, function(i, v) {
        if (v.itemCategory == cat) {
            resp = v.balancePerCategory;            
            return false;
        }
    });
    
    return resp;
}

var getUserQuotaAJR = null; 
function getUserQuota(nationalNumber, cardNumber){
    if(cardNumber==null){
        toastr.error("Please select an active municipality card");
        return false;
    }
    getUserQuotaAJR = $.ajax({
            url:domain+'/api/getQuota/'+nationalNumber+"/"+cardNumber,
            beforeSend:function(){
                if(getUserQuotaAJR != null) {
                    getUserQuotaAJR.abort();
                }
                $(".tbl-quota-wrapper").html('<div class="alert alert-info text-center"> <i class="fa fa-spinner fa-spin"></i> Featching Customer Quota. </div>');
            },success:function(resp){ 
                if(resp.includes("ERROR")){
                    $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i>'+resp+'</div>');
                }else{
                    try {
                        data = JSON.parse(resp);
                        localStorage.setItem('Quota',JSON.stringify(data.result));
                        if(data.success == true){
                            fillTblQuota(data.result);
                        }
                    } catch(e) {
                        toastr.error(e.message);
                        $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> '+e.message+'<br> Call IT at ext# 674. </div>');
                    }
                }
            },error:function(){
                $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> Error fetching Quota, Contact IT Section. </div>');
            }
        });
}

function fillTblQuota(data){
    mappedItemsAjax = null;
    $.ajax({
        url:domain+'/items/mapped_items',
        success:function(resp){
            localStorage.setItem('mapped_items',resp);
            var mappedItems = JSON.parse(resp);
            var tbl = `<table class="table tbl-quota table-striped table-condensed table-hover">
                    <thead class=" bg-primary">
                        <tr>
                            <th>No</th>
                            <th>Item ID</th>
                            <th>Item Name</th>
                            <th>Vat</th>
                            <th>Qty</th>
                            <th>Cat</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>`;
                    
            $.each(data,function(i,v){
                if(mappedItems.hasOwnProperty(v.itemId)){
                    tbl+=`
                        <tr data-id="`+v.itemId+`" data-cat="`+v.itemCategory+`">
                            <td>`+(i+1)+`</td>
                            <td><a href="#" class="addItem text-primary itemId">`+v.itemId+`</a></td>
                            <td><a href="#" class="addItem text-primary itemName">`+(v.itemNameE==null?v.itemNameA:v.itemNameE)+`</a></td>
                            <td class="vat">`+(v.hasVat==true?'A':'B')+`</td>
                            <td class="qty">`+v.balancePerCategory+`</td>
                            <td class="category">`+v.itemCategory+`</td>
                            <td class="price">`+v.itemPrice+`</td>
                        </tr>`;
                }
            });
            
            tbl+=`</tbody>
                </table>`;
            $(".tbl-quota-wrapper").html(tbl);

            var dom = '<"row reduced-margin"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 text-right"f>>'+
                '<"row"<"col-sm-12"t>>'+
                '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>';
            $('.tbl-quota-wrapper .tbl-quota').dataTable({
                "dom":dom,
                "drawCallback": function (e) {
                    $(".dataTables_wrapper").removeClass('form-inline');
                    $('.dataTables_paginate > .pagination li').addClass('page-item');
                    $('.dataTables_paginate > .pagination li a').addClass('page-link');
                    $('.dataTables_length label select').css({'display':'inline-block !important'});
                },
                // columnDefs: [
                //     { orderable: false, targets: 0 }
                // ]
            });
        },error:function(){
            toastr.error("Could not get Mapped_items, Contact ADCS IT at 674");
        }
    });
    
}

function selectOnScan(term){
    //if scanned term equal to data-id and only one item filtered
    if($('.ac-dd a[data-barcode="'+term+'"]').length==1){
        $('.ac-dd a[data-barcode="'+term+'"]').trigger('click');
    }
}

function calcSubTotal(iqty){
    var price = iqty.parents('tr').find('.iPrice').html();
    var qty = iqty.val();
    var subTotal = price * qty;
    iqty.parents('tr').find('.iSubTotal').html(subTotal.toFixed(2));

    calcTotal();
}

function calcTotal(){
    var total = 0.0;
    var totalItemTypes = $(".table-items tbody tr").length;
    var totalItems = 0;
    
    aAmt = 0;
    bNetAmt = 0;
    $(".table-items tbody tr[data-vat='A']").each(function(){
        aAmt += parseFloat($(this).find('.iSubTotal').html());
        totalItems += parseInt($(this).find('.iQty').val());
    });
    $(".table-items tbody tr[data-vat='B']").each(function(){
        bNetAmt += parseFloat($(this).find('.iSubTotal').html());
        totalItems += parseInt($(this).find('.iQty').val());
    });
     
    $('.itemsCount').html('<span class="tItemTypes">'+totalItemTypes+'</span>(<span class="tItems">'+totalItems+'</span>)');
    //$('.aNetAmt').html(aNetAmt.toFixed(2));
    
    //var aPercentage = parseFloat($('.aVatPercent').html());
    var aVat = aAmt-(aAmt / 1.05);
    
    $(".aVat").html(aVat.toFixed(2));
    //var aAmt = aNetAmt + aVat;
    var aNetAmt = aAmt / 1.05;
    //$('.aAmt').html(aAmt.toFixed(2));
    $('.aAmt').html(aAmt.toFixed(2));
    $('.aNetAmt').html(aNetAmt.toFixed(2));

    $('.bNetAmt').html(bNetAmt.toFixed(2));
    var bPercentage = parseFloat($('.bVatPercent').html());
    var bVat = bNetAmt * bPercentage  / 100;
    $(".bVat").html(bVat.toFixed(2));
    var bAmt = bNetAmt + bVat;
    $('.bAmt').html(bAmt.toFixed(2));



    //var totalPayable = (aAmt+bAmt).toFixed(2);//vat already included
    var totalPayable = (aAmt+bAmt).toFixed(2);
    $('.totalPayable').html( totalPayable  );
    $('#txtPayed').val( totalPayable );

    var cash =  parseFloat($('#txtCashGiven').val());
    var change = '';
    if(cash>0){
        change = (cash-totalPayable);
    }
    $('#txtChange').val((change*1).toFixed(2));
}

purchaseAjax = null;
function registerPurchase($receipt_id){
    purchaseAjax = $.ajax({
        url:domain+'/api/registerPurchase/'+$receipt_id,
        //data:{receipt_id:$receipt_id},
        beforeSend:function(){
            if(purchaseAjax != null) {
                purchaseAjax.abort();
            }
        },success:function(resp){    
            var data = JSON.parse(resp);        
            if(data.hasOwnProperty("success") && data.success == true){
                toastr.success("Successfully posted to Municipality");    
                //return 1;
            }else{
                //toastr.error("Failed to post to Municipality");
                //return "ERROR: failed to post the order to Municipality";
            }
        },error:function(){
            toastr.error("Error in posting to Municipality");
            //return "ERROR: connecting Municipality system";
        }
    });
}

updAjax = null;
function updateStatus($receipt_id){
    navAjax = $.ajax({
        url:domain+'/receipts/updatestatus/'+$receipt_id,
        method:'post',
        //data:{receipt_id:$receipt_id},
        beforeSend:function(){
            if(updAjax != null) {
                updAjax.abort();
            }
        },success:function(resp){    
            var data = JSON.parse(resp);        
            if(data.success == true){
               return 1;                    
            }else{
                return "Faild to update status";
            }
        },error:function(){
            return "ERROR while updfating status";
        }
    });
}

navAjax = null;
function saveInNav($receipt_id){
    navAjax = $.ajax({
        url:domain+'/nav/createSalesInvoice/'+$receipt_id,
        //data:{receipt_id:$receipt_id},
        beforeSend:function(){
            if(navAjax != null) {
                navAjax.abort();
            }
        },success:function(resp){    
            var data = JSON.parse(resp);        
            if(data.success == true){
                toastr.success("Successfully posted to NAV");                    
            }else{
                //toastr.error("Failed to post to NAV");
            }
        },error:function(){
            toastr.error("Error in posting to NAV");
        }
    });
}

window.addEventListener("afterprint", function(e){
    console.log('called');
    $("#myModal").modal('hide');
});
 