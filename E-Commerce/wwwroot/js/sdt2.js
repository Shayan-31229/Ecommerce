var sdt;
    $(function() {

        makeFilters();

        var sdt_default_object = { 
            dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            lengthMenu: [
                [10, 25, 50, 100, 1000],
                [10, 25, 50, 100, 1000]
            ], // Page length options with customized labels
            pageLength: 10, // Default page length 
            buttons: [{
                extend: "copyHtml5",
                exportOptions: {
                    columns: ":visible",
                },
                text: "Copy",
                className: "btn btn-sm btn-info p-1 m-1",
            },
            {
                extend: "excelHtml5",
                exportOptions: {
                    columns: ":visible",
                },
                text: "Excel",
                className: "btn btn-sm btn-info p-1 m-1",
            },
            {
                extend: "pdfHtml5",
                exportOptions: {
                    columns: ":visible",
                },
                text: "PDF",
                className: "btn btn-sm btn-info p-1 m-1",
            },
            {
                extend: "csvHtml5",
                exportOptions: {
                    columns: ":visible",
                },
                text: "CSV",
                extension: ".txt",
                className: "btn btn-sm btn-info p-1 m-1",
            },
            {
                extend: "print",
                exportOptions: {
                    columns: ":visible",
                },
                text: "Print",
                className: "btn btn-sm btn-info p-1 m-1",
            },
            {
                extend: "colvis",
                text: "Columns",
                className: "btn btn-sm btn-info p-1 m-1",
            },
            {
                text: "Clear Filters",
                className: function (dt) {
                    var hasFilter = false;
                    $(".sdt")
                        .DataTable()
                        .columns()
                        .every(function () {
                            if (this.search() !== "") {
                                hasFilter = true;
                                return false; // Exit the loop if a filter is found in any column
                            }
                        });
                    if ($(".sdt").DataTable().search().trim() != "") {
                        hasFilter = true;
                    }
                    return hasFilter ?
                        "custom-dt-button buttons-clear-filter buttons-html5 btn btn-sm btn-danger p-1 m-1" :
                        "custom-dt-button buttons-clear-filter buttons-html5 btn btn-sm btn-info p-1 m-1";
                },
                action: function (e, dt, node, config) {
                    // Clear filters
                    dt.search("").columns().search("").draw();

                    // Clear column-level filters individually
                    var columns = dt.columns()[0];
                    $.each(columns, function (index, column) {
                        dt.column(column)
                            .search("", false, false, true)
                            .draw();
                    });
                    // Clear filter input textbox value
                    $(".txtDtFilter").val("").keyup();

                    // Optionally, you can remove the focus from the filter input
                    $(".dataTables_filter input").blur();
                    node.removeClass("btn-danger").addClass("btn-info");
                },
            },

            ],
            fixedHeader: true,
            "bStateSave": true,
            stateDuration: 60 * 60 * 24 * 7,
            // stateSaveParams: function (settings, data) {
            // 	data.start = 0;
            // 	//data.search.search = "";
            // 	$.each(data.columns, function (i, v) {
            // 		//prevent column level searching
            // 		v.search.search = v;
            // 	});
            // },
            stateLoadParams: function(settings, data) {
                var hasColumnFilter = false;
                $.each(data.columns, function(i, v) {
                    if (v.search.search !== "") {
                        hasColumnFilter = true;
                        return false; // Exit the loop
                    }
                });

                if (data.search.search !== "" || hasColumnFilter) {
                    $(".txtDtFilter").each(function(i) {
                        $(this).val(data?.columns[i]?.search?.search || "");
                    });
                }
            },
            // "stateSaveParams": function(settings, data) {
            //     data.start = 0;
            //     data.search.search = "";
            //     $.each(data.columns, function(i, v) {
            //         //prevent column level searching 
            //         v.search.search = "";
            //     });
            // },
            "processing": true,
            responsive: true, 
            bServerSide: true, 
        };

        $.extend(sdt_default_object, sdt_obj); 
        sdt = $('.sdt').DataTable(sdt_default_object);


        $(".txtDtFilter,.dataTables_filter .form-control").on('keyup change',function() {
            var hasFilter = false;
            $(".sdt")
                .DataTable()
                .columns()
                .every(function() {
                    if (this.search() !== "") {
                        hasFilter = true;
                        return false; // Exit the loop if a filter is found in any column
                    }
                });
            if ($(".sdt").DataTable().search().trim() != "") {
                hasFilter = true;
            }
            if (hasFilter) {
                $(".buttons-clear-filter")
                    .addClass("btn-danger")
                    .removeClass("btn-info");
            } else {
                $(".buttons-clear-filter")
                    .addClass("btn-info")
                    .removeClass("btn-danger");
            }
        });

         
    });

    function makeFilters() {
        if ($('.sdt thead tr').length == 1) {

            $('.sdt thead tr').clone(true).appendTo('.sdt thead');
            $('.sdt thead tr:eq(0) th').each(function(i) {
                var title = $(this).text();
                if($(this).attr('data-drp')){
                    var tbldrpOpts = JSON.parse($(this).attr('data-drp')); 
                    var str = '<select class="form-control browser-default custom-select px-1 py-0 txtDtFilter w-100" style="height:28px;">';
                    str += '<option value="">All</option>';
                    $.each(tbldrpOpts,function(i,v){
                        str += '<option value="'+i.trim()+'">'+v+'</option>';
                    });
                    str += '</select>';
                    $(this).html(str);

                    $('select', this).on('keyup change', function() {
                        if (sdt.column(i).search() !== this.value) {
                            sdt
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                }else if (!(title == "Actions1" || title == '1الإجراءات')) {
                    $(this).html('<input type="text" class="form-control p-1 txtDtFilter w-100"  placeholder="Search ' + title.trim() + '" />');

                    $('input', this).on('keyup change', function() {
                        if (sdt.column(i).search() !== this.value) {
                            sdt
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                }
            });
        }
    }

     