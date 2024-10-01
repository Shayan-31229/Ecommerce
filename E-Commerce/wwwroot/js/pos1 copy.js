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
                            $("#drpCards").html("<option value>--No Muncipility Card Found--</option>");
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
                                    $("#drpCards").append("<option data-status='"+r.status+"' data-fmember='"+r.familyMembersCount+"' data-cType='"+r.cardType+"' data-mId='"+r.municipalityId+"' value="+r.cardNo+">"+r.cardNo+" ["+r.status+"]"+"</option>");
                                }else{
                                    $("#drpCards").append("<option value="+r.cardNo+" disabled>"+r.cardNo+" ["+r.status+"]"+"</option>");
                                }                       
                            });

                            $("#drpCards").trigger("change");
                            $("#btnFetchQuota").removeAttr("disabled");
                            $(".tbl-quota-wrapper").html('<div class="alert alert-success text-center"> <i class="fa fa-warning"></i> Please select an active card and click Get-Quota</div>');
                        }
                        
                    }
                } catch(e) {
                    $("#drpCards").html("<option value>--ERROR (2): Contact IT...--</option>");
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
        $(".tbl-quota-wrapper").html('<div class="alert alert-success text-center"> <i class="fa fa-warning"></i> Please select an active card and click Get-Quota</div>');
        var selectedOpt = $("#drpCards").find(':selected').attr('data-id');
        $("#lblMID").html($("#drpCards").find(':selected').attr('data-mId'));                        
        $("#lblCardType").html($("#drpCards").find(':selected').attr('data-cType'));                        
        $("#lblFMember").html($("#drpCards").find(':selected').attr('data-fMember'));  
        var stts = $("#drpCards").find(':selected').attr('data-status');        
        $("#lblStts").html(stts=='Active'?'<span class="badge badge-success">Active</span>':'<span class="badge badge-danger">'+stts+'</span>');
    });
    /** On DRP Cards change clear quota endds here*/

    var getItemsAJR = null;
    $("#itemID").on("keyup",function(){
        var term = $(this).val().trim();
        if(term!=''){
            getItemsAJR = $.ajax({
                url:domain+'/api/items/'+term,
                type:'POST', 
                beforeSend:function(){
                    if(getItemsAJR!=null){
                        getItemsAJR.abort();
                    }
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
                },
                success:function(resp){
                    var obj = JSON.parse(resp);
                    if(obj.success == true){
                        $('.ac-dd').html('');
                        if(obj.result.length==0){
                            $('.ac-dd').append(`
                                <div class="alert alert-danger text-center">No Such Item Found.</div>`);
                        }else{
                            $.each(obj.result,function(i,v){
                                $('.ac-dd').append(`
                                <a href="#" class="waves-effect waves-light" data-name="`+v.itemNameE+`" data-id="`+v.itemId+`" data-price="`+v.itemPrice+`" data-vat="`+(v.hasVat=='True'?'A':'B')+`">
                                    <div>
                                        <span>`+v.itemNameE+`</span>
                                    </div>
                                    <div>
                                        <span class="float-right">`+v.itemId+`</span>
                                        <strong>AED `+v.itemPrice+`</strong>  <span>`+(v.hasVat=='True'?'A':'B')+`</span>                              
                                    </div>
                                </a>`);
                            });
                            //if scanned term equal to data-id and only one item filtered
                            if(obj.result.length==1){
                                selectOnScan(term);
                            }
                            
                        }                        
                    }
                },
                error:function(a,b){
                    $('.ac-dd').html('<div class="alert alert-danger text-center">Some Ajax Error: Contact IT Section.</alert>');
                }
            });
            $(".btn-clearFilter").removeClass("d-none");
        }else{
            $(".btn-clearFilter").addClass("d-none");
            $('.ac-dd').hide();
        }
    });


    $("#txtNationalNumber").on('keyup',function(){
        //reset every thing
        $(".drpCards").html('<option value="" class="disabled">--Filter National ID--</option>');
        $(".tbl-quota-wrapper").html('<div class="alert alert-warning text-center">Please type-in valid National_ID_Number and click Fetch-Cards button.</div>');
        $(".table-items tbody").html('');
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
         
        itemAlreadyAdded = $(".table-items tbody tr[data-id="+ItemID+"]").length;
        var chkItmQuota = checkItemQuota(ItemID);
        
        if(chkItmQuota==0){
            alert('Item not found in Quota.');
            $("#itemID").val("").focus();
            $('.ac-dd').hide();
            $(".btn-clearFilter").addClass("d-none");
            return false;
        }else if(itemAlreadyAdded>0){
            var oldQtty = $(".table-items tbody tr[data-id="+ItemID+"] .iQty").val();
            if((parseInt(oldQtty)+1) > chkItmQuota){
                alert('Max Quota limit for this item is '+ chkItmQuota);
                $("#itemID").val("").focus();
                $('.ac-dd').hide();
                $(".btn-clearFilter").addClass("d-none");
               return false;
            }
        }

        if(itemAlreadyAdded==0){
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
            var oldQty = $(".table-items tbody tr[data-id="+ItemID+"] .iQty").val();
            $(".table-items tbody tr[data-id="+ItemID+"] .iQty").val(parseInt(oldQty)+1); 

            calcSubTotal( $(".table-items tbody tr[data-id="+ItemID+"] .iQty") );
        }

        $("#itemID").val("").focus();
        $('.ac-dd').hide();
        $(".btn-clearFilter").addClass("d-none");
    });


    $('body').on('click','.ac-dd a',function(e){        
        e.preventDefault();        
        ItemID = $(this).attr('data-id');
        ItemName = $(this).attr('data-name');
        ItemPrice = $(this).attr('data-price');
        vat = $(this).attr('data-vat');
        
         
        itemAlreadyAdded = $(".table-items tbody tr[data-id="+ItemID+"]").length;
        
        var chkItmQuota = checkItemQuota(ItemID);
         
        if(chkItmQuota==0){
            alert('Item not found in Quota.');
            $("#itemID").val("").focus();
            $('.ac-dd').hide();
            $(".btn-clearFilter").addClass("d-none");
            return false;
        }else if(itemAlreadyAdded>0){
            var oldQtty = $(".table-items tbody tr[data-id="+ItemID+"] .iQty").val();
            if((parseInt(oldQtty)+1) > chkItmQuota){
                alert('Max Quota limit for this item is '+ chkItmQuota);
                $("#itemID").val("").focus();
                $('.ac-dd').hide();
                $(".btn-clearFilter").addClass("d-none");
               return false;
            }
        }

        if(itemAlreadyAdded==0){
            $(".table-items tbody").append(`<tr data-id="`+ItemID+`" data-vat="`+vat+`">
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
            var oldQty = $(".table-items tbody tr[data-id="+ItemID+"] .iQty").val();
            $(".table-items tbody tr[data-id="+ItemID+"] .iQty").val(parseInt(oldQty)+1); 

            calcSubTotal( $(".table-items tbody tr[data-id="+ItemID+"] .iQty") );
        }

        $("#itemID").val("").focus();
        $('.ac-dd').hide();
        $(".btn-clearFilter").addClass("d-none");
    });

    $("body").on("click",".btn-removeItem",function(){
        $(this).parents("tr").remove();
        calcTotal();
    });

    $('body').on('change','.iQty',function(){
        var itemId = $(this).parents('tr').attr('data-id');
        var itemQuota = checkItemQuota(itemId);
        if(itemQuota < $(this).val()){
            alert("["+$(this).parents('tr').find('.iItemName').html()+"] has quota limit "+itemQuota);
            $(this).val(itemQuota);
        }
        calcSubTotal($(this));
    });


    //ceil floor
    $('.btn-reset-total').click(function(e){
        var total = parseFloat($('.totalPayable').html());
        $('#txtPayed').val(total.toFixed(2));
        $('#txtPayed').trigger('change').focus();
    });
    $('.btn-ceil').click(function(e){
        var total = parseFloat($('#txtPayed').val());
        var ctotal = c(total);
        ctotal = (ctotal>0)?ctotal:0;
        $('#txtPayed').val(ctotal.toFixed(2));
        $('#txtPayed').trigger('change').focus();
    });
    $('.btn-floor').click(function(e){
        var total = parseFloat($('#txtPayed').val());
        var ftotal = f(total);
        ftotal = (ftotal>0)?ftotal:0;
        $('#txtPayed').val(ftotal.toFixed(2));
        $('#txtPayed').trigger('change').focus();
    });


    //calculate change
    $('#txtPayed, #txtCashGiven').on('keyup change',function(e){
        if(e.target.id=='txtPayed'){
            e.preventDefault();
            ceilFloorByKeys(e);
        }
        var total = parseFloat($('#txtPayed').val());
        var cash = parseFloat($('#txtCashGiven').val());
        var change = cash - total;
        $("#txtChange").val(change.toFixed(2));

    });

//submit order
//toastr.info('Hi! I am info message.');
    $('.btn-SubmitOrder').click(function(e){
        e.preventDefault();
        //toastr.error('Hi! I am info message.','Turtle Bay Resort', {"autohide": false,"closeButton": true,});
        var f = buildForm();
        if(f.items.length<1){
            toastr.error('No Item selected...');
            return false;
        }

        if(f.total_payable<1){
            toastr.error('Invalid payable amount...');
            return false;
        }

        var disc = 5;
        if(Math.abs(f.total_payable-f.payed)>disc){
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
                    $("#myModal .modal-body").html("<iframe src='"+domain+"/receipts/receiptpreview/"+obj.data+"' style='height:80vh;width:100%;border:0px;'></iframe>");
                    $("#myModal").modal("show");
                    toastr.error(obj.message);
                }else{
                    toastr.error(obj.message+' contact IT Section at ext# 674');
                }
            },error:function(){
                toastr.error('Ajax Error at receipt creation, contact IT Section at ext# 674');
            }
        });//end ajax


    });


});


function buildForm(){
    
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

function ceilFloorByKeys(e){
    
    if(e.target.id=='txtPayed' && e.which == 38){
        $('.btn-ceil').trigger('click');
    }
    if(e.target.id=='txtPayed' && e.which == 40){
        $('.btn-floor').trigger('click');
    }
    if(e.target.id=='txtPayed' && e.which == 27){
        $('.btn-reset-total').trigger('click');
    }
}


function checkItemQuota(itemId){
    var resp=0;
    var tr = $('.tbl-quota tbody tr[data-id="'+itemId+'"]');
    var data = JSON.parse(localStorage.getItem('Quota'));
    $.each(data, function(i, v) {
        if (v.itemId == itemId) {
            resp = v.balancePerCategory;
            return false;
        }
    });
    return resp;
}

var getUserQuotaAJR = null; 
function getUserQuota(nationalNumber, cardNumber){
    
    getUserQuotaAJR = $.ajax({
            url:domain+'/api/getQuota/'+nationalNumber+"/"+cardNumber,
            beforeSend:function(){
                if(getUserQuotaAJR != null) {
                    getUserQuotaAJR.abort();
                }
                $(".tbl-quota-wrapper").html('<div class="alert alert-info text-center"> <i class="fa fa-spinner fa-spin"></i> Featching Customer Quota. </div>');
            },success:function(resp){
                try {
                    data = JSON.parse(resp);
                    localStorage.setItem('Quota',JSON.stringify(data.result));
                    if(data.success == true){
                        fillTblQuota(data.result);
                    }
                } catch(e) {
                    alert(e.message);
                    $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> '+e.message+'<br> Call IT at ext# 674. </div>');
                }
            },error:function(){
                $(".tbl-quota-wrapper").html('<div class="alert alert-danger text-center"> <i class="fa fa-warning"></i> Error fetching Quota, Contact IT Section. </div>');
            }
        });
}

function fillTblQuota(data){
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
            console.log(data);
    $.each(data,function(i,v){
        
        tbl+=`
            <tr data-id="`+v.itemId+`" data-cat="`+v.itemCategory+`">
                <td>`+(i+1)+`</td>
                <td><a href="#" class="addItem text-primary itemId">`+v.itemId+`</a></td>
                <td><a href="#" class="addItem text-primary itemName">`+(v.itemNameE==null?v.itemNameA:v.itemNameE)+`</a></td>
                <td class="vat">`+(v.hasVat==true?'A':'B')+`</td>
                <td class="qty">`+v.balancePerCategory+`</td>
                <td class="category">`+v.itemCategory+`</td>
                <td class="price">`+v.itemPrice+`</td>
            </tr>
        `;
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
}

function selectOnScan(term){
    //if scanned term equal to data-id and only one item filtered
    if($('.ac-dd a[data-id="'+term+'"]').length==1){
        $('.ac-dd a[data-id="'+term+'"]').trigger('click');
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
    
    aNetAmt = 0;
    bNetAmt = 0;
    $(".table-items tbody tr[data-vat='A']").each(function(){
        aNetAmt += parseFloat($(this).find('.iSubTotal').html());
        totalItems += parseInt($(this).find('.iQty').val());
    });
    $(".table-items tbody tr[data-vat='B']").each(function(){
        bNetAmt += parseFloat($(this).find('.iSubTotal').html());
        totalItems += parseInt($(this).find('.iQty').val());
    });
     
    $('.itemsCount').html('<span class="tItemTypes">'+totalItemTypes+'</span>(<span class="tItems">'+totalItems+'</span>)');
    $('.aNetAmt').html(aNetAmt.toFixed(2));
    var aPercentage = parseFloat($('.aVatPercent').html());
    var aVat = aNetAmt * aPercentage  / 100;
    console.log(aVat);
    $(".aVat").html(aVat.toFixed(2));
    var aAmt = aNetAmt + aVat;
    $('.aAmt').html(aAmt.toFixed(2));

    $('.bNetAmt').html(bNetAmt.toFixed(2));
    var bPercentage = parseFloat($('.bVatPercent').html());
    var bVat = bNetAmt * bPercentage  / 100;
    $(".bVat").html(bVat.toFixed(2));
    var bAmt = bNetAmt + bVat;
    $('.bAmt').html(bAmt.toFixed(2));



    var totalPayable = (aAmt+bAmt).toFixed(2);
    $('.totalPayable').html( totalPayable  );
    $('#txtPayed').val( totalPayable );

}