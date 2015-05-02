<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Index" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Rock后台管理系统模板</title>
    <link href="themes/default/style.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="themes/css/core.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="themes/css/print.css" rel="stylesheet" type="text/css" media="print" />
    <link href="uploadify/css/uploadify.css" rel="stylesheet" type="text/css" media="screen" />
    <!--[if IE]>
<link href="themes/css/ieHack.css" rel="stylesheet" type="text/css" media="screen"/>
<![endif]-->
    <script src="js/speedup.js" type="text/javascript"></script>
    <script src="js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/jquery.validate.js" type="text/javascript"></script>
    <script src="js/jquery.bgiframe.js" type="text/javascript"></script>
    <script src="xheditor/xheditor-1.1.12-zh-cn.min.js" type="text/javascript"></script>
    <script src="uploadify/scripts/swfobject.js" type="text/javascript"></script>
    <script src="uploadify/scripts/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.core.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.util.date.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.validate.method.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.regional.zh.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.barDrag.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.drag.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.tree.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.accordion.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.ui.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.theme.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.switchEnv.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.alertMsg.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.contextmenu.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.navTab.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.tab.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.resize.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.dialog.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.dialogDrag.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.sortDrag.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.cssTable.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.stable.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.taskBar.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.ajax.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.pagination.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.database.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.datepicker.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.effects.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.panel.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.checkbox.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.history.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.combox.js" type="text/javascript"></script>
    <script src="js/dwz/dwz.print.js" type="text/javascript"></script>
    <!--
<script src="bin/dwz.min.js" type="text/javascript"></script>
-->
    <script src="js/dwz/dwz.regional.zh.js" type="text/javascript"></script>
    <script src="js/common.js" type="text/javascript"></script>
    <script src="js/commonExternal.js" type="text/javascript"></script>
    <script src="js/IntegralManager/IntegralHistory.js" type="text/javascript"></script>
    <script src="js/dwzCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            DWZ.init("config/dwz/dwz.frag.xml", {
                loginUrl: "login_dialog.html", loginTitle: "登录", // 弹出登录对话框
                //		loginUrl:"login.html",	// 跳到登录页面
                statusCode: { ok: 200, error: 300, timeout: 301 }, //【可选】
                pageInfo: { pageNum: "pageNum", numPerPage: "numPerPage", orderField: "orderField", orderDirection: "orderDirection" }, //【可选】
                debug: false, // 调试模式 【true|false】
                callback: function () {
                    initEnv();
                    $("#themeList").theme({ themeBase: "themes" }); // themeBase 相对于index页面的主题base路径
                }
            });

//            $("table td.expand").click(function (my) {
//                alert("init");
//                var id = $(my).title;
//                $("tr[partentId][partentId='" + id + "']").show();
            //            });

        });

        //展示行子列表
        function expandClick(my) {
            var $CurrentObject = $(my);
            //获取当前ID
            var id = $CurrentObject.attr('title');
            //呈现子数据
            $("tr[partentId][partentId='" + id + "']").toggleClass("hideHtmlTag");
            //切换图标
            $CurrentObject.hasClass('expand') == true ? $CurrentObject.removeClass('expand').addClass('collapse') : $CurrentObject.removeClass('collapse').addClass('expand');
        }
        
    </script>
</head>
<body scroll="no">
    <div id="layout">
        <div id="header">
            <div class="headerNav">
                <%--<a class="logo" href="http://j-ui.com">标志</a>--%>
                <p style="font-size: 24px; color: red; padding: 10px;">
                    <%--Rock后台管理系统模板--%></p>
                <ul class="nav">
                    <li><a href="ChangePwd.aspx" target="dialog" width="600">设置</a></li>
                    <li><a href="#" onclick="return  SysExit('admin')">安全退出</a></li>
                </ul>
                <ul class="themeList" id="themeList">
                    <li theme="default">
                        <div class="selected">
                            蓝色</div>
                    </li>
                    <li theme="green">
                        <div>
                            绿色</div>
                    </li>
                    <!--<li theme="red"><div>红色</div></li>-->
                    <li theme="purple">
                        <div>
                            紫色</div>
                    </li>
                    <li theme="silver">
                        <div>
                            银色</div>
                    </li>
                    <li theme="azure">
                        <div>
                            天蓝</div>
                    </li>
                </ul>
            </div>
            <!-- navMenu -->
        </div>
        <div id="leftside">
            <div id="sidebar_s">
                <div class="collapse">
                    <div class="toggleCollapse">
                        <div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="sidebar">
                <div class="toggleCollapse">
                    <h2>
                        主菜单</h2>
                    <div>
                        收缩</div>
                </div>
                <div class="accordion" fillspace="sidebar">
                    <div class="accordionHeader">
                        <h2>
                            <span>Folder</span>系统管理</h2>
                    </div>
                    <div class="accordionContent">
                        <ul class="tree treeFolder">
                            <li><a href="Programs/SysManager/CompanyList.aspx" target="navTab" rel="CompanyList" >机构管理</a></li>
                            <li><a href="Programs/SysManager/DepartmentList.aspx" target="navTab" rel="DepartmentList" >部门管理</a></li>
                            <li><a href="Programs/SysManager/UserInfoList.aspx" target="navTab" rel="UserInfoList" >用户管理</a></li>
                            <%--<li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("CompanyList"))==0) %>><a href="Programs/SysManager/CompanyList.aspx" target="navTab" rel="CompanyList" >
                                市区管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("DepartmentList"))==0) %>><a href="Programs/SysManager/DepartmentList.aspx" target="navTab" rel="DepartmentList" >
                                区县管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("UserInfoList"))==0) %>><a href="Programs/SysManager/UserInfoList.aspx" target="navTab" rel="UserInfoList" >
                                用户管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("MemberList"))==0) %>><a href="Programs/IntegralManager/MemberList.aspx" target="navTab" rel="MemberList" >
                                企业管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("PointsReasonList"))==0) %>><a href="Programs/IntegralManager/PointsReasonList.aspx" target="navTab" rel="PointsReasonList" >
                                扣分原因管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("PointsLevelList"))==0) %>><a href="Programs/IntegralManager/PointsLevelList.aspx" target="navTab" rel="PointsLevelList" >
                                扣分等级管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("IntegralHistoryList"))==0) %>><a href="Programs/IntegralManager/IntegralHistoryList.aspx" target="navTab" rel="IntegralHistoryList" >
                                扣分管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("MateriallHistoryList"))==0) %>><a href="Programs/IntegralManager/MateriallHistoryList.aspx" target="navTab" 
                                rel="MateriallHistoryList">材料审核管理</a></li>
                            <li <%=OutputHtmlAttributeStringHelper.OutPutDisplayNone(CurrentPageList.Count(p=>p.FunCode.Equals("SysConfigList"))==0) %>><a href="Programs/IntegralManager/SysConfigList.aspx" target="navTab" rel="SysConfigList" >
                                参数设置</a></li>--%>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="container">
            <div id="navTab" class="tabsPage">
                <div class="tabsPageHeader">
                    <div class="tabsPageHeaderContent">
                        <!-- 显示左右控制时添加 class="tabsPageHeaderMargin" -->
                        <ul class="navTab-tab">
                            <li tabid="main" class="main"><a href="javascript:;"><span><span class="home_icon">我的主页</span></span></a></li>
                        </ul>
                    </div>
                    <div class="tabsLeft">
                        left</div>
                    <!-- 禁用只需要添加一个样式 class="tabsLeft tabsLeftDisabled" -->
                    <div class="tabsRight">
                        right</div>
                    <!-- 禁用只需要添加一个样式 class="tabsRight tabsRightDisabled" -->
                    <div class="tabsMore">
                        more</div>
                </div>
                <ul class="tabsMoreList">
                    <li><a href="javascript:;">我的主页</a></li>
                </ul>
                <div class="navTab-panel tabsPageContent layoutBox">
                    <div class="page unitBox">
                        <div class="accountInfo">
                            <%--<div class="alertInfo">
                                <h2><%=this.CurrentLoginUser.RealName %>,你好</h2>
                            </div>--%>
                            <div class="right">
                               <%-- <p>待审核的积分企业：<label style="color: red"><%=auditIntegral %></label> 家</p>
                                <p>待审核的材料企业：<label style="color: red"><%=auditMateriall%></label> 家</p>--%>
                            </div>
                            <p>
                                <span><h2><%--<%=this.CurrentLoginUser.RealName %>,你好!--%></h2></span></p>
                            <%--<p>
                                DWZ官方微博:<a href="http://weibo.com/dwzui" target="_blank">http://weibo.com/dwzui</a></p>--%>
                        </div>
                        <div <%--class="pageFormContent" layouth="80" style="margin-right: 230px"--%>>
                            <!--页面内容-->
                             <div class="pageContent">
                                <%--<table class="table" width="100%" layouth="138">
                                    <thead>
                                        <tr>
                                            <th>
                                                企业名称
                                            </th>
                                            <th>
                                                积分数
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%
                
                                            foreach (Member model in DefaultList)
                                            {
                                        %>
                                        <tr>
                                            <td>
                                                <%=model.Name %>
                                            </td>
                                            <td>
                                                <%=model.IntegralInfo.IntegralNumber %>
                                            </td>
           
                                        </tr>
                                        <%
                }
                                        %>
                                    </tbody>
                                </table>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="footer">
            Copyright &copy; 2012 Rock Spring.net+NHibernate+DWZ</div>
</body>
</html>
