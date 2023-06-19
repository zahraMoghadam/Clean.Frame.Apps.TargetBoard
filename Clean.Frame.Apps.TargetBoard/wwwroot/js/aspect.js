(function ($) {

    var $aspectModal = $("#AspectModal"),
        $aspectForm = $aspectModal.find("#aspectForm");
    var $aspectTable = $("#AspectTable").DataTable({
        paging: false,
        serverSide: true,
        ajax: function (data, callback, settings) {
            blockPage();
            var mainBoardId = $("#mainBoardId").val();
            var filter = { mainBoardId: mainBoardId};
            $.post("get_all_aspect_grid_data", filter, function (result) {
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
                action: () => $aspectTable.draw(false)
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
                data: 'weight'
            },
            {
                targets: 2,
                data: 'score'
            }, {
                targets: 3,
                data: 'progress'
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
                        `   <button type="button" class="btn btn-sm btn-icon  bg-info text-white view-target-record" data-id="${row.id}" data-ent-name="${row.title}" > <i class="fas fa-eye"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-icon btn-primary edit-aspect-record" data-id="${row.id}" data-bs-toggle="modal" data-bs-target="#AspectModal"> <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-icon btn-danger delete-aspect-record" data-id="${row.id}" data-ent-name="${row.title}" >  <i class="fas fa-trash"></i>`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });
   
    $(document).on('click', '.edit-aspect-record', function (e) {
        var id = $(this).attr("data-id");
        e.preventDefault();
        blockPage();
        $.post("get-aspect-by-id", { id: id, mainBoardId:null}, function (result) {
            $aspectModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });

    });
    $("#addAspect").click(function (e) {
        e.preventDefault();
        blockPage();
        var mainBoardId = $("#mainBoardId").val();
      
        $.post("get-aspect-by-id", {id:0, mainBoardId: mainBoardId},
        function (result) {
            $aspectModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });
    });

    $(document).on('click', '.view-target-record', function(e) {
        e.preventDefault();
        $("#AspectTable > tbody >tr").removeClass("bg-info");
        $(this).parent().parent().addClass("bg-info");
        var id = $(this).attr("data-id");
        var title = $(this).attr("data-ent-name");
        viewTargetByASpect(id, title);
    });

    function viewTargetByASpect(id,title) {
        $.post("/target/Index",
            {
                aspectId: id
            }, function (result) {
                $("#targets").html(result);
                $('#flush-target').addClass("show");
                $("#aspect-selected").text([" (", id, ")", "-", title].join("  "));
            }).always(function () {
            unblockPage();
        });
    }

   
    $(document).on("submit", "#aspectForm", function (e) {
        e.preventDefault();
        if (e.target.name == "aspectForm") {
            var formData = $(e.target).serializeFormToObject();
            debugger;
            $.post('aspect-save',
                formData,
                function(data) {

                    var message = "success";
                    if (data.message) {
                        if (data.message.value) {
                            message = data.message.value;
                        } else {
                            message = data.message;
                        }
                    };

                    $.notify(message, "success");
                    $aspectTable.ajax.reload();
                    $aspectModal.hide();
                    $aspectModal.removeClass("in");
                    $(".modal-backdrop").remove();
                    $('body').removeAttr('style');

                }).fail(function(error) {
                $.notify(error.responseJSON.message, "success");
            });
        }

    });
   
    function reloadAspectTable() {
        $aspectTable.ajax.reload();
    }
})(jQuery);




