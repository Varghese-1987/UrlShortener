window.nintex = window.nintex || {};
window.nintex.urlshortener = window.nintex.urlshortener || {};
(function (o) {
    o.validateUrl = function (input) {
        var expresssion = /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[\-;:&=\+\$,\w]+@)?[A-Za-z0-9\.\-]+|(?:www\.|[\-;:&=\+\$,\w]+@)[A-Za-z0-9\.\-]+)((?:\/[\+~%\/\.\w\-_]*)?\??(?:[\-\+=&;%@\.\w_]*)#?(?:[\.\!\/\\\w]*))?)/
        return expresssion.test(input);
    };
    o.getShortUrl = function (inputUrl) {
        $('#loader-wrapper').show();
        $.ajax({
            url: "/Home/GetShortUrl",
            type: "POST",
            data: {
                originalUrl: inputUrl
            },
            success: function (result) {
                $('#Url').val(result);
                $('#modalViewUrl').modal();
                $('#loader-wrapper').hide();
            },
            error: function (request, status, error) {
                console.log(request.responseText);
                $('#loader-wrapper').hide();
            }
        });
    };

    o.getOriginalUrl = function (inputUrl) {
        $('#loader-wrapper').show();
        $.ajax({
            url: "/Home/GetOriginalUrl",
            type: "POST",
            data: {
                inputUrl: inputUrl
            },
            success: function (result) {
                if (result.IsSuccess)
                {
                    $('#Url').val(result.OriginalUrl);
                    $('#modalViewUrl').modal();
                } else {
                    alert(result.OriginalUrl);
                }
                $('#loader-wrapper').hide();
            },
            error: function (request, status, error) {
                console.log(request.responseText);
                $('#loader-wrapper').hide();
            }
        });
    };

})(window.nintex.urlshortener);