<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoList.aspx.cs" Inherits="Web.Programs.SysManager.UserInfoList" %>
<%@ Import Namespace="BLL.Impl.SysManager" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>
<%@ Import Namespace="System.Data" %>
<form id="pagerForm" method="post" action="Programs/SysManager/UserInfoList.aspx">
<input type="hidden" name="pageNum" value="1" />
<input type="hidden" name="numPerPage" value="<%=DefaultListPagination.PageSize %>" />
<input type="hidden" name="orderField" value="<%=OrderbyStr %>" />
<input type="hidden" name="orderDirection" value="<%=OrderByDirection %>" />
</form>


<div class="pageHeader">
	<form id="pageHeader" onsubmit="return navTabSearch(this);" action="Programs/SysManager/UserInfoList.aspx" method="post">
	<div class="searchBar">
		<table class="searchContent">
			<tr>
			    
			    <td>
登录名<input type="text" name="UserName" value="<%=Request["UserName"] ?? string.Empty %>" />
</td>

<td>
登录密码<input type="text" name="UserPassword" value="<%=Request["UserPassword"] ?? string.Empty %>" />
</td>

<td>
真实姓名<input type="text" name="UseActurlName" value="<%=Request["UseActurlName"] ?? string.Empty %>" />
</td>

<td>
所属角色<input type="text" name="RolID" value="<%=Request["RolID"] ?? string.Empty %>" />
</td>

<td>
所在公司<input type="text" name="CompanyId" value="<%=Request["CompanyId"] ?? string.Empty %>" />
</td>

<td>
所在部门<input type="text" name="DepartmentId" value="<%=Request["DepartmentId"] ?? string.Empty %>" />
</td>

<td>
用户电话<input type="text" name="UserTel" value="<%=Request["UserTel"] ?? string.Empty %>" />
</td>

<td>
手机<input type="text" name="UserMobile" value="<%=Request["UserMobile"] ?? string.Empty %>" />
</td>

<td>
邮件<input type="text" name="UserEmail" value="<%=Request["UserEmail"] ?? string.Empty %>" />
</td>


				
			</tr>
		</table>
		<div class="subBar">
			<ul>
				<li><div class="buttonActive"><div class="buttonContent"><button type="submit">检索</button></div></div></li>
			</ul>
		</div>
	</div>
	</form>
</div>
<div class="pageContent">
	<div class="panelBar">
		<ul class="toolBar">
			<li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/UserInfoAdd.aspx','UserInfoAdd','新增')"><span>添加</span></a></li>
			<li <%=CheckFunOutDisplay(FunType.Delete) %>><a class="delete" href="#" onclick="$.RockDwz.OpenAlertWindowTodoDeleteToOptions('Ajax/SysManager/UserInfoService.ashx?action=Delete','UserID','确认要删除此记录吗！')"><span>删除</span></a></li>
		</ul>
	</div>
	<table class="table" width="100%" layoutH="138">
		<thead>
			<tr>
				<th style="width: 4%;">
            <input type="checkbox" name="UserID" class="checkboxCtrl"  />
        </th>
				
				<th>
登录名
</th>
<th>
登录密码
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
                
                foreach (UserInfo model in DefaultList)
                {
                  %>
                  <tr>
				                <td style="width: 4%;"> 
                          <input type="checkbox" name="UserID" value='<%=model.Id %>' />
                        </td>
                         
                        <td>
<%=model.UserName %>
</td>
<td>
<%=model.UserPassword %>
</td>
<td>
<%=model.UseActurlName %>
</td>
<td>
<%=model.RolID %>
</td>
<td>
<%=model.CompanyId %>
</td>
<td>
<%=model.DepartmentId %>
</td>
<td>
<%=model.UserTel %>
</td>
<td>
<%=model.UserMobile %>
</td>
<td>
<%=model.UserEmail %>
</td>

				        
				                <td <%=CheckFunOutDisplay(FunType.Update) %>>
                            <label><a title="修改" href="#"  class="btnEdit" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/UserInfoEdit.aspx?UserID=<%=model.Id %>','UserInfoEdit','修改')"></a></label>
                        </td>
			            </tr>
                  <%
                }
            %>
			
		</tbody>
	</table>

    <%=OutPutPagerNavigation(DefaultListPagination) %>
</div>

