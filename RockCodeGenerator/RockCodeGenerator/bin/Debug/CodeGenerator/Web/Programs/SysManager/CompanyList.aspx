<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="Web.Programs.SysManager.CompanyList" %>
<%@ Import Namespace="BLL.Impl.SysManager" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>
<%@ Import Namespace="System.Data" %>
<form id="pagerForm" method="post" action="Programs/SysManager/CompanyList.aspx">
<input type="hidden" name="pageNum" value="1" />
<input type="hidden" name="numPerPage" value="<%=DefaultListPagination.PageSize %>" />
<input type="hidden" name="orderField" value="<%=OrderbyStr %>" />
<input type="hidden" name="orderDirection" value="<%=OrderByDirection %>" />
</form>


<div class="pageHeader">
	<form id="pageHeader" onsubmit="return navTabSearch(this);" action="Programs/SysManager/CompanyList.aspx" method="post">
	<div class="searchBar">
		<table class="searchContent">
			<tr>
			    
			    <td>
<input type="text" name="Name" value="<%=Request["Name"] ?? string.Empty %>" />
</td>

<td>
<input type="text" name="Description" value="<%=Request["Description"] ?? string.Empty %>" />
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
			<li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/CompanyAdd.aspx','CompanyAdd','新增')"><span>添加</span></a></li>
			<li <%=CheckFunOutDisplay(FunType.Delete) %>><a class="delete" href="#" onclick="$.RockDwz.OpenAlertWindowTodoDeleteToOptions('Ajax/SysManager/CompanyService.ashx?action=Delete','CompanyId','确认要删除此记录吗！')"><span>删除</span></a></li>
		</ul>
	</div>
	<table class="table" width="100%" layoutH="138">
		<thead>
			<tr>
				<th style="width: 4%;">
            <input type="checkbox" name="CompanyId" class="checkboxCtrl"  />
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
                
                foreach (Company model in DefaultList)
                {
                  %>
                  <tr>
				                <td style="width: 4%;"> 
                          <input type="checkbox" name="CompanyId" value='<%=model.Id %>' />
                        </td>
                         
                        <td>
<%=model.Name %>
</td>
<td>
<%=model.Description %>
</td>

				        
				                <td <%=CheckFunOutDisplay(FunType.Update) %>>
                            <label><a title="修改" href="#"  class="btnEdit" onclick="$.RockDwz.OpenDialogWindow('Programs/SysManager/CompanyEdit.aspx?CompanyId=<%=model.Id %>','CompanyEdit','修改')"></a></label>
                        </td>
			            </tr>
                  <%
                }
            %>
			
		</tbody>
	</table>

    <%=OutPutPagerNavigation(DefaultListPagination) %>
</div>

