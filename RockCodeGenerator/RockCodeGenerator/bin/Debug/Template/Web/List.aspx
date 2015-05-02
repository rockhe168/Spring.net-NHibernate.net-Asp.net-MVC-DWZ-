<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="{TableName}List.aspx.cs" Inherits="{Web}.Programs.{ConfigNameSpace}.{TableName}List" %>
<%@ Import Namespace="{BLL}.Impl.{ConfigNameSpace}" %>
<%@ Import Namespace="{Domain}.Entities.{ConfigNameSpace}" %>
<%@ Import Namespace="RockFramework.DWZ" %>
<%@ Import Namespace="RockFramework.DWZ.Constant" %>
<%@ Import Namespace="System.Data" %>
<form id="pagerForm" method="post" action="Programs/{ConfigNameSpace}/{TableName}List.aspx">
<input type="hidden" name="pageNum" value="1" />
<input type="hidden" name="numPerPage" value="<%=DefaultListPagination.PageSize %>" />
<input type="hidden" name="orderField" value="<%=OrderbyStr %>" />
<input type="hidden" name="orderDirection" value="<%=OrderByDirection %>" />
</form>


<div class="pageHeader">
	<form id="pageHeader" onsubmit="return navTabSearch(this);" action="Programs/{ConfigNameSpace}/{TableName}List.aspx" method="post">
	<div class="searchBar">
		<table class="searchContent">
			<tr>
			    
			    {PropertySearchListStr}
				
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
			<li <%=CheckFunOutDisplay(FunType.Add) %>><a class="add" href="#" onclick="$.RockDwz.OpenDialogWindow('Programs/{ConfigNameSpace}/{TableName}Add.aspx','{TableName}Add','新增')"><span>添加</span></a></li>
			<li <%=CheckFunOutDisplay(FunType.Delete) %>><a class="delete" href="#" onclick="$.RockDwz.OpenAlertWindowTodoDeleteToOptions('Ajax/{ConfigNameSpace}/{TableName}Service.ashx?action=Delete','{PKName}','确认要删除此记录吗！')"><span>删除</span></a></li>
		</ul>
	</div>
	<table class="table" width="100%" layoutH="138">
		<thead>
			<tr>
				<th style="width: 4%;">
            <input type="checkbox" group="{PKName}" class="checkboxCtrl"  />
        </th>
				
				{PropertyTabelHeaderListStr}
				
				<th>
				修改
				</th>
			</tr>
		</thead>
		<tbody>
		    
		    <%
                
                foreach ({TableName} model in DefaultList)
                {
                  %>
                  <tr>
				                <td style="width: 4%;"> 
                          <input type="checkbox" name="{PKName}" value='<%=model.Id %>' />
                        </td>
                         
                        {PropertyTabelContextListStr}
				        
				                <td <%=CheckFunOutDisplay(FunType.Update) %>>
                            <label><a title="修改" href="#"  class="btnEdit" onclick="$.RockDwz.OpenDialogWindow('Programs/{ConfigNameSpace}/{TableName}Edit.aspx?{PKName}=<%=model.Id %>','{TableName}Edit','修改')"></a></label>
                        </td>
			            </tr>
                  <%
                }
            %>
			
		</tbody>
	</table>

    <%=OutPutPagerNavigation(DefaultListPagination) %>
</div>