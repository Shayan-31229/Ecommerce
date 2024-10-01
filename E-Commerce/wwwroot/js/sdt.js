var sdt;
$(function(){

    makeFilters();

    var calculatedFields = [
        {  targets: columns.length,
            title:'Actions',
            width: '95px',
            render: function (data, type, row, meta) {
                var uid = Math.floor(1000 + (Math.random() * 99999));
                var btnView =  '<a href="'+domainWithLang+'admin/'+_controller+'/view/'+row.id+'" title="view record" class="btn btn-sm py-1 px-2 mr-1 btn-info waves-effect"><i class="fa fa-eye"></i></a> ';
                var btnedit =  '<a href="'+domainWithLang+'admin/'+_controller+'/edit/'+row.id+'" title="edit record" class="btn btn-sm py-1 px-2 mr-1 btn-warning waves-effect"><i class="fa fa-pencil"></i></a> ';
                var btnDelete =  '<form name="post_'+uid+'_'+row.id+'" style="display:none;" method="post" action="'+domainWithLang+'admin/'+_controller+'/delete/'+row.id+'"><input type="hidden" name="_method" value="POST"></form> ';
                btnDelete +=  '<a href="#" title="Delete record" class="btn btn-sm py-1 px-2 btn-danger waves-effect" data-confirm-message="Are you sure you want to delete # '+row.id+'?" onclick="if (confirm(this.dataset.confirmMessage)) { document.post_'+uid+'_'+row.id+'.submit(); } event.returnValue = false; return false;"><i class="fa fa-trash"></i></a> ';
                var btns = btnView+btnedit+btnDelete;
                return btns;
            }
    
        }
    ];
    if(typeof columnDefs!=='undefined'){
        calculatedFields = calculatedFields.concat(columnDefs);
    }


    sdt = $('.sdt').DataTable({
        orderCellsTop: true,
        fixedHeader: true,
        "order": [],
        "pageLength": 10,
        "bStateSave":true,
        "stateDuration": 60 * 60 * 24 * 7,
        "stateSaveParams": function (settings, data) {
            data.start = 0;
            data.search.search = "";
            $.each(data.columns,function(i,v){
                //prevent column level searching 
                v.search.search = "";
            });
        },    
        dom:"<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-5'i><'col-sm-7'p>>",
		columnDefs: calculatedFields,	
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':visible'
                },
                text :'Copy',
                className:'btn btn-sm btn-info p-1 m-1'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                },
                text :'Excel',
                className:'btn btn-sm btn-info p-1 m-1'
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: ':visible'
                },
                text :'PDF',
                className:'btn btn-sm btn-info p-1 m-1'
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: ':visible'
                },
                text :'CSV',
                extension: '.txt',
                className:'btn btn-sm btn-info p-1 m-1'
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                },
                text :'Print',
                className:'btn btn-sm btn-info p-1 m-1'
            },
            {
                extend:'colvis',
                text:'Columns',
                className : 'btn btn-sm btn-info p-1 m-1'
            }
        ],
        "processing": true,
        responsive: true,
        columns:(typeof columns==='undefined'?[]:columns),
        bServerSide : true,
        sAjaxSource : (typeof sAjaxSource==='undefined'?[]:sAjaxSource)    
    });

    
    
});

function makeFilters(){
    if($('.sdt thead tr').length == 1){

        $('.sdt thead tr').clone(true).appendTo( '.sdt thead' );
        $('.sdt thead tr:eq(1) th').each( function (i) {
            if($(this).html()=='select'){
                var chkAll = `<div class="form-check chkALLWrapper">
                                <input class="form-check-input chkAll" type="checkbox" value="" id="chkAll" />
                                <label class="form-check-label" for="chkAll">&nbsp;</label>
                            </div>`;
                $(this).html(chkAll);
            }else{
                var title = $(this).text();
                if($.inArray(title,["Delete","actions"])){
                    
                    $(this).html( '<input type="text" class="form-control p-1 txtDtFilter"  placeholder="Search '+title+'" />' );
                
                    $( 'input', this ).on( 'keyup change', function () {
                        if ( sdt.column(i).search() !== this.value ) {
                            sdt
                                .column(i)
                                .search( this.value )
                                .draw();
                        }
                    } );
                }
            }
            
        } );
    }
}