<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentEdit.aspx.cs" Inherits="Web.Programs.SystemManager.DepartmentEdit" %>
<%@ Import Namespace="Domain.Entities.SystemManager" %>
<%@ Import Namespace="RockFramework.DWZ" %>


<div class="pageContent">
	
	<form method="post" action="Ajax/SystemManager/DepartmentService.ashx?action=Update" class="pageForm" onsubmit="return validateCallback(this, dialogAjaxDone)">
		<input type="hidden" name="DepartmentId" value="<%=this.DefaultObject.Id %>"/>
		 <div class="pageFormContent" layoutH="58">
				
	              <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Name" size="30" value='<%=this.DefaultObject.Name %>'  class="required"  />
       </div>

        <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Code" size="30" value='<%=this.DefaultObject.Code %>'   />
       </div>

        <div class="unit">
           <label>
            ：</label>
<textarea type="text" name="Description" cols="30" rows="4"><%=this.DefaultObject.Description %></textarea>
       </div>

        <div class="unit">
           <label>
            ：</label>
            <input type="text" name="Company" size="30" value='<%=this.DefaultObject.Company %>'  class="required"  />
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

