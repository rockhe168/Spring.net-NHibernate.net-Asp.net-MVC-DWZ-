<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs"
    Inherits="Web.Programs.SysManager.DepartmentList" %>

<%@ Import Namespace="BLL.Impl.SysManager" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>

<form id="pagerForm" method="post" action="Programs/SysManager/DepartmentList.aspx">
<input type="hidden" name="pageNum" value="1" />
<input type="hidden" name="numPerPage" value="<%=DefaultListPagination.PageSize %>" />
<input type="hidden" name="orderField" value="<%=OrderbyStr %>" />
<input type="hidden" name="orderDirection" value="<%=OrderByDirection %>" />
</form>

<div class="pageHeader">
    <form onsubmit="return navTabSearch(this);" action="Programs/SysManager/DepartmentList.aspx"
    method="post">
    <div class="searchBar">
        <table class="searchContent">
            <tr>
                <td>
                    所属机构<input type="text" name="Company" value="<%=Request["Company"] ?? string.Empty %>" />
                </td>
                <td>
                    部门名称<input type="text" name="Name" value="<%=Request["Name"] ?? string.Empty %>" />
                </td>
                <td>
                    部门代码<input type="text" name="Code" value="<%=Request["Code"] ?? string.Empty %>" />
                </td>
                <td>
                    描述<input type="text" name="Description" value="<%=Request["Description"] ?? string.Empty %>" />
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
            <li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/DepartmentAdd.aspx','DepartmentAdd','新增')">
                <span>添加</span></a></li>
            <li <%=CheckFunOutDisplay(FunType.Delete) %>><a class="delete" href="#" onclick="OpenAlertWindowTodoDelete('Ajax/SysManager/DepartmentService.ashx?action=Delete','DepartmentId','确认要删除此记录吗！')">
                <span>删除</span></a></li>
        </ul>
    </div>
    <table class="list" width="100%" layouth="120">
        <thead>
            <tr>
                <th style="width: 4%;">
                    <input type="checkbox"group="DepartmentId" class="checkboxCtrl"  />
                </th>
                <th>
                    所属机构
                </th>
                <th orderfield="Name" <%=ShowColumnHeaderOrderByDirection("Name")%>>
                    部门名称
                </th>
                <th orderfield="Code" <%=ShowColumnHeaderOrderByDirection("Code")%>>
                    部门代码
                </th>
                <th orderfield="Description" <%=ShowColumnHeaderOrderByDirection("Description")%>>
                    描述
                </th>
                <th style="width: 28px;">
                    修改
                </th>
                 <th style="width: 28px;">
                    删除
                </th>
            </tr>
        </thead>
        <tbody>
            <%
                
                foreach (Department model in DefaultList)
                {
            %>
            <tr>
                <td style="width: 4%;">
                    <input type="checkbox" name="DepartmentId" value='<%=model.Id %>' />
                </td>
                <td>
                    <%=model.Company %>
                </td>
                <td>
                    <%=model.Name %>
                </td>
                <td>
                    <%=model.Code %>
                </td>
                <td>
                    <%=model.Description %>
                </td>
                <td <%=CheckFunOutDisplay(FunType.Update) %>>
                    <label>
                        <a title="修改" href="#" class="btnEdit" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/DepartmentEdit.aspx?DepartmentId=<%=model.Id %>','DepartmentEdit','修改')">
                        </a>
                    </label>
                </td>
                <td <%=CheckFunOutDisplay(FunType.Delete) %>>
                        <label>
                            <a title="删除" href="#" class="btnDel" onclick="$.RockDwz.OpenAlertWindowTodoDeleteByPK('Ajax/SysManager/DepartmentService.ashx?action=DeleteByPk&DepartmentId=<%=model.Id %>','确定要删除此条数据吗？')">
                            </a>
                        </label>
                </td>
            </tr>
            <%
                }
            %>
        </tbody>
    </table>
    <%=OutPutPagerNavigation() %>
</div>
