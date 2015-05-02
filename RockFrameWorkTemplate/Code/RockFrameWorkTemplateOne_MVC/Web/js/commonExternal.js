/*
打开一个弹出窗口（弹出层）【适应新增功能】
@url    弹出层对于的页面URL(可带参数)
@rel    弹出层唯一标识（可以理解成ID）
@title  弹出层窗口标题
@width  弹出层宽度【可选、如果不填写、默认为500】
@height 弹出层高度【可选、如果不填写、默认为400】  
*/
function OpenDialogWindow(url, rel, title, width, height) {
    if (width != null && height != null) {
        $.pdialog.open(url, rel, title, { width: width, height: height });
    }
    else {
        $.pdialog.open(url, rel, title, { width: 500, height: 400 });
    }
}

/*
打开一个弹出窗口（弹出层）【适应修改功能】
@url    弹出层对于的页面URL(可带参数)
@rel    弹出层唯一标识（可以理解成ID）
@title  弹出层窗口标题
@checkboxName  要循环的checkbox元素name属性名称
@width  弹出层宽度【可选、如果不填写、默认为500】
@height 弹出层高度【可选、如果不填写、默认为400】  
*/
function OpenDialogWindowTodoEdit(url, rel, title,checkboxName, width, height) {
    var number = 0;
    var userid = "";
    $(":checkbox[name=" + checkboxName + "]:checked").each(function () {
        number++;
        userid = $(this).val();
    });
    if (number == 1) {
        if (width != null && height != null) {
            $.pdialog.open(url + "?id=" + userid, rel, title, { width: width, height: height });
        }
        else
            $.pdialog.open(url + "?id=" + userid, rel, title, { width: 500, height: 400 });
    }
    else {
        alertMsg.info('只能勾选一项，请重新选择！');
        return false;
    }
}

/*
打开一个弹出窗口（弹出层）【适应修改功能】
@url    弹出层对于的页面URL(可带参数)
@rel    弹出层唯一标识（可以理解成ID）
@title  弹出层窗口标题
@checkboxName  要循环的checkbox元素name属性名称
@width  弹出层宽度【可选、如果不填写、默认为500】
@height 弹出层高度【可选、如果不填写、默认为400】  
*/
function OpenDialogWindowTodoEditByPK(url, rel, title, pkId, width, height) {
    var id = $("#" + pkId).val();
    if (width != null && height != null) {
        $.pdialog.open(url + "?" + pkId + "=" + id, rel, title, { width: width, height: height });
    }
    else
        $.pdialog.open(url + "?" + pkId + "=" + id, rel, title, { width: 500, height: 400 });
}

/*
打开一个提示框【适应删除功能、用于表格数据】
@url    删除Url
@checkboxName  要循环的checkbox元素name属性名称
@info   删除确认信息
@data   其他条件参数 
*/
function OpenAlertWindowTodoDeleteByPK(url,info, data) {
    alertMsg.confirm(info, {
        okCall: function () {
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                dataType: "json",
                cache: false,
                success: dialogAjaxDone, //navTabAjaxDone,--如果是navTabAjaxDone则关闭了当前选项卡、此前应该是关闭当前的弹出层、并刷新当前选项卡
                error: DWZ.ajaxError
            });
        }
    });
}

/*
打开一个弹出窗口（弹出层）【适应修改功能、用于表格数据里】
@url    弹出层对于的页面URL(可带参数)
@rel    弹出层唯一标识（可以理解成ID）
@title  弹出层窗口标题
@checkboxName  要循环的checkbox元素name属性名称
@width  弹出层宽度【可选、如果不填写、默认为500】
@height 弹出层高度【可选、如果不填写、默认为400】  
*/
function OpenDialogWindowTodoEditLinked(url, rel, title, id, width, height) {
    
    if (width != null && height != null) {
        $.pdialog.open(url + "?id=" + id, rel, title, { width: width, height: height });
    }
    else
        $.pdialog.open(url + "?id=" + id, rel, title, { width: 500, height: 400 });

}




/*
打开一个提示框【适应删除功能】
@url    删除Url
@checkboxName  要循环的checkbox元素name属性名称
@info   删除确认信息
@data   其他条件参数 
*/
function OpenAlertWindowTodoDelete(url, checkboxName, info, data) {
    var number = 0;
    var userid = "";
    $(":checkbox[name=" + checkboxName + "]:checked").each(function () {
        number++;
        userid += "'" + $(this).val() + "',";
    });
    if (number == 0) {
        alertMsg.info("至少勾选一项，请重新选择！");
        return false;
    }
    else {
        userid = userid.substring(0, userid.length - 1);
        alertMsg.confirm(info, {
            okCall: function () {
                $.ajax({
                    type: 'POST',
                    url: url + "&"+checkboxName+"=" + userid,
                    data: data,
                    dataType: "json",
                    cache: false,
                    success: dialogAjaxDone, //navTabAjaxDone,--如果是navTabAjaxDone则关闭了当前选项卡、此前应该是关闭当前的弹出层、并刷新当前选项卡
                    error: DWZ.ajaxError
                });
            }
        });
    }
}

/*
打开一个提示框【适应删除功能、用于表格数据】
@url    删除Url
@checkboxName  要循环的checkbox元素name属性名称
@info   删除确认信息
@data   其他条件参数 
*/
function OpenAlertWindowTodoDeleteLinked(url, checkboxName, info, data) {
        alertMsg.confirm(info, {
            okCall: function () {
                $.ajax({
                    type: 'POST',
                    url: url + "&" + checkboxName + "=" + id,
                    data: data,
                    dataType: "json",
                    cache: false,
                    success: dialogAjaxDone, //navTabAjaxDone,--如果是navTabAjaxDone则关闭了当前选项卡、此前应该是关闭当前的弹出层、并刷新当前选项卡
                    error: DWZ.ajaxError
                });
            }
        });
}

function OpenAlertWindowTodo(url, checkboxName, info, data) {
    var number = 0;
    var userid = "";
    $(":checkbox[name=" + checkboxName + "]:checked").each(function () {
        number++;
        userid += "'" + $(this).val() + "',";
    });
    if (number == 0) {
        alertMsg.info("至少勾选一项，请重新选择！");
        return false;
    }
    else {
        userid = userid.substring(0, userid.length - 1);
        alertMsg.confirm(info, {
            okCall: function () {
                $.ajax({
                    type: 'POST',
                    url: url + "&id=" + userid+"&date="+new Date().getTime(),
                    data: data,
                    dataType: "json",
                    cache: false,
                    success: dialogAjaxDone, //navTabAjaxDone,--如果是navTabAjaxDone则关闭了当前选项卡、此前应该是关闭当前的弹出层、并刷新当前选项卡
                    error: DWZ.ajaxError
                });
            }
        });
    }
}


