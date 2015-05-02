<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoList.aspx.cs" Inherits="Web.Programs.SysManager.UserInfoList" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BLL.Impl.SysManager" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>
<form id="pagerForm" method="post" action="Programs/SysManager/UserInfoList.aspx">
<input type="hidden" name="pageNum" value="1" />
<input type="hidden" name="numPerPage" value="<%=DefaultListPagination.PageSize %>" />
<input type="hidden" name="orderField" value="<%=OrderbyStr %>" />
<input type="hidden" name="orderDirection" value="<%=OrderByDirection %>" />
</form>
<div class="pageHeader">
    <form id="pageHeader" onsubmit="return navTabSearch(this);" action="Programs/SysManager/UserInfoList.aspx"
    method="post">
    <div class="searchBar">
        <table class="searchContent">
            <tr>
                <td>
                    登录名<input type="text" name="UserName" value="<%=Request["UserName"] ?? string.Empty %>" />
                </td>
               
                <td>
                    真实姓名<input type="text" name="UseActurlName" value="<%=Request["UseActurlName"] ?? string.Empty %>" />
                </td>
              
                <td>
                    手机<input type="text" name="UserMobile" value="<%=Request["UserMobile"] ?? string.Empty %>" />
                </td>
               
            </tr>
        </table>
        <div class="subBar">
            <ul>
                <li>
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button type="submit">
                                检索</button></div>
                    </div>
                </li>
            </ul>
        </div>
    </div>
    </form>
</div>
<div class="pageContent">
    <div class="panelBar">
        <ul class="toolBar">
            <li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/UserInfoAdd.aspx','UserInfoAdd','新增')">
                <span>添加</span></a></li>
            <li <%=CheckFunOutDisplay(FunType.Delete) %>><a class="delete" href="#" onclick="$.RockDwz.OpenAlertWindowTodoDeleteToOptions('Ajax/SysManager/UserInfoService.ashx?action=Delete','UserID','确认要删除此记录吗！')">
                <span>删除</span></a></li>
        </ul>
    </div>
    <table class="list" width="100%" layouth="120">
        <thead>
            <tr>
                <th style="width: 4%;">
                    <input type="checkbox" group="UserID"  class="checkboxCtrl" />
                </th>
                <th>
                    登录名
                </th>
                <th>
                    真实姓名
                </th>
                <th>
                    所属角色
                </th>
                <th>
                    所在公司
                </th>
                <th>
                    所在部门
                </th>
                <th>
                    用户电话
                </th>
                <th>
                    手机
                </th>
                <th>
                    邮件
                </th>
                <th>
                    修改
                </th>
            </tr>
        </thead>
        <tbody>
            <%                
                foreach (DataRow row in DefaultTable.Rows)
                {
            %>
            <tr>
                <td style="width: 4%;">
                    <input type="checkbox" id="UserID" name="UserID" value='<%=row["UserID"] %>' />
                </td>
                <td>
                    <%=row["UserName"] %>
                </td>
                <td>
                    <%=row["UseActurlName"] %>
                </td>
                <td>
                    <%=row["RolID"] %>
                </td>
                <td>
                    <%=row["CompanyId"] %>
                </td>
                <td>
                    <%=row["DepartmentId"] %>
                </td>
                <td>
                    <%=row["UserTel"] %>
                </td>
                <td>
                    <%=row["UserMobile"] %>
                </td>
                <td>
                    <%=row["UserEmail"]%>
                </td>
                <td <%=CheckFunOutDisplay(FunType.Update) %>>
                    <label>
                        <a title="修改" href="#" class="btnEdit" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/UserInfoEdit.aspx?UserID=<%=row["UserID"] %>','UserInfoEdit','修改')">
                        </a>
                    </label>
                </td>
            </tr>
            <%
                }
            %>
        </tbody>
    </table>
    <%=OutPutPagerNavigation(DefaultListPagination) %>
</div>
