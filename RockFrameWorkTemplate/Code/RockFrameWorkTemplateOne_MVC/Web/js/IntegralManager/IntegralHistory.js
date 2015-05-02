//$(document).ready(function () {
//    $("#PointsType").change(function () {
//        var selectIntegralReason = $(this).val();
//        $.get('Ajax/IntegralManager/PointsReasonService.ashx?action=GetPointsReasonNumberById&Id=' + selectIntegralReason, function (date) {
//            $("#IntegralNumber").val()
//        });
//    });
//});


//初次审核
function OpenAlertTodoAudit(info,url) {
    alertMsg.confirm(info, {
        okCall: function () {
            url += "&date=" + new Date().getTime();
            $.get(url, function (date) {
                var json = jQuery.parseJSON(date);
                //刷新窗口
                dialogAjaxDone(json);
            });
        }
    });
}

function validateCallbackUploadFile(form, callback) {
    var $form = $(form);
    if (!$form.valid()) {
        return false;
    }

    $.ajax({
        type: form.method || 'POST',
        url: $form.attr("action"),
        data: $form.serializeArray(),
        dataType: "json",
        cache: false,
        success: callback || DWZ.ajaxDone,
        error: DWZ.ajaxError
        });

//    var url =$form.attr("action");
//    $.post(url, function(date) {
//        var json = jQuery.parseJSON(date);
//        //刷新窗口
//        dialogAjaxDone(json);
//    });

    return false;
}
