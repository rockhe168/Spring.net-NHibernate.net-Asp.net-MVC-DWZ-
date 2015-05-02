<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentAdd.aspx.cs"
    Inherits="Web.Programs.SysManager.DepartmentAdd" %>

<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<div class="pageContent">
    <form method="post" action="Ajax/SysManager/DepartmentService.ashx?action=Add" class="pageForm"
    onsubmit="return validateCallback(this, dialogAjaxDone)">
    <div class="pageFormContent" layouth="58">
        <div class="unit">
            <label>
                所属机构：</label>
            <select class="combox required" name="Company">
                <%=HtmlHelper.ComboxOutputOptions<Company>(BllObjectFactory.CompanyManager.SelectAll(), "Name", "Id")%>
            </select>
        </div>
        <div class="unit">
            <label>
                部门名称：</label>
            <input type="text" name="Name" size="30" value='' class="required" />
        </div>
        <div class="unit">
            <label>
                部门代码：</label>
            <input type="text" name="Code" size="30" value='' />
        </div>
        <div class="unit">
            <label>
                描述：</label>
            <textarea name="Description" cols="30" rows="4"></textarea>
        </div>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="submit">
                            提交</button></div>
                </div>
            </li>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="button" class="close">
                            取消</button></div>
                </div>
            </li>
        </ul>
    </div>
    </form>
</div>
