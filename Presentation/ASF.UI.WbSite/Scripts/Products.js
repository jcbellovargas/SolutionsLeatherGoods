//obtiene los clientes para el autocomplete
$("#DealerName")
    .autocomplete({
        autoSelect: true,
        autoFocus: true,
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: '/Product/Product/GetDealers',
                datatype: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data,
                        function (val, item) {
                            return {
                                label: val.LastName + ' ' + val.FirstName,
                                value: val.LastName + ' ' + val.FirstName,
                                Id: val.Id
                            };
                        }));
                }
            });
        },
        select: function (event, ui) {
            $("#DealerId").val(ui.item.Id);
        }
    });