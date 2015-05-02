<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="{TableName}Edit.aspx.cs" Inherits="{Web}.Programs.{ConfigNameSpace}.{TableName}Edit" %>
<%@ Import Namespace="{Domain}.Entities.{ConfigNameSpace}" %>
<%@ Import Namespace="RockFramework.DWZ" %>


<div class="pageContent">
	
	<form method="post" action="Ajax/{ConfigNameSpace}/{TableName}Service.ashx?action=Update" class="pageForm" onsubmit="return validateCallback(this, dialogAjaxDone)">
		<input type="hidden" name="{PKName}" value="<%=this.DefaultObject.Id %>"/>
		 <div class="pageFormContent" layoutH="58">
				
	      {ProPertyList}
	
			</div>
		<div class="formBar">
			<ul>
				<li><div class="buttonActive"><div class="buttonContent"><button type="submit">提交</button></div></div></li>
				<li><div class="button"><div class="buttonContent"><button type="button" class="close">取消</button></div></div></li>
			</ul>
		</div>
	</form>
</div>
