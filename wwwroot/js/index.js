//window.jsPDF = window.jspdf.jsPDF;

$(function() {
    const dataGrid = $("#dataGrid").dxDataGrid({
        dataSource: employees,
        keyExpr: "EmployeeID",
        allowColumnResizing: true,
        columnAutoWidth: true,
        columnFixing: {
            enabled: true
        },
        allowColumnReordering: true,
        columnChooser: { enabled: true },
        columns: [{
            dataField: "FullName",
            validationRules: [{
                type: "required"
            }],
            fixed: true
        }, {
            dataField: "Position",
            validationRules: [{
                type: "required"
            }]
        }, {
            dataField: "BirthDate", 
            dataType: "date",
            width: 100,
            validationRules: [{
                type: "required"
            }]
        }, {
            dataField: "HireDate", 
            dataType: "date",
            width: 100,
            validationRules: [{
                type: "required"
            }]
        }, "City", {
            dataField: "Country",
            groupIndex: 0,
            sortOrder: "asc",
            validationRules: [{
                type: "required"
            }]
        },
        "Address",
        "HomePhone", {
            dataField: "PostalCode",
            visible: false
        }],
        filterRow: { visible: true },
        searchPanel: { visible: true },
        groupPanel: { visible: true },
        selection: { mode: "single" },
        onSelectionChanged: function(e) {
            e.component.byKey(e.currentSelectedRowKeys[0]).done(employee => {
                if(employee) {
                    $("#selected-employee").text(`Selected employee: ${employee.FullName}`);
                }
            });
        },
        summary: {
            groupItems: [{
                summaryType: "count"
            }]
        },
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },
        toolbar: {
            items: [
                "groupPanel",
                {
                    location: "after",
                    widget: "dxButton",
                    options: {
                        text: "Collapse All",
                        width: 136,
                        onClick(e) {
                            const expanding = e.component.option("text") === "Expand All";
                            dataGrid.option("grouping.autoExpandAll", expanding);
                            e.component.option("text", expanding ? "Collapse All" : "Expand All");
                        },
                    },
                },
                {
                    name: "addRowButton",
                    showText: "always"
                },
                "exportButton",
                "columnChooserButton",
                "searchPanel"
            ]
        },
        masterDetail: {
            enabled: true,
            template: function (_, options) {
                const employee = options.data;
                const photo = $("<img>")
                    .addClass("employee-photo")
                    .attr("src", employee.Photo);
                const notes = $("<p>")
                    .addClass("employee-notes")
                    .text(employee.Notes);
                return $("<div>").append(photo, notes);
            }
        },
        export: {
            enabled: false,
            formats: ['xlsx', 'pdf']
        },
        //onExporting(e) {
        //    if (e.format === 'xlsx') {
        //        const workbook = new ExcelJS.Workbook(); 
        //        const worksheet = workbook.addWorksheet("Main sheet"); 
        //        DevExpress.excelExporter.exportDataGrid({ 
        //            worksheet: worksheet, 
        //            component: e.component,
        //        }).then(function() {
        //            workbook.xlsx.writeBuffer().then(function(buffer) { 
        //                saveAs(new Blob([buffer], { type: "application/octet-stream" }), "DataGrid.xlsx"); 
        //            }); 
        //        }); 
        //        e.cancel = true;
        //    } 
        //    else if (e.format === 'pdf') {
        //        const doc = new jsPDF();
        //        DevExpress.pdfExporter.exportDataGrid({
        //            jsPDFDocument: doc,
        //            component: e.component,
        //        }).then(() => {
        //            doc.save('DataGrid.pdf');
        //        });
        //    }
        //}
    }).dxDataGrid("instance");
});