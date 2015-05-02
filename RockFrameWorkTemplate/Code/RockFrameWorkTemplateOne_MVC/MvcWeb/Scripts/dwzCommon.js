
//-----------------------------------------------------------------此js统一暂时有Rock统一维护、修改请先请咨询------------------------------------------------

jQuery.RockDwz = {
    /*
    打开一个弹出窗口（弹出层）【适应新增、修改功能】
    @url    弹出层对于的页面URL(可带参数)
    @rel    弹出层唯一标识（可以理解成ID）
    @title  弹出层窗口标题
    @width  弹出层宽度【可选、如果不填写、默认为550】
    @height 弹出层高度【可选、如果不填写、默认为450】  
    */
    OpenDialogWindow: function (url, rel, title, width, height) {
        if (width != null && height != null) {
            $.pdialog.open(url, rel, title, { width: width, height: height });
        }
        else {
            $.pdialog.open(url, rel, title, { width: 550, height: 450 });
        }
    },

    /*
    打开一个新的选项卡【适应新增、修改功能】
    @url    选项卡页面URL(可带参数)
    @rel    选项卡唯一标识（可以理解成ID）
    @title  选项卡窗口标题
    @isExternal 是否已iframe的形式嵌入
    */
    OpenNavTabWindow: function (url, tabid, title, data, isExternal) {
        if (isExternal != null) {
            navTab.openTab(tabid, url, { title: title, fresh: true, data: data, external: true });
        } else {
            navTab.openTab(tabid, url, { title: title, fresh: true, data: data });
        }
    },
    /*
    打开一个提示框【适应删除功能、用于表格删除一条数据】
    @url    删除Url
    @checkboxName  要循环的checkbox元素name属性名称
    @info   删除确认信息
    @data   其他条件参数 
    */
    OpenAlertWindowTodoDeleteByPK: function (url, info, data) {
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
    },
    /*
    打开一个提示框【适应删除功能,删除多项】
    @url    删除Url
    @checkboxName  要循环的checkbox元素name属性名称
    @info   删除确认信息
    @data   其他条件参数 
    */
    OpenAlertWindowTodoDeleteToOptions: function (url, checkboxName, info, data) {
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
                        url: url,
                        data: ({Ids:userid}),
                        dataType: "json",
                        cache: false,
                        success: dialogAjaxDone, //navTabAjaxDone,--如果是navTabAjaxDone则关闭了当前选项卡、此前应该是关闭当前的弹出层、并刷新当前选项卡
                        error: DWZ.ajaxError
                    });
                }
            });
        }
    },
    /*
    打开提示框，提示是否导出Excel【适应导出Excel】
    @url    Excel导出Url
    @info   确认信息
    */
    OpenAlertWindowTodoOutPutExcel: function (url, info) {
        if (info) {
            alertMsg.confirm(info, {
                okCall: function () { doExport(); }
            });
        } else { doExport(); }

        function doExport() {
            var $p = navTab.getCurrentPanel();
            var $form = $("#pageHeader", $p);
            window.location = url + (url.indexOf('?') == -1 ? "?" : "&") + $form.serialize();
        }
    }
};

//---------------------------------------------公共函数---------------------------------------------------------
jQuery.RockFun = {
    /*
    为指定的inputId元素设置一个新的Guid值
    @inputId为input元素的Id标签
    */
    NewGuid: function (inputId) {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                guid += "-";
        }

        $("#" + inputId).val(guid);
    }
};












