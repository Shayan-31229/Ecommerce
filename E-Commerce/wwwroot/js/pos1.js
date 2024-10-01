var walkincustomer = {"id":1,"customer_name":"Walkin","mobile":"-","email":"-","national_id_no":"-","loyalty_card_no":"-","store_id":null,"img":null,"status":1};
var customer = {};
var order = {};
var holdedOrders = [];
var paymentlines = [];
var store_id = null;
var fullscreen = 0;
$(function(){

    if(Object.keys(customer).length === 0 && getData('customer')!=null){
        customer = getData('customer');
    }else if(Object.keys(customer).length === 0 && getData('customer')==null){
        customer = walkincustomer;
    }

    var storedOrder = getData('order');
    order = storedOrder!=null?storedOrder:{};
    renderOrder();

    var storedHoldedOrders = getData('holdedOrders');
    holdedOrders = storedHoldedOrders!=null?storedHoldedOrders:[];
    renderHoldedOrders();

    var storedStore_id = getData('store_id');
    store_id = storedStore_id!=null?storedStore_id:$('#drpStore').val();
    $('#drpStore').val(store_id);

    var storedFS = getData('fullscreen');
    fullscreen = storedFS!=null?storedFS:0;
    if(fullscreen==1){  
        setTimeout(function () {
            var elem = document.body;
            if (elem.requestFullScreen) {
                elem.requestFullScreen();
            } else if (elem.mozRequestFullScreen) {
                elem.mozRequestFullScreen();
            } else if (elem.webkitRequestFullScreen) {
                elem.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
            } else if (elem.msRequestFullscreen) {
                elem.msRequestFullscreen();
            } 
        }, 1000); 
    } 

    //load items
    loaditems();


    //change store
    $('#drpStore').change(function(e){
        e.preventDefault();
        if(Object.keys(order).length !== 0){
            $('#drpStore').val(store_id);
            toastr.error(__('To switch store cart must be cleared.'));
        }else{
            paymentlines = [];
            saveData('paymentlines',paymentlines);
            renderPaymentLines();
            store_id = $('#drpStore').val();
            saveData('store_id',store_id);

            loaditems();
        }
    });

    //submit payment starts here
    ajxSubmitPayemnt = null;
    $('.btnSubmitPayment').click(function(e){
        e.preventDefault();
         
        ajxSubmitPayemnt = $.ajax({
            url:domainWithLang+'pos/submitpayment',
            type:'post',
            data:{store_id:store_id,order:order,customer:customer,paymentlines:paymentlines},
            beforeSend:function(){
                if(ajxSubmitPayemnt!=null){
                    ajxSubmitPayemnt.abort();
                }
                $('.btnSubmitPayment').hide();
                $('.btnSubmitPayment').parents('.card-footer').append('<div class="msgbox"></div>');
                $('.msgbox').html('<i class="fa fa-spin fa-spinner fa-2x text-info"></i>');
            },success:function(resp){
                try {
                    var obj = JSON.parse(resp);
                    if(obj.status==1){

                        $("#myModal .modal-header").hide();
                        $("#myModal .modal-footer").hide();
                        $("#myModal .modal-body").html("<iframe src='"+domainWithLang+"pos/receipt/"+obj.data+"' style='height:80vh;width:100%;border:0px;'></iframe>");
                        $("#myModal").modal("show");


                        customer = walkincustomer;
                        order = {};
                        paymentlines = [];

                        saveData('customer',customer);
                        saveData('order',order);
                        saveData('paymentlines',paymentlines);

                        renderSelectedCustomer();
                        renderOrder();
                        renderPaymentLines();

                        //show receipt iframe in modal
                        $('.msgbox').remove();
                        $('#cart-tab-md').click();
  
                    }else{
                        $('.msgbox').html('<div class="alert alert-danger text-center">'+obj.message+'</div>');
                    }
                } catch (e) {
                    $('.msgbox').html('<div class="alert alert-danger text-center">It seem you are logged out.</div>');
                }
            },
            error:function(a,b){
                $('.msgbox').html('<div class="alert alert-danger text-center">Opps! got ajax error, please contact administrator.</div>');
            }
        });
    });
    //submit payment ends here

    //payment lines starts here
    var storedPaymentlines = getData('paymentlines');
    paymentlines = storedPaymentlines!=null?storedPaymentlines:[];
    renderPaymentLines();

    $('.btnCashLine').click(function(e){
        e.preventDefault();
        var amt = $('.txtCashLine').val();
        if(amt<1){
            toastr.error('Invalid amount');
            return false;
        } 
        if(hasPaymentMethod('cash')){
            toastr.error('Cash line already added');
            return false;
        }
        paymentlines.push({
            'payment_method' : 'cash',
            'amount' : amt,
        });
        saveData('paymentlines',paymentlines);
        $('.txtCashLine').val('');
        renderPaymentLines();
    });

    $('.btnCardLine').click(function(e){
        e.preventDefault();
        var amt = $('.txtCardAmount').val();
        var last4digits = $('.txtCardLastDigits').val();
        var expirydate = $('.txtCardExpiry').val();
        if(amt<1){
            toastr.error('Invalid amount');
            return false;
        } 
        if(hasPaymentMethod('card')){
            toastr.error('card line already added');
            return false;
        }
        paymentlines.push({
            'payment_method' : 'card',
            'amount' : amt,
            'description1' : last4digits,
            'description2' : expirydate,
        });
        saveData('paymentlines',paymentlines);
        $('.txtCardAmount, .txtCardLastDigits, .txtCardExpiry').val('');
        renderPaymentLines();
    });

    $('.btnPointsLine').click(function(e){
        e.preventDefault();
        var amt = $('.txtRedeemAmount').val();
        var totalPoints = $('.txtTotalPoints').val();
        var redeemPoints = $('.txtRedeemPoints').val();
        if(amt<1){
            toastr.error('Invalid amount');
            return false;
        } 
        if(hasPaymentMethod('points')){
            toastr.error('points line already added');
            return false;
        }
        paymentlines.push({
            'payment_method' : 'points',
            'amount' : amt,
            'description1' : totalPoints,
            'description2' : redeemPoints,
        });
        saveData('paymentlines',paymentlines);
        $('.txtRedeemAmount, .txtRedeemPoints').val('');
        // var remainingPoints = parseInt(totalPoints) - parseInt(redeemPoints);
        // customer.total_points = remainingPoints;
        renderSelectedCustomer();
        // $('.txtTotalPoints').val(totalPoints);
        renderPaymentLines();
    });

    $('body').on('click','.btn-remove-paymentline',function(e){
        e.preventDefault();
        var indx = $(this).attr('data-indx'); 
        paymentlines.splice(indx,1);
        saveData('paymentlines',paymentlines);
        renderPaymentLines();
        renderSelectedCustomer();
    });

    $('.btn-remove-all-paymentlines').click(function(e){
        e.preventDefault(); 
        paymentlines=[];
        saveData('paymentlines',paymentlines);
        renderPaymentLines();
    });
    //payment lines ends here

    //customer starts here
    var ajxSearchCustomer = null;
    $('#txtCustomerSearch').keyup(function(){ 
        var drp = $('.drpCustomerSearch');
        var term =  $(this).val().trim();
        if(term != ""){
            ajxSearchCustomer = $.ajax({
                url:domainWithLang+'pos/searchcustomer',
                type:'POST',
                data:{term:term},
                beforeSend:function(){
                    if(ajxSearchCustomer!=null){
                        ajxSearchCustomer.abort();
                    }
                    drp.show().html('<div class="text-center"><i class="fa fa-spinner fa-spin fa-2x text-info"></i></div>');
                },
                success:function(resp){
                    var obj=JSON.parse(resp);
                    if(obj.status==0){
                        toastr.error(obj.message);
                        drp.html('').hide();
                    }else{
                        populateCustomersDrp(obj.data);
                    }
                },
                error:function(jqXHR, exception){
                    drp.html('').hide();
                    var msg = '';
                    if (jqXHR.status ===  0 || jqXHR.readyState === 0) {
                        return;
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
                    toastr.error(msg);
                }
        
            });
        }else{
            drp.html('').hide();
        }//end if has term
    });
    $('body').on('click','.btnSelectCustomer',function(e){
        e.preventDefault();
        var data = JSON.parse($(this).find('._data').text());
        data.total_points    = getSum(data.customer_points,'points');
        data.total_orders    = data.customer_points.length;
        customer = data;
        saveData('customer',customer);
        
        $('#txtCustomerSearch').val('').trigger('keyup'); 
        renderSelectedCustomer(); 

        //remove if there is any paymentline for points
        $.each(paymentlines,function(i,l){
            if(l.payment_method=='points'){
                paymentlines.splice(i,1);
            }
        });
        renderPaymentLines();
    });

    //select default customer(Walkin) 
    renderSelectedCustomer(); 
    //customer ends here


    //itemSearch dropdown starts here
    var ajxItemSearc=null;
    $('#txtItemSearch').keyup(function(e){
        if(e.which == 27){
            $('#txtItemSearch').val('');
        }
        var drp = $('.drpItemSearch');
        var term = $(this).val().trim();
        if(term.length>0){
            $('.btn-clearFilter').show();
            ajxItemSearc = $.ajax({
                url:domainWithLang+'pos/itemsearch/',
                type:'post',
                data:{term:term,store_id:store_id},
                beforeSend:function(){
                    if(ajxItemSearc!=null){
                        ajxItemSearc.abort();
                    }
                    drp.show().html('<div class="text-center"><i class="fa fa-spinner fa-spin fa-2x text-info"></i></div>');
                },
                success:function(resp){
                    var obj=JSON.parse(resp);
                    if(obj.status==0){
                        toastr.error(obj.message);
                        drp.html('').hide();
                    }else{
                        populateItemsDrp(obj.data);
                    }
                },
                error:function(jqXHR, exception){
                    drp.html('').hide();
                    var msg = '';
                    if (jqXHR.status ===  0 || jqXHR.readyState === 0) {
                        return;
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
                    toastr.error(msg);
                }
            });
        }else{
            $('.btn-clearFilter').hide();
            drp.html('').hide();
        }
        
    });
    $('.btn-clearFilter').click(function(){
        $('#txtItemSearch').val('').trigger('keyup');
    });
    $(document).click(function(){
        $('.drpItemSearch').hide();
    });
    $('.drpItemSearchWrapper').click(function(e){
        //e.preventDefault();
    });
    //itemSearch dropdown ends here

    //order holding
    $('.btnHoldOrder').click(function(){ 
        if(Object.keys(order).length !==0){
            var holdOrder={
                store_id:$('#drpStore').val(),
                store:$('#drpStore option:selected').text(),
                order:order,
                cashier:'Loggedin user',
                created: moment().format('MMMM Do YYYY, h:mm:ss a'),
                customer:customer,
                remarks: $('.txtHolderOrderRemarks').val()
            }; 
            holdedOrders.push(holdOrder);
            saveData('holdedOrders',holdedOrders);
            order={};
            saveData('order',order);
            renderOrder();
            customer = walkincustomer;
            renderSelectedCustomer();
            $('.txtHolderOrderRemarks').val('');
            renderHoldedOrders();
            toastr.success('Order holded successfully.');
        }else{
            toastr.error('There is no order');
        } 
    });//end btnHoldOrder

    $('body').on('click','.btnRestoreHoldedOrder',function(){
        
        if(Object.keys(order).length ===0){ 
            var indx = $(this).attr('data-id');
            if($('#drpStore').val() != holdedOrders[indx].store_id){
                toastr.error('This order is for different store ['+holdedOrders[indx].store+']');
            }else{
                order = holdedOrders[indx].order;
                saveData('order',order);
                renderOrder();

                customer = holdedOrders[indx].customer;
                saveData('customer',customer);
                renderSelectedCustomer();
                
                holdedOrders.splice(indx,1);
                saveData('holdedOrders',holdedOrders);
                renderHoldedOrders();
                toastr.success('Order restored successfully.');
            } 
        }else{
            toastr.error('There is an open order avaiable first complete or hold that.');
        }
    });

    $('body').on('click','.btnDeleteHoldedOrder',function(){
        if(confirm('Are you sure to remove this order?')){
            var indx = $(this).attr('data-id');
            holdedOrders.splice(indx,1);
            saveData('holdedOrders',holdedOrders);
            renderHoldedOrders();
            toastr.success('Holded order removed successfully.'); 
        }
    });

    //tab animation
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var animation = $(this).attr('data-amination');
        if(animation && animation!=''){
            var animationEffect = 'animated '+animation;
            var tabContent = $(this).parents('.nav-tabs').next('.tab-content');
            tabContent.addClass(animationEffect);
            setTimeout(function(){
                tabContent.removeClass(animationEffect);
            }, 1000);
        }
    });

    //check if already full screen toggle classes accordingly
    if (!window.screenTop && !window.screenY) {
        $('#btnFullscreen').removeClass('fs-enabled').addClass('fs-disabled');
    }else{
        $('#btnFullscreen').removeClass('fs-disabled').addClass('fs-enabled');
    }

    
    //toggle fullscreen
    $('#btnFullscreen').click(function(){ 
        var elem = document.body;
        if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
            if (elem.requestFullScreen) {
                elem.requestFullScreen();
            } else if (elem.mozRequestFullScreen) {
                elem.mozRequestFullScreen();
            } else if (elem.webkitRequestFullScreen) {
                elem.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
            } else if (elem.msRequestFullscreen) {
                elem.msRequestFullscreen();
            }
            $('#btnFullscreen').removeClass('fs-disabled').addClass('fs-enabled');
            fullscreen = 1;
            saveData('fullscreen',fullscreen);
        } else {
            if (document.cancelFullScreen) {
                document.cancelFullScreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitCancelFullScreen) {
                document.webkitCancelFullScreen();
            } else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            }
            $('#btnFullscreen').removeClass('fs-enabled').addClass('fs-disabled');
            fullscreen = 0;
            saveData('fullscreen',fullscreen);
        }
         
    });
    

    //paste anywhere
    $('html').bind('paste', function(e) {
        e.preventDefault();
        var trgt = e.target;
        if(e.originalEvent.clipboardData){
           var text = e.originalEvent.clipboardData.getData("text/plain");
           $('#txtItemSearch').val(text);
           $('#txtItemSearch').trigger('keyup');
         }
    });

    $(document).on('click','.btnAddToOrder',function(e){
        e.preventDefault();  
        $('#cart-tab-md').click();
        var item_id = $(this).parents('.item-item').find('._item_id').text();
        var item_uom_id = $(this).parents('.item-item').find('._item_uom_id').text(); 
        var description = $(this).parents('.item-item').find('._description').text();
        var vat = $(this).parents('.item-item').find('._vat').text();
        var price = $(this).parents('.item-item').find('._price_incl_vat').text();
        var img = $(this).parents('.item-item').find('._item_img').text();
        var stock = $(this).parents('.item-item').find('._stock').text();
        
        if(order.hasOwnProperty(item_uom_id)){
            order[item_uom_id].qty++;
            order[item_uom_id].sub_total = Number(order[item_uom_id].qty * order[item_uom_id].price ).toFixed(2);
        }else{
            var orderLine = {
                'item_id':item_id,
                'item_uom_id':item_uom_id,
                'description':description,
                'img':img,
                'vat':vat,
                'price':price,
                'qty':1,
                'sub_total':Number(price).toFixed(2),
                'stock':stock,
                }; 
            order[item_uom_id] = orderLine; 
        }
        saveData('order',order);
        renderOrder();
        renderPaymentLines();
        $('#txtItemSearch').val('').trigger('keyup');
    });

    $('body').on('click','.btn-removeItem',function(e){
        e.preventDefault();
        if(confirm("Are you sure to remove this item?")){
            var item_uom_id = $(this).parents('.orderLine').attr('data-item_uom_id');
            delete order[item_uom_id];
            saveData('order',order);
            renderOrder();
        }
    });

    $('body').on('click','.btn-removeAllItems',function(e){
        e.preventDefault();
        if(confirm("Are you sure to remove all items?")){
            order={};
            saveData('order',order);
            renderOrder();
        }
    });

    $('body').on('change','input.iQty',function(){
        var item_uom_id = $(this).parents('.orderLine').attr('data-item_uom_id');
        var stock = $(this).attr('max');
        var qty = $(this).val();
        console.log(stock);
        console.log(qty);
        if(parseInt(qty) > parseInt(stock)){
            $(this).val(stock);
        }else if(qty<1){
            $(this).val(1);
        }
        order[item_uom_id].qty = $(this).val();
        order[item_uom_id].sub_total = Number(order[item_uom_id].qty * order[item_uom_id].price ).toFixed(2);
        saveData('order',order);
        renderOrder();
    });

    $('#txtCashGiven').on('change keyup',function(){
         
        var cashGiven   = $('#txtCashGiven').val();
        var changeToGive = Number(parseFloat(cashGiven) - parseFloat(totalAmount) ).toFixed(2);
        $('#txtChange').val(changeToGive);
        if(changeToGive<0){
            $('#txtChange').addClass('bg-danger text-white');
        }else{
            $('#txtChange').removeClass('bg-danger text-white');
        }
    });

    $('body').on('click','.btnQtyUp',function(){ 
        var inpt = $(this).parents('.inputgroup').find('input');
        inpt.val(parseInt(inpt.val())+1);
        inpt.trigger('change');
    });
    $('body').on('click','.btnQtyDown',function(){
        var inpt = $(this).parents('.inputgroup').find('input');
        inpt.val(inpt.val()-1);
        inpt.trigger('change');
    });


    $('.txtRedeemPoints').on('keyup change',function(e){
        var max = parseInt($('.txtTotalPoints').val());
        var p = parseInt($(this).val());
        if(p > max){
            toastr.error('You should not select points more than total points ('+max+')');
            p=max;
        }else if(p<1){
            toastr.error('Minimum 1 point should be selected');
            p=0;
        }
        $('.txtRedeemPoints').val(p);
        $('.txtRedeemAmount').val(p/100);
    });

    $('.txtRedeemAmount').on('keyup change',function(e){
        
        var amt = $(this).val(); 
        var maxAmt = Number($('.txtTotalPoints').val()/100).toFixed(2);
        if(amt>maxAmt){
            toastr.error('You should not select mount more than ('+maxAmt+')');
            amt = maxAmt;
        }else if(amt < 0.01){
            toastr.error('Minimum 0.01 amount should be selected');
            amt=0;
        }
        $('.txtRedeemAmount').val(amt);
        var points = amt*100;
        $('.txtRedeemPoints').val(points);
    });


     



});//end ready

function renderHoldedOrders(){ 
    
    $('.holdedOrdersWrapper').html('<div class="alert alert-info text-center">No holded order found</div>');
    if(holdedOrders.length > 0){
        $('.holdedOrdersWrapper').html('');
        var str = '';
        $.each(holdedOrders,function(i,ord){
            var totalQty = 0;
            var iQty = 0;
            var totalAmount = 0;
            $.each(ord.order,function(i,v){ 
                totalAmount+= parseFloat(v.sub_total);
                totalQty += parseInt(v.qty); 
                iQty += 1; 
            });

            str +='<div class="card mb-3">';
            str +='<div class="card-body">';
                str +='<div class="float-right">';
                    str +='<small>'+iQty+'('+totalQty+')</small>';
                str +='</div>';
                str +='<div><label class="m-0">'+currency_symbol+' '+Number(totalAmount).toFixed(2)+'</label></div>';
                str +='<div class="float-right">';
                    str +='<small>'+ord.created+'</small>';
                str +='</div>';
                str +='<div class="bold">';
                    str +='<small>'+ord.cashier+'</small>';
                str +='</div>';
                str +='<div>';
                    str +='<small>'+ord.customer.customer_name+'</small>';
                str +='</div>';
                str +='<div>';
                    str +='<small>'+ord.remarks+'</small>';
                str +='</div>';
                str +='<div>';
                    str +='<small>Store: '+ord.store+'</small>';
                str +='</div>';
                str +='<div class="buttonz">';
                    str +='<button class="btn btnRestoreHoldedOrder btn-sm btn-info ml-0" data-id="'+i+'">';
                        str +='<i class="fa fa-undo"></i> Restore';
                    str +='</button>';
                    str +='<button class="btn btnDeleteHoldedOrder btn-sm btn-danger mr-0 float-right" data-id="'+i+'">';
                        str +='<i class="fa fa-trash"></i> delete';
                    str +='</button>'; 
                str +='</div>';
            str +='</div>';
            str +='</div>';
        });
        $('.holdedOrdersWrapper').append(str);
    }
}
function renderOrder(){ 
    $('.tbl-order tbody').html("");
    var totalAmount = 0;
    var totalQty = 0;
    var tr="";
    $.each(order,function(i,v){  
        tr += '<tr class="orderLine" data-item_id="'+v.item_id+'" data-item_uom_id="'+v.item_uom_id+'">';
            tr += '<td class="iItemName minlheight py-0 align-middle">'+v.description+'</td>';
            tr += '<td class="iPrice py-0 align-middle">'+v.price+'</td>';
            
            tr += '<td class="iQty py-0 px-0 align-middle"><div class="inputgroup">';
            tr += '<span class="btn btnQtyDown btn-sm btn-info py-1 px-1 m-0"><i class="fa fa-minus"></i></span>';
            tr += '<input type="number" min="1" max="'+v.stock+'" value="'+v.qty+'" class="iQty txtFluid border border-light">';
            tr += '<span class="btn btnQtyUp btn-sm btn-info py-1 px-1 m-0"><i class="fa fa-plus"></i></span></div></td>';
            
            tr += '<td class="iSubTotal py-0 align-middle">'+v.sub_total+'</td>';
            tr += '<td class="vat py-0 align-middle">'+(v.vat?"A":"B")+'</td>';
            tr += '<td class="iRemove py-0 align-middle"><span class="btn btn-sm btn-danger px-2 btn-removeItem waves-effect waves-light m-0"><i class="fa fa-trash"></i></span></td>';
        tr += '</tr>';
        totalAmount+= parseFloat(v.sub_total);
        totalQty += parseInt(v.qty);
    }); 
    $('.tbl-order tbody').append(tr);

    totalAmount = Number(totalAmount).toFixed(2);
    $('.totalPayable').html(totalAmount); 

    var totalItems = Object.keys(order).length;
    $('.itemsCount').html(totalItems+'('+totalQty+')');

    $('#txtCashGiven').val(0);
}

// function getOrder(){
//     if(localStorage.getItem("username") === null){
//         orderData = [];
//         saveData('order',orderData)
//     }else{
//         orderData = getData('order');
//     }
//     return orderData;
// }

function saveData(_var,data){
    if( typeof (data) === 'object' ){
        data = JSON.stringify(data)
    }
    localStorage.setItem(_var, data);
}

function getData(_var){
    data = localStorage.getItem(_var);
    try {
        data = JSON.parse(data);
        return data
    } catch (e) {
        return data;
    }
}

function removeData(_var){
    try {
        localStorage.removeItem(_var);
        return true;
    } catch (e) {
        return false;
    }
}

function populateItemsDrp(data){
    
    var drp = $('.drpItemSearch');
    if(data==null || data.length==0){
        drp.html('<div class="alert alert-danger text-center m-2">No matching item found.</div>');
    }else{
        var str = '<div class="p-2">';
        $.each(data,function(i,itm){
            str += '<div class="card item-item winter-neva-gradient mb-2"><a class="btnAddToOrder" href="#!">';            
            str += '<span class="d-none _vat">'+itm.item.vat+'</span>';
            str += '<span class="d-none _item_uom_id">'+itm.id+'</span>';
            str += '<span class="d-none _item_id">'+itm.item_id+'</span>';
            str += '<span class="d-none _stock">'+itm.qty+'</span>';
            str += '<div class="card-body p-1">';
            str += '<img class="float-left itemImg" src="'+domain+'img/items/thumbs/100x_'+itm.img+'" />';
            str += '<div class="item-title d-inline _description">'+itm.description_en+'</div>';
            str += '<span class="float-right badge badge-warning">qty: '+itm.qty+'</span>&nbsp;';
            str += '<span class="float-right badge badge-info mr-3">'+currency_symbol+' <span class="_price_incl_vat">'+itm.price_incl_vat+'</span></span>&nbsp;';
            str += '</div>';
            str += '</a></div>';
        });
        str+='</div>';
        drp.html(str);
    }
}

function getSum(array, column){
    if(array.length==0){ return 0; }
    let values = array.map((item) => parseInt(item[column]) || 0)
    return values.reduce((a, b) => a + b)
}

function populateCustomersDrp(data){
    
    var drp = $('.drpCustomerSearch');
    if(data==null || data.length==0){
        drp.html('<div class="alert alert-danger text-center m-2">No matching customer found.</div>');
    }else{
        var str = '<div class="p-2">';
        $.each(data,function(i,customr){
            var points          = getSum(customr.customer_points,'points');
            var total_orders    = customr.customer_points.length;
            str += '<div class="card customer-item winter-neva-gradient mb-2"><a class="btnSelectCustomer" href="#!">';            
            str += '<span class="d-none _data">'+ JSON.stringify(customr) +'</span>';
            str += '<div class="card-body p-1">'; 
            str += '<div class="customer-title d-inline _description">'+customr.customer_name+'</div>';
            str += '<span class="float-right badge badge-warning">points: '+points+'</span>&nbsp;';
            str += '<br><div class="customer-title d-inline _mobile">'+customr.mobile+'</div>';
            str += '<span class="float-right badge badge-info mr-3">rank: '+total_orders+'</span>&nbsp;';
            str += '<br><div class="customer-title d-inline _national_id_no">'+customr.national_id_no+'</div>';
            str += '</div>';
            str += '</a></div>';
        });
        str+='</div>';
        drp.html(str);
    }
}

function renderSelectedCustomer(){ 
    if(Object.keys(customer).length === 0 && getData('customer')!=null){
        customer = getData('customer');
    }else if(Object.keys(customer).length === 0 && getData('customer')==null){
        customer = walkincustomer;
    }
    $('.selected-customer').html(customer.customer_name);
    var str = '';
    str += '<div class="card-header">Selected customer</div>'; 
    str += '<div class="card-body p-1">'; 
    str += '<div class="customer-title d-inline _description">'+customer.customer_name+'</div>';
    str += '<span class="float-right badge badge-warning">points: '+customer.total_points+'</span>&nbsp;';
    str += '<br><div class="customer-title d-inline _mobile">'+customer.mobile+'</div>';
    str += '<span class="float-right badge badge-info mr-3">rank: '+customer.total_orders+'</span>&nbsp;';
    str += '<br><div class="customer-title d-inline _national_id_no">'+customer.national_id_no+'</div>';
    str += '</div>';
    $('.selected-customer-card').html(str);

    var addPointsPaymentLinePoints = 0;
    $.each(paymentlines,function(i,l){
        if(l.payment_method=='points'){
            addPointsPaymentLinePoints = l.description2;
        }
    });
    var remainingPoints = customer.total_points - addPointsPaymentLinePoints;
    $('.txtTotalPoints').val(remainingPoints)
}

function renderPaymentLines(){
    $('.tbl-order-lines tbody').html('');
    var tr = '';
    var ttl = 0;
    $.each(paymentlines,function(i,l){
        tr+='<tr>';
        tr+='<td>'+(i+1)+'</td>';
        tr+='<td>'+l.payment_method+'</td>';
        tr+='<td>'+l.amount+'</td>';
        tr+='<td class="text-right"><span data-indx="'+i+'" class="btn btn-sm btn-danger px-2 btn-remove-paymentline waves-effect waves-light m-0"><i class="fa fa-trash"></i></span></td>';
        tr+='</tr>';
        ttl += parseFloat(l.amount);
    });
    $('.tbl-order-lines tbody').append(tr);

    //total of lines and balance
    $('.tbl-order-lines tfoot').html('');
    var balance = parseFloat($('h3.totalPayable').text())-ttl;
    
    var tr2 = '<tr>';
    tr2 += '<td colspan="2" class="text-right">Total</td>';
    tr2 += '<td colspan="2">'+Number(ttl).toFixed(2)+'</td>';
    tr2 += '</tr>';
    tr2 += '<tr>';
    tr2 += '<td colspan="2" class="text-right">Balance</td>';
    tr2 += '<td colspan="2">'+ Number(balance).toFixed(2)+'</td>';
    tr2 += '</tr>';
    $('.tbl-order-lines tfoot').append(tr2);

    if(balance > 0){
        $('.btnSubmitPayment').hide();
    }else{
        $('.btnSubmitPayment').show();
    }
}
function hasPaymentMethod(pm){ 
    var hasPaymentMethod = false;
    $.each(paymentlines,function(i,l){ 
        if (l.payment_method ==pm){
            hasPaymentMethod = true;
            return false;
        }
    });
    return hasPaymentMethod;
}

function __(str){
    return str;
}

function loaditems(){
    var ajxLoaditems=null;
    ajxLoaditems =$.ajax({
        url: domainWithLang+'pos/loaditems',
        type:'post',
        data:{store_id:store_id},
        beforeSend:function(){
            if(ajxLoaditems!=null){
                ajxLoaditems.abort();
            }
            $('.items-wrapper').html('<div class="d-flex align-items-center justify-content-center smooth-scroll text-info"><p><i class="fa fa-spinner fa-spin fa-5x"></i></p><p>Loading items... </p></div>');
        },
        success:function(resp){
            try{
                var obj = JSON.parse(resp);
                var str = '<div class="row g-1 smooth-scroll">'; 
                if(obj.data.length>0){
                    $.each(obj.data,function(i,itm){ 
                        str+='<div class="col-xs-6 col-sm-6 col-md-4 col-lg-3 col-xl-2 col-item">';
                            str+='<div class="card item-item winter-neva-gradient card-item mb-3 z-depth-21 rounded-top">';
                                str+='<div class="bg-image hover-overlay ripple text-center d-flex align-items-center justify-content-center" data-mdb-ripple-color="light">';
                                    str+='<img src="'+domain+'/img/items/thumbs/100x_'+(itm.item_uom_images[0].image)+'" class="img-fluid rounded-top" alt=""> ';
                                    str+='<span class="d-none _item_img">'+(itm.item_uom_images[0].image)+'</span>';
                                str+='</div>';
                                str+='<div class="card-body p-2"> ';
                                    str+='<p class="card-text">';
                                        str+='<span class="item-description" title="'+itm['description_'+_lang]+'">';
                                            str+='<span class="_description">'+itm['description_'+_lang]+'</span>';
                                            str+='<span class="d-none _vat">'+itm.vat+'</span>';
                                            str+='<span class="d-none _item_uom_id">'+itm.id+'</span>';
                                            str+='<span class="d-none _item_id">'+itm.item_id+'</span>';
                                            str+='<span class="d-none _stock">'+itm['qty'+store_id]+'</span>';
                                        str+='</span>';
                                        str+='<span class="item-price badge badge-info" title="price">'+currency_symbol+' <span class="_price_incl_vat">'+itm.price_incl_vat+'</span> ';
                                        str+='</span>';
                                        str+=' <span class="item-price badge badge-warning" title="available quantity">'+itm['qty'+store_id]+'</span>';
                                        str+=' <span class="btn btn-sm btn-success py-1 px-2 float-right btnAddToOrder waves-effect waves-light">';
                                            str+='<i class="fa fa-plus"></i>';
                                        str+='</span>';
                                    str+='</p> ';
                                str+='</div>';
                            str+='</div> ';
                        str+='</div>';
                    });
                }else{
                   str +='<div class="col-xs-12 col-lg-12"><div class="alert alert-info text-center">No item found in this store.</div></div>'; 
                }
                str += '</div><!--row g-1 smooth-scroll-->';
                $('.items-wrapper').html(str);
            }catch (e) { 
                console.log(e.message);
                $('.items-wrapper').html('<div class="d-flex align-items-center justify-content-center"><div class="alert alert-danger text-center">It seem you are logged out.</div></div>');
            }
        },
        error:function(){
            $('.items-wrapper').html('<div class="d-flex align-items-center justify-content-center smooth-scroll"><div class="alert alert-danger">Opps! Ajax error, please contact administrator.</div></div>');
        }
    });
}