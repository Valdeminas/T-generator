// Write your Javascript code.
$(document).ready(function () {
    $('*[data-autocomplete-url]')
        .each(function () {
            var labelMember = $(this).data("label-member");
            var valueMember = $(this).data("value-member");
            var url = $(this).data("autocomplete-url");
            $(this).autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: url,
                        data: { term: request.term },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item[labelMember], value: item[valueMember] };
                            }));
                        }
                    });
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                    $('#' + $(this).data("value-input")).val(ui.item.value);
                    //$("#selected-customer").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    $(this).val(ui.item.label);
                }
            });
        });
});
