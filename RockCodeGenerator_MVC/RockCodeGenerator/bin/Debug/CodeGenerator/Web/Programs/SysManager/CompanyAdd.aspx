<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyAdd.aspx.cs" Inherits="Web.Programs.SysManager.CompanyAdd" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>


<div class="pageContent">
	
	<form method="post" action="Ajax/SysManager/CompanyService.ashx?action=Add" class="pageForm" onsubmit="return validateCallback(this, dialogAjaxDone)">
		<div class="pageFormContent" layoutH="58">
			        <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Name" size="30" value=''  class="required"  />
       </div>

        <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Description" size="30" value=''   />
       </div>


		</div>
		<div class="formBar">
			<ul>
				<li><div class="buttonActive"><div class="buttonContent"><button type="submit">提交</button></div></div></li>
				<li><div class="button"><div class="buttonContent"><button type="button" class="close">取消</button></div></div></li>
			</ul>
		</div>
	</form>
</div>

