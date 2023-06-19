(function ($) {

    var $mainBoardModal = $("#MainBoardModal"),
        $mainBoardForm = $mainBoardModal.find("#mainBoardForm");

    var $aspectModal = $("#AspectModal"),
        $aspectForm = $aspectModal.find("#aspectForm");

    var $targetModal = $("#TargetModal"),
        $targetForm = $targetModal.find("#targetForm");

    //-----------Main Board Table -------------
    var $mainBoardTable = $("#MainBoardTable").DataTable({
        paging: false,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            blockPage();

            $.post("get-all-mainboard", filter, function (result) {
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
                action: () => $mainBoardTable.draw(false)
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
                data: 'monthName'
            },
            {
                targets: 1,
                data: 'unitName'
            },
            {
                targets: 2,
                data: 'year'
            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                className: 'text-center fit',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-icon bg-info text-white view-aspect-record" data-id="${row.id}" data-ent-name="${row.year}" data-ent-month="${row.monthName}" > <i class="fas fa-eye"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-icon btn-primary edit-mainBoard-record" data-id="${row.id}" data-bs-toggle="modal" data-bs-target="#MainBoardModal"> <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-icon btn-danger delete-mainBoard-record" data-id="${row.id}" data-ent-name="${row.unitName}" > <i class="fas fa-trash"></i>`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    /***************MainBoard Edit*********/
    $(document).on('click', '.edit-mainBoard-record', function (e) {
        var id = $(this).attr("data-id");
        e.preventDefault();
        blockPage();
        $.post("get-mainboard-by-id", { id: id }, function (result) {
            $mainBoardModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });

    });
    /***************Aspect Edit*********/
    $(document).on('click', '.edit-aspect-record', function (e) {
        var id = $(this).attr("data-id");
        e.preventDefault();
        blockPage();
        $.post("get-aspect-by-id", { id: id, mainBoardId: null }, function (result) {
            $aspectModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });

    });
    /***************Target Edit*********/
    $(document).on('click', '.edit-target-record', function (e) {

        var id = $(this).attr("data-id");
        debugger;
        e.preventDefault();
        blockPage();
        $.post("get-target-by-id", { id: id, aspectId: null }, function (result) {
            $targetModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });

    });

    /***************Add Main Board *********/
    $("#addMainBoard").click(function (e) {
        e.preventDefault();
        blockPage();
        $.post("get-mainboard-by-id", function (result) {
            $mainBoardModal.find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });
    });
    $(document).on("submit", $mainBoardForm, function (e) {
        e.preventDefault();
        if (e.target.name == "mainBoardForm") {
            var formData = $(e.target).serializeFormToObject();

            $.post('mainBoard-save',
                formData,
                function (data) {

                    var message = "success";
                    if (data.message) {
                        if (data.message.value) {
                            message = data.message.value;
                        } else {
                            message = data.message;
                        }
                    };

                    $.notify(message, "success");
                    $mainBoardTable.ajax.reload();
                    $mainBoardModal.hide();
                    $mainBoardModal.removeClass("in");
                    $(".modal-backdrop").remove();

                    $('body').removeAttr('style');

                }).fail(function (error) {
                    $.notify(error.responseJSON.message, "error");
                });
        }
    });
    /***************Add ASpect *********/
    $(document).on("click","#addAspect",function (e) {
        e.preventDefault();
        blockPage();
        var mainBoardId = $("#mainBoardId").val();

        $.post("get-aspect-by-id", { id: 0, mainBoardId: mainBoardId },
            function (result) {
                $("#AspectModal").find(".modal-content").html(result);
            }).always(function () {
                unblockPage();
            });
    });
    $(document).on("submit", "#aspectForm", function (e) {
        e.preventDefault();
        if (e.target.name == "aspectForm") {
            var formData = $(e.target).serializeFormToObject();
            debugger;
            $.post('aspect-save',
                formData,
                function (data) {

                    var message = "success";
                    if (data.message) {
                        if (data.message.value) {
                            message = data.message.value;
                        } else {
                            message = data.message;
                        }
                    };

                    $.notify(message, "success");
                    $("#AspectTable").DataTable().ajax.reload(null, false);
                    $("#AspectModal").hide();
                    $("#AspectModal").removeClass("in");
                    $(".modal-backdrop").remove();
                    $('body').removeAttr('style');

                }).fail(function (error) {
                    $.notify(error.statusText, "error");
                });
        }

    });
    /***************Add Target *********/
    $(document).on("submit", "#targetForm", function (e) {
        e.preventDefault();
        if (e.target.name == "targetForm") {
            var formData = $(e.target).serializeFormToObject();
            debugger;
            $.post('target-save',
                formData,
                function (data) {

                    var message = "success";
                    if (data.message) {
                        if (data.message.value) {
                            message = data.message.value;
                        } else {
                            message = data.message;
                        }
                    };

                    $.notify(message, "success");
                    $("#TargetTable").DataTable().ajax.reload(null, false);
                    $("#TargetModal").hide();
                    $("#TargetModal").removeClass("in");
                    $(".modal-backdrop").remove();
                    $('body').removeAttr('style');

                }).fail(function (error) {
                    $.notify(error.responseJSON.message, "error");
                });
        }

    });

    /***************Delete Main Board ************/
    $(document).on('click', '.delete-mainBoard-record', function () {
        var id = $(this).attr("data-id");
        deleteMainBoard(id);
    });
    function deleteMainBoard(id) {

        Notiflix.Confirm.show(
            'حذف اطلاعات', 'آیا قصد دارید این آیتم را حذف نمایید؟', 'بله', 'خیر',
            function () {
                var url = 'mainBoard-remove'
                $.ajax({
                    url: url,
                    data: { "id": id },
                    type: 'DELETE',
                    success: function (data) {
                        if (data.result === "success") {

                            $.notify(data.message, "success");
                            $mainBoardTable.ajax.reload();

                        } else {
                            $.notify(data.message, "error");

                        }
                    } ,error: function (data, textStatus, errorThrown) {
                        $.notify(data.responseJSON.message, "error");
                    }
                });
            },
            () => {

            },
            {
            }
        );
    }
    /***************Delete ASpect ***************/
    $(document).on('click', '.delete-aspect-record', function () {
        var id = $(this).attr("data-id");
        deleteAspect(id);

    });

    function deleteAspect(id) {
        debugger;
        Notiflix.Confirm.show(
            'حذف اطلاعات', 'آیا قصد دارید این آیتم را حذف نمایید؟', 'بله', 'خیر',
            function () {
                var url = 'aspect-remove'

                $.ajax({
                    url: url,
                    data: { "id": id },
                    type: 'DELETE',
                    success: function (data) {
                        debugger;
                        if (data.result === "success") {
                            console.log(data);
                            $.notify(data.message, "success");
                            $("#AspectTable").DataTable().ajax.reload(null, false);
                        } else {
                            $.notify(data.message, "error");

                        }
                    }, error: function (data, textStatus, errorThrown) {
                        $.notify(data.responseJSON.message, "error");
                    }
                });
            },
            () => {

            },
            {
            }
        );
    }
    /***************Delete Target *********/
    $(document).on('click', '.delete-target-record', function () {
        var id = $(this).attr("data-id");
        deleteTarget(id);

    });
    function deleteTarget(id) {

        Notiflix.Confirm.show(
            'حذف اطلاعات', 'آیا قصد دارید این آیتم را حذف نمایید؟', 'بله', 'خیر',
            function () {
                var url = 'target-remove'

                $.ajax({
                    url: url,
                    data: { "id": id },
                    type: 'DELETE',
                    success: function (data) {
                        if (data.result === "success") {

                            $.notify(data.message, "success");
                            $("#TargetTable").DataTable().ajax.reload(null, false);

                        } else {
                            $.notify(data.message, "error");

                        }
                    }
                });
            },
            () => {

            },
            {
            }
        );



    }

    /***************View Aspect Record *********/
    $(document).on('click', '.view-aspect-record', function (e) {
        e.preventDefault();
        $("#MainBoardTable > tbody >tr").removeClass("bg-info");
        $(this).parent().parent().addClass("bg-info");
        var id = $(this).attr("data-id");
        var month = $(this).attr("data-ent-month");
        var year = $(this).attr("data-ent-name");
        viewAspectByMainBoard(id, year, month);
    });
    function viewAspectByMainBoard(id, year, month) {
        $.post("get_aspect_by_mainboard",
            {
                mainBoardId: id

            }, function (result) {
                $("#aspects").html(result);
                $('#flush-aspect').addClass("show");
                reloadAspectTable();

                $("#main-board-record-selected").text([" (", id, ")", "-", month, "-", year].join("  "));
            }).always(function () {
                unblockPage();
            });
    }
    /***************View Target Record *********/
    $(document).on('click', '.view-target-record', function (e) {
        e.preventDefault();
        $("#AspectTable > tbody >tr").removeClass("bg-info");
        $(this).parent().parent().addClass("bg-info");
        var id = $(this).attr("data-id");
        var title = $(this).attr("data-ent-name");
        viewTargetByASpect(id, title);
    });
    function viewTargetByASpect(id, title) {
        $.post("get_target_by_aspect",
            {
                aspectId: id
            }, function (result) {
                $("#targets").html(result);
                $('#flush-target').addClass("show");
                $("#aspect-selected").text([" (", id, ")", "-", title].join("  "));
                reloadTargetTable();
            }).always(function () {
                unblockPage();
            });
    }


    function reloadMainBoardTable() {
        $mainBoardTable.ajax.reload();
    }
    function reloadAspectTable() {
        var $aspectTable = $("#AspectTable").DataTable({
            paging: false,
            serverSide: true,
            ajax: function (data, callback, settings) {
                blockPage();
                var mainBoardId = $("#mainBoardId").val();
                var filter = { mainBoardId: mainBoardId };
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
    }
    function reloadTargetTable() {
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
    }

    $(document).on('click', '#addMessages', function () {
        var $this = $(this);
        debugger;
        var index = parseInt($(".targetMessages").length);
        $("#divMessages").append(" <input type='text' asp-for='Messages'  name='messages[" + index + "]' class='form-control targetMessages m-2'  />");
    });
    /***************Add Main Target *********/

})(jQuery);

$(document).on("click", "#addTarget", function (e) {
    debugger;
    e.preventDefault();
    blockPage();
    var aspectId = $("#aspectId").val();

    $.post("get-target-by-id", { id: 0, aspectId: aspectId },
        function (result) {
            $("#TargetModal").find(".modal-content").html(result);
        }).always(function () {
            unblockPage();
        });
});

