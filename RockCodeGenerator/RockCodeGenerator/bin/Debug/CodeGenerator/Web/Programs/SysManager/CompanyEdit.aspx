<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="Web.Programs.SysManager.CompanyEdit" %>
<%@ Import Namespace="Domain.Entities.SysManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>


<div class="pageContent">
	
	<form method="post" action="Ajax/SysManager/CompanyService.ashx?action=Update" class="pageForm" onsubmit="return validateCallback(this, dialogAjaxDone)">
		<input type="hidden" name="CompanyId" value="<%=this.DefaultObject.Id %>"/>
		 <div class="pageFormContent" layoutH="58">
				
	              <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Name" size="30" value='<%=this.DefaultObject.Name %>'  class="required"  />
       </div>

        <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Description" size="30" value='<%=this.DefaultObject.Description %>'   />
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

