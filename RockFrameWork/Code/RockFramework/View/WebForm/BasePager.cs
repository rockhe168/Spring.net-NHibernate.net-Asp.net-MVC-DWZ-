using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RockFramework.DomainModel;

namespace RockFramework.View.WebForm
{
    /// <summary>
    /// 所有WebForm页面的基类
    /// </summary>
    public class BasePager : System.Web.UI.Page
    {
        //检查用户是否会话过期
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //检查会员是否过期
            var currentUser = HttpContext.Current.Session[SystemConstant.CurrentUserInfo] as Entity;
            if (currentUser == null)
            {
                //提示过期，并跳转到登录页面
                //Response.Write("<script type='text/javascript'>SessionOut();</script>");
                //Response.End();


                //Session.Abandon();
                //Response.Write("<script language='javascript'>alert('您登录已超时，请重新登录！');top.location.href = 'Login.aspx';</script>");
                //Response.End();
            }
        }
    }
}
