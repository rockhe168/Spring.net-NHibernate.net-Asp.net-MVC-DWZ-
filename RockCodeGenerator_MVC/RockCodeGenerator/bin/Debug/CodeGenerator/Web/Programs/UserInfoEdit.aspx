<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoEdit.aspx.cs" Inherits="Web.Programs..UserInfoEdit" %>
<%@ Import Namespace="Domain.Entities." %>
<%@ Import Namespace="RockFramework.DWZ" %>


<div class="pageContent">
	
	<form method="post" action="Ajax//UserInfoService.ashx?action=Update" class="pageForm" onsubmit="return validateCallback(this, dialogAjaxDone)">
		<input type="hidden" name="UserID" value="<%=this.DefaultObject.Id %>"/>
		 <div class="pageFormContent" layoutH="58">
				
	              <div class="unit">
           <label>
            登录名：</label>
            <input type="text" name="UserName" size="30" value='<%=this.DefaultObject.UserName %>'  class="required"  />
       </div>

        <div class="unit">
           <label>
            登录密码：</label>
            <input type="text" name="UserPassword" size="30" value='<%=this.DefaultObject.UserPassword %>'  class="required"  />
       </div>

        <div class="unit">
           <label>
            真实姓名：</label>
            <input type="text" name="UseActurlName" size="30" value='<%=this.DefaultObject.UseActurlName %>'   />
       </div>

        <div class="unit">
           <label>
            所属角色：</label>
            <input type="text" name="RolID" size="30" value='<%=this.DefaultObject.RolID %>'   />
       </div>

        <div class="unit">
           <label>
            所在公司：</label>
            <input type="text" name="CompanyId" size="30" value='<%=this.DefaultObject.CompanyId %>'   />
       </div>

        <div class="unit">
           <label>
            所在部门：</label>
            <input type="text" name="DepartmentId" size="30" value='<%=this.DefaultObject.DepartmentId %>'   />
       </div>

        <div class="unit">
           <label>
            用户电话：</label>
            <input type="text" name="UserTel" size="30" value='<%=this.DefaultObject.UserTel %>'   />
       </div>

        <div class="unit">
           <label>
            手机：</label>
            <input type="text" name="UserMobile" size="30" value='<%=this.DefaultObject.UserMobile %>'   />
       </div>

        <div class="unit">
           <label>
            邮件：</label>
            <input type="text" name="UserEmail" size="30" value='<%=this.DefaultObject.UserEmail %>'   />
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

