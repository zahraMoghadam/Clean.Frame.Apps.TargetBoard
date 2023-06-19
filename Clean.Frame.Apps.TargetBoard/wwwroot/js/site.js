
var blockPage, unblockPage;

(function ($) {
    
    var language = {
        emptyTable: "هیچ داده ای در جدول موجود نیست",
        info: "_START_-_END_ از _TOTAL_ ردیف",
        infoEmpty: "No records",
        infoFiltered: "(filtered from _MAX_ total entries)",
        infoPostFix: "",
        infoThousands: ",",
        lengthMenu: "نمایش _MENU_ ردیف",
        loadingRecords: "در حال بارگذاری..",
        processing: '<i class="fas fa-refresh fa-spin"></i>',
        search: "جستجو:",
        zeroRecords: "هیچ ردیفی یافت نشد",
        paginate: {
            first: '<i class="fas fa-angle-double-right"></i>',
            last: '<i class="fas fa-angle-double-left"></i>',
            next: '<i class="fas fa-chevron-left"></i>',
            previous: '<i class="fas fa-chevron-right"></i>'
        },
        aria: {
            sortAscending: ": activate to sort column ascending",
            sortDescending: ": activate to sort column descending"
        }
    };

    //$.extend(true, $.fn.dataTable.defaults, {
    //    searching: false,
    //    ordering: false,
    //    language: language,
    //    processing: true,
    //    autoWidth: false,
    //    responsive: true,
    //    bLengthChange: false,
    //    dom: [
    //        "<'row'<'col-md-12'f>>",
    //        "<'row'<'col-md-12't>>",
    //        "<'row mt-2'",
    //        "<'col-lg-1 col-xs-12'<'float-right text-center data-tables-refresh'B>>",
    //        "<'col-lg-3 col-xs-12'<'float-right text-center'i>>",
    //        "<'col-lg-3 col-xs-12'<'text-center'l>>",
    //        "<'col-lg-5 col-xs-12'<'float-left'p>>",
    //        ">"
    //    ].join('')
    //});

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function (camelCased = false) {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        if (camelCased && camelCased === true) {
            return convertToCamelCasedObject(obj);
        }

        return obj;
    };


    function convertToCamelCasedObject(obj) {
        var newObj, origKey, newKey, value;
        if (obj instanceof Array) {
            return obj.map(value => {
                if (typeof value === 'object') {
                    value = convertToCamelCasedObject(value);
                }
                return value;
            });
        } else {
            newObj = {};
            for (origKey in obj) {
                if (obj.hasOwnProperty(origKey)) {
                    newKey = (
                        origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey
                    ).toString();
                    value = obj[origKey];
                    if (
                        value instanceof Array ||
                            (value !== null && value.constructor === Object)
                    ) {
                        value = convertToCamelCasedObject(value);
                    }
                    newObj[newKey] = value;
                }
            }
        }
        return newObj;
    }

    blockPage = function () {
        $("#overlay").show();
    }


    unblockPage =function () {
        $("#overlay").hide();
    }
   
})(jQuery);




