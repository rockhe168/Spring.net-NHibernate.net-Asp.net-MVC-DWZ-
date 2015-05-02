<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="{TableName}Add.aspx.cs" Inherits="{Web}.Programs.{ConfigNameSpace}.{TableName}Add" %>
<%@ Import Namespace="{Domain}.Entities.{ConfigNameSpace}" %>
<%@ Import Namespace="RockFramework.DWZ" %>


<div class="pageContent">
	
	<form method="post" action="Ajax/{ConfigNameSpace}/{TableName}Service.ashx?action=Add" class="pageForm" onsubmit="return validateCallback(this, dialogAjaxDone)">
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
