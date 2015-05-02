<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="Web.Programs.SystemManager.DepartmentList" %>
<%@ Import Namespace="BLL.Impl.SystemManager" %>
<%@ Import Namespace="Domain.Entities.SystemManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>
<form id="pagerForm" method="post" action="Programs/SystemManager/DepartmentList.aspx">
  <input type="hidden" name="status" value="${param.status}" />
	<input type="hidden" name="keywords" value="${param.keywords}" />
	<input type="hidden" name="pageNum" value="1" />
	<input type="hidden" name="numPerPage" value="${model.numPerPage}" />
	<input type="hidden" name="orderField" value="${param.orderField}" />
</form>


<div class="pageHeader">
	<form onsubmit="return navTabSearch(this);" action="Programs/SystemManager/DepartmentList.aspx" method="post">
	<div class="searchBar">
		<table class="searchContent">
			<tr>
			    
			    <td>
<input type="text" name="Name" value="<%=Request["Name"] ?? string.Empty %>" />
</td>

<td>
<input type="text" name="Code" value="<%=Request["Code"] ?? string.Empty %>" />
</td>

<td>
<input type="text" name="Description" value="<%=Request["Description"] ?? string.Empty %>" />
</td>

<td>
<input type="text" name="Company" value="<%=Request["Company"] ?? string.Empty %>" />
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
			<li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="OpenDialogWindow('Programs/SystemManager/DepartmentAdd.aspx','DepartmentAdd','新增')"><span>添加</span></a></li>
			<li <%=CheckFunOutDisplay(FunType.Delete) %>><a class="delete" href="#" onclick="OpenAlertWindowTodoDelete('Ajax/SystemManager/DepartmentService.ashx?action=Delete','DepartmentId','确认要删除此记录吗！')"><span>删除</span></a></li>
		</ul>
	</div>
	<table class="table" width="100%" layoutH="138">
		<thead>
			<tr>
				<th style="width: 4%;">
            <input id="chkAddAll" onclick="AllCheck('DepartmentId')" type="checkbox" name="chkAddu" />
        </th>
				
				<th>

</th>
<th>

</th>
<th>

</th>
<th>

</th>

				
				<th>
				修改
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
                          <input type="checkbox" id="DepartmentId" name="DepartmentId" value='<%=model.Id %>' />
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
<td>
<%=model.Company %>
</td>

				        
				                <td <%=CheckFunOutDisplay(FunType.Update) %>>
                            <label><a title="修改" href="#"  class="btnEdit" onclick="OpenDialogWindowTodoEditByPK('Programs/SystemManager/DepartmentEdit.aspx','DepartmentEdit','修改','DepartmentId')"></a></label>
                        </td>
			            </tr>
                  <%
                }
            %>
			
		</tbody>
	</table>

    <%=OutPutPagerNavigation(DefaultListPagination) %>
</div>

