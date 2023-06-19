(function ($) {

    var $unitModal = $("#UnitModal"),
        $form = $unitModal.find("form");

    var $unitsTable = $("#UnitsTable").DataTable({
        paging: false,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            blockPage();

            $.post("/Unit/GetAll", filter, function (result) {
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
                action: () => $unitsTable.draw(false)
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
                data: 'name'
            },
            {
                targets: 1,
                data: 'description'
            },
            {
                targets: 2,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                className: 'text-center fit',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-icon btn-primary edit-record" data-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UnitModal"> <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-icon btn-danger delete-record" data-id="${row.id}" data-ent-name="${row.name}" >  <i class="fas fa-trash"></i>`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    $(document).on("submit", $form, function(e) {
        e.preventDefault();
        var formData = $(e.target).serializeFormToObject();
        $.post('unit-save', formData , function (data) {
                var message = "success";
                if (data.message) {
                    if (data.message.value) {
                        message = data.message.value;
                    }
                    else {
                        message = data.message;
                    }
                };

                $.notify(message, "success");
                $unitsTable.ajax.reload();
            $unitModal.hide();
            $("#TargetModal").removeClass("in");
            $(".modal-backdrop").remove();
            
        }).fail(function(error) {
            $.notify(error.responseJSON.message, "success");
        });
    });

    $(document).on('click', '.edit-record', function (e) {
        var id = $(this).attr("data-id");
        e.preventDefault();

        blockPage();
        $.post("/Unit/GetUnit", {id : id}, function (result) {
            $unitModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });
    });

    $(document).on('click', '.delete-record', function () {
        var id = $(this).attr("data-id");
        deleteUnit(id);
    });
    
    $("#addUnit").click(function (e) {
       e.preventDefault();
        blockPage();
        $.post("/Unit/GetUnit", {}, function(result) {
            $unitModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });
    });

    function deleteUnit(id) {
        window.Notiflix.Confirm.show(
            'حذف اطلاعات', 'آیا قصد دارید این آیتم را حذف نمایید؟', 'بله', 'خیر',
            function () {
                var url = 'unit-remove';

                $.ajax({
                    url: url,
                    data: { "id": id },
                    type: 'DELETE',
                    success: function (data) {
                        if (data.Value.result === "success") {

                            $.notify(data.message, "success");
                            $unitsTable.ajax.reload();


                        } else {
                            $.notify(data.message, "error");

                        }
                    } ,error: function (data, textStatus, errorThrown) {
                        $.notify(data.responseJSON.message, "error");
                    }
                });
            },
            () => {

            }
        );
    }

})(jQuery);




