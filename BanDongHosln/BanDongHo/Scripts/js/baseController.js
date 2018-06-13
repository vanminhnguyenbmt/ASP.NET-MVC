var common = {
    init: function () {
        common.registerevent();
    },
    registerevent: function () {
        $("#txtkeyword").autocomplete({
            minlength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName",
                    datatype: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtkeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtkeyword").val(ui.item.label);
                return false;
            }
        })
         .autocomplete("instance")._renderitem = function (ul, item) {
             return $("<li>")
               .append("<a>" + item.label + "</a>")
               .appendto(ul);
         };
    }
}
common.init();