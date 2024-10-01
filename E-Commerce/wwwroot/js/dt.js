$(function () {
    $(".dt thead tr").clone(true).appendTo(".dt thead");
    $(".dt thead tr:eq(1) th").each(function (i) {
        var title = $(this).text();
        if ($.inArray(title, ["Delete", "actions"])) {
            $(this).html(
                '<input type="text" class="form-control p-1 txtDtFilter"  placeholder="Search ' +
                    title +
                    '" />'
            );

            $("input", this).on("keyup change", function () {
                if (table.column(i).search() !== this.value) {
                    table.column(i).search(this.value).draw();
                }
            });
        }
    });

    if ($(".dt").length > 0) {
        var table = $(".dt").DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            order: [],
            pageLength: 10,
            bStateSave: true,
            stateDuration: 60 * 60 * 24 * 7,
            // stateSaveParams: function (settings, data) {
            // 	data.start = 0;
            // 	//data.search.search = "";
            // 	$.each(data.columns, function (i, v) {
            // 		//prevent column level searching
            // 		v.search.search = v;
            // 	});
            // },
            stateLoadParams: function (settings, data) {
                var hasColumnFilter = false;
                $.each(data.columns, function (i, v) {
                    if (v.search.search !== "") {
                        hasColumnFilter = true;
                        return false; // Exit the loop
                    }
                });

                if (data.search.search !== "" || hasColumnFilter) {
                    $(".txtDtFilter").each(function (i) {
                        $(this).val(data?.columns[i]?.search?.search || "");
                    });
                }
            },

            dom:
                "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            columnDefs: typeof columnDefs === "undefined" ? [] : columnDefs,
            buttons: [
                {
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
                        $(".dt")
                            .DataTable()
                            .columns()
                            .every(function () {
                                if (this.search() !== "") {
                                    hasFilter = true;
                                    return false; // Exit the loop if a filter is found in any column
                                }
                            });
                        if ($(".dt").DataTable().search().trim() != "") {
                            hasFilter = true;
                        }
                        return hasFilter
                            ? "custom-dt-button buttons-clear-filter buttons-html5 btn btn-sm btn-danger p-1 m-1"
                            : "custom-dt-button buttons-clear-filter buttons-html5 btn btn-sm btn-info p-1 m-1";
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
        });
        $("div.dataTables_filter input").addClass(
            "form-control d-inline-block py-0 w-50"
        );
    }

    $(".txtDtFilter,.dataTables_filter input").keyup(function () {
        var hasFilter = false;
        $(".dt")
            .DataTable()
            .columns()
            .every(function () {
                if (this.search() !== "") {
                    hasFilter = true;
                    return false; // Exit the loop if a filter is found in any column
                }
            });
        if ($(".dt").DataTable().search().trim() != "") {
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
