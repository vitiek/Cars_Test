$(document).ready(function () {

    var editCarUrl = '/Car/CreateUpdateCar';
    var deleteCarUrl = '/Car/ConfirmDeleteCar';

    var table = $('#example').DataTable({
        paging: true,
        processing: true,
        serverSide: true,
        searching: true,
        sDom: 'lrtip',
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        ajax: {
            url: "/Car/GetCarsDataTable",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        columns: [
            { data: "id", visible: false },
            { data: "manufacturer" },
            { data: "model" },
            { data: "imageUrl", orderable: false },
            { data: "year" },
            { data: "price" },
            { data: "id", orderable: false }
        ],
        // Column Definitions
        columnDefs: [
            {
                targets: 3,
                data: "imageUrl",
                render: function (data, type, row, meta) {
                    if (data === null) data = 'https://www.getinstantprinting.com/site_data/category/noimage.jpg';
                    return '<img src="' + data + '"class="img-thumbnail img-datatbles">';
                }
            },
            {
                targets: 6,
                data: "id",
                render: function (data, type, row, meta) {

                    return '<div class="btn-group">' +
                        '<button class="btn btn-primary btn-sm btn-car-edit" data-id="' + data + '">Edit</button>' +
                        '<button class="btn btn-primary btn-sm btn-car-delete" data-id="' + data + '">Delete</button>' +
                        '</div>';
                }
            }
        ]
    });

    modalFormLink('.create-car', {
        url: editCarUrl,
        formSubmitSuccess: function () {
            table.draw();
        }
    });

    $('.car-search').on('click', function () {
        var year = $('#year').val();
        var manufacturer = $('#manufacturer').val();
        table.columns(4).search(year).columns(1).search(manufacturer).draw();
    });

    $('.create-random-car').on('click', function () {
        $.ajax({
            url: '/Car/CreateRandomCars',
            type: 'POST',
            success: function (data) {
                table.draw(false);
            }
        });
    });

    modalFormLink('.btn-car-edit', {
        url: editCarUrl,
        data: function (el) {
            return { id: el.data('id') };
        },
        formSubmitSuccess: function () {
            table.draw(false);
        }
    });

    modalFormLink('.btn-car-delete', {
        url: deleteCarUrl,
        data: function (el) {
            return { id: el.data('id') };
        },
        formSubmitSuccess: function () {
            table.draw(true);
        }
    });
});

function modalFormLink(selector, options) {
    var settings = $.extend({
        // These are the defaults.
        url: null,
        formSubmitSuccess: null,
        modalsSelector: '#modal-medium',
        dataSelector: 'id'
    }, options);

    $(document).on('click', selector, function (e) {
        e.preventDefault();

        var data = settings.data !== null && typeof settings.data === 'function' ? settings.data($(this)) : null;

        $.ajax({
            url: settings.url,
            data: data,
            type: 'GET',
            success: function (data) {
                $(settings.modalsSelector).find('.modal-content').html(data);
                $(settings.modalsSelector).modal('show');

                handleModalFormSubmit(settings.modalsSelector, settings.customReturnUrl, settings.formSubmitSuccess);

                if (settings.formInitFunction !== null && typeof settings.formInitFunction === 'function')
                    settings.formInitFunction();
            }
        });
    });
}


function handleModalFormSubmit(modalSelector, customReturnUrl, formSubmitSuccess) {
    var form = $(modalSelector).find('form');

    if (form.length === 0)
        return;

    $(form).on('submit', function (e) {
        form = $(this);
        e.preventDefault();


        let btn = $(this).find('button[type=submit]');

        btn.attr('disabled', 'disabled');
        let btnInnerHtml = btn.html();
        btn.text(btn.data('loading-text'));

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            success: function (data) {

                if (data.success) {

                    $(modalSelector).modal('hide');

                    $(modalSelector).one('hidden.bs.modal', function (e) {
                        if (formSubmitSuccess !== null && typeof formSubmitSuccess === 'function')
                            formSubmitSuccess(form, data);
                    });

                } else {

                    $('.modal-content').html(data);

                    //form was update, re-apply the submit handler
                    handleModalFormSubmit(modalSelector, customReturnUrl);
                }
            },
            complete: function () {
                btn.html(btnInnerHtml);
                btn.removeAttr('disabled');
            }
        });
    });
}