<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="Web.Programs.SysManager.CompanyList" %>

<%@ Import Namespace="BLL.Impl.SysManager" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>

<form id="pagerForm" method="post" action="Programs/SysManager/CompanyList.aspx">
<input type="hidden" name="pageNum" value="1" />
<input type="hidden" name="numPerPage" value="<%=DefaultListPagination.PageSize %>" />
<input type="hidden" name="orderField" value="<%=OrderbyStr %>" />
<input type="hidden" name="orderDirection" value="<%=OrderByDirection %>" />
</form>
<div class="pageHeader">
    <form onsubmit="return navTabSearch(this);" action="Programs/SysManager/CompanyList.aspx"
    method="post">
    <div class="searchBar">
        <table class="searchContent">
            <tr>
                <td>
                    机构名称<input type="text" name="Name" value="<%=Request["Name"] ?? string.Empty %>" />
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
            <li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/CompanyAdd.aspx','CompanyAdd','新增')">
                <span>添加</span></a></li>
           
        </ul>
    </div>
    <table class="list" width="100%" layouth="120">
        <thead>
            <tr>
                <th style="width:15px;" valign="middle">
                    <%--<input id="chkAddAll" onclick="AllCheck('CompanyId')" type="checkbox" name="chkAddu" />--%>
                </th>
                <th>
                    机构名称
                </th>
                <th>
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
                
                foreach (Company model in DefaultList)
                {
            %>
                <tr id="<%=model.Id %>" partentId="">
                    <td style="width:15px;" valign="middle">
                        <label><a href="#" class="expand" title="<%=model.Id %>" onclick="expandClick(this)"></a></label> 
                    </td>
                    <td>
                        <%=model.Name %>
                    </td>
                    <td>
                        <%=model.Description %>
                    </td>
                    <td <%=CheckFunOutDisplay(FunType.Update) %>>
                        <label>
                            <a title="修改" href="#" class="btnEdit" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/CompanyEdit.aspx?CompanyId=<%=model.Id %>','CompanyEdit','修改')">
                            </a>
                        </label>
                    </td>
                     <td <%=CheckFunOutDisplay(FunType.Delete) %>>
                        <label>
                            <a title="删除" href="#" class="btnDel" onclick="$.RockDwz.OpenAlertWindowTodoDeleteByPK('Ajax/SysManager/CompanyService.ashx?action=DeleteByPk&CompanyId=<%=model.Id %>','确定要删除此条数据吗？')">
                            </a>
                        </label>
                    </td>
                </tr>
                
                <tr partentId="<%=model.Id %>" class="hideHtmlTag">
                    <td></td>
                    <td colspan="3">
                    <table cellpadding="0" cellspacing="0" style="margin:0px;padding:0px;width:100%">
                        <thead>
                            <tr>
                            <th>部门名称</th>
                            <th>部门代码</th>
                            <th>部门描述</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var department in model.Departments)
                               {
                            %>
                               <tr>
                                <td><%=department.Name %></td>
                                <td><%=department.Code %></td>
                                <td><%=department.Description %></td>
                               </tr>
                            <%
                               } 
                            %>
                           
                        </tbody>
                    </table>
                    </td>
                </tr>
            <%
                }
            %>
        </tbody>
    </table>
    <%=OutPutPagerNavigation() %>
</div>
