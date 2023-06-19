//var Minio = require("./minio")

var title = "";
(function ($) {

    loadBoard(null, null);

    $("#year").change(function () {
        $("#boardMonth").change();
    });


    $("#boardMonth").change(function () {
        var month = parseInt($("#boardMonth").val()) == -1 ? null : parseInt($("#boardMonth").val());
        var year = parseInt($("#year").val()) == -1 ? null : parseInt($("#year").val());

        loadBoard(month, year);
    });


    function loadBoard(month, year) {

        var $modal = new window.bootstrap.Modal(document.getElementById('detailsModal'), {});

        $.ajax({
            url: 'get-all-mainboard-home', type: "GET", data: { month: month, year: year },
            'contentType': 'application/json',
            success: function (result) {
                var data = JSON.parse(result);
                if (data) {
                    if (data.length == 0) {
                        var message = "متاسفانه داده یافت نشد";
                        $("#wrapper").html('<div class="alert alert-danger m-5 text-center" role="alert">متاسفانه داده یافت نشد</div >');
                        return false;
                    }
                    var months = data.monthNames;
                    $("#month").text(`${months[data.month - 1]} ماه ${data.year}`);
                    $("#wrapper").empty();
                    _.mapKeys(data,
                        function (value, key, object) {

                            if (key === "month" || key === "year" || key == "monthNames")
                                return;

                            title = data['aspects'];
                            _.forEach(data['aspects'],
                                function (value) {
                                    var $titleBlock = $(`
                            <div class="row my-2" id="${value.title}">
                                <div class="col-lg-2 col-md-2 title-block">
                                    <div class="card ${calcClass(value.progress).card} text-center h-100">
                                        <div class="card-header">منظر <span class="fw-bolder">${value.title}</span></div>
                                        <div class="card-body text-start d-flex justify-content-center flex-column">
                                            <div class="fw-light weight">وزن کل: <span class="value">${value.weight
                                        } % </span></div>
                                            <div class="fw-light score">نمره کل: <span class="value">${value.score
                                        }</span></div>
                                            <div class="fw-light progresss">درصد تحقق: <span class="value">${value.progress
                                        } % </span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            `);

                                    _.forEach(value.targets,
                                        function (value) {

                                            var $targetBlock = $(`
                            <div class="col target-block">
                                <div class="card text-center ${calcClass(value.progress).card} h-100">
                                    <div class="card-header">${value.title}</div>
                                    <div class="card-body">
                                        ${value.progress > 0
                                                    ? `<div class='row'>
                                            <div class='col text-center'>
                                                <button type='button' class='btn ${calcClass(value.progress).button
                                                    } score'>نمره کل: <span class='value'>${value.score}</span></button>
                                            </div>
                                            <div class='col text-center'>
                                                <button type='button' class='btn ${calcClass(value.progress).button
                                                    } progresss'>درصد تحقق: <span class='value'>${value.progress}%</span></button>
                                            </div>
                                        </div>`
                                                    : ''}
                                        <div class="messages mt-1">${value.messages.map(function (item) {
                                                        return `<div>${item}</div>`;
                                                    }).join('')}</div>
                                    </div>
                                </div>
                            </div>
                        `);

                                            $targetBlock.click(function (e) {
                                                e.preventDefault();

                                                $("#detailsModalLabel").text(value.title);
                                                $("#detailsModal div.modal-body").addClass(calcClass(value.progress).card)
                                                    .html($targetBlock.find("div.card-body").clone());
                                                $modal.show();
                                            });

                                            $titleBlock.append($targetBlock);

                                        });


                                    $("#wrapper").append($titleBlock);
                                });


                        });


                    document.getElementById('detailsModal').addEventListener('hidden.bs.modal', function () {
                        $("#detailsModalLabel").text("");
                        $("#detailsModal div.modal-body").removeClass().addClass("modal-body").html("");
                    });

                    
                }

                return false;
            }
        });
    };

    function calcClass(progress) {
        switch (true) {
        case (progress >= 110):
            return { card: "text-dark bg-info", button: "btn-outline-dark" }

        case (progress >= 90):
            return { card: "text-white bg-success", button: "btn-outline-light" }

        case (progress >= 70):
            return { card: "text-dark bg-warning", button: "btn-outline-dark" }

        case (progress > 0 && progress < 70):
            return { card: "text-white bg-danger", button: "btn-outline-light" }

        default:
            return { card: "text-dark bg-light", button: "btn-outline-dark" }
        }
    }

})(jQuery);





