
(function ($) {

    var $targetModal = $("#TargetModal"),
        $targetForm = $targetModal.find("#targetForm");
    var $targetTable = $("#TargetTable").DataTable({
        paging: false,
        serverSide: true,
        ajax: function (data, callback, settings) {
            blockPage();
            var aspectId = $("#aspectId").val();
            var filter = { aspectId: aspectId };
            $.post("get_all_target_grid_data", filter, function (result) {
                callback({
                    recordsTotal: result.length,
                    recordsFiltered: result.length,
                    data: result
                });
            }, "json").always(function () {
                unblockPage();

            });

        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => $targetTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },

        columnDefs: [
            {
                targets: 0,
                data: 'title'
            },
            {
                targets: 1,
                data: 'score'
            }, {
                targets: 2,
                data: 'progress'
            }, {
                targets: 3,
                data: 'message'
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                className: 'text-center fit',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-icon btn-primary edit-target-record" data-id="${row.id}" data-bs-toggle="modal" data-bs-target="#TargetModal">  <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-icon btn-danger delete-target-record" data-id="${row.id}" data-ent-name="${row.title}" >  <i class="fas fa-trash"></i>`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });
   
    $(document).on('click', '.edit-target-record', function (e) {

        var id = $(this).attr("data-id");
        debugger;
        e.preventDefault();
        blockPage();
        $.post("get-target-by-id", { id: id, aspectId:null }, function (result) {
            $targetModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });

    });
    $("#addTarget").click(function (e) {
        debugger;
        e.preventDefault();
        blockPage();
        var aspectId = $("#aspectId").val();

        $.post("get-target-by-id", {id:0, aspectId: aspectId },
            function (result) {
                $targetModal.find(".modal-content").html(result);
            }).always(function () {
                unblockPage();
            });
    });
    
   
})(jQuery);

