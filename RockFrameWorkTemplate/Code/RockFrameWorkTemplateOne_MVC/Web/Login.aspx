<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>淮安市道路危险货物运输安全风险管理系统</title>
<link href="themes/css/login.css" rel="stylesheet" type="text/css" />
<script src="js/jquery-1.7.1.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    //读取VerifyCode写入Cookie的验证码
    function getCookie(name) //cookies读取
    {
        var search = name + "=";
        if (document.cookie.length > 0) {
            var offset = document.cookie.indexOf(search);
            if (offset != -1) {
                offset += search.length;
                var end = document.cookie.indexOf(";", offset);
                if (end == -1) end = document.cookie.length;
                return unescape(document.cookie.substring(offset, end));
            }
            else return "";
        }
        return "";
    }

    $(document).ready(function () {
        $("#adminLogin").click(function () {
            var username = $("#username").val();
            var password = $("#password").val();

            var code = $("#code").val();
            var verifyCode = getCookie("CheckCode");
            if (code != verifyCode) {
                //$("#msg").text("验证码错误,请重新输入");
                alert("验证码错误,请重新输入");
                return false;
            }

            var url = "Ajax/SysManager/UserInfoService.ashx?action=CheckLogin&username=" + username + "&password=" + password + "&date=" + new Date().getTime();

            var url1 = "Ajax/IntegralManager/MemberService.ashx?action=CheckLogin&username=" + username + "&password=" + password + "&date=" + new Date().getTime();

            //先企业
            $.get(url1, function (date1) {

                if (date1.indexOf("false") >= 0) {
                    //再后台
                    $.get(url, function(date) {
                        if (date == "true") {
                            window.location.href = "Index.aspx";
                        } else {
                            alert("用户名或密码错误");
                        }
                    });

                } else {
                    if (date1 == "true") {
                        window.location.href = "MemberIndex.aspx";
                    } else {
                        alert("用户名或密码错误");
                    }
                }
            });
        });
    });
</script>
</head>

<body>
	<div id="login">
		<div id="login_header">
			<h1 class="login_logo">
				<a href="http://demo.dwzjs.com"><img src="themes/default/images/login_logo.gif" /></a>
			</h1>
			<div class="login_headerContent">
				<div class="navList">
					<ul>
						<%--<li><a href="#">设为首页</a></li>
						<li><a href="http://bbs.dwzjs.com">反馈</a></li>
						<li><a href="doc/dwz-user-guide.pdf" target="_blank">帮助</a></li>--%>
					</ul>
				</div>
				<h2 class="login_title"><img src="themes/default/images/login_title.png" /></h2>
			</div>
		</div>
		<div id="login_content">
			<div class="loginForm">
				<form action="#" method="POST">
					<p>
						<label>用户名：</label>
						<input type="text" id="username" name="username" size="20" class="login_input" />
					</p>
					<p>
						<label>密码：</label>
						<input type="password" id="password" name="password" size="20" class="login_input" />
					</p>
					<p>
						<label>验证码：</label>
						<input class="code" id="code" type="text" size="4" />
						<span><img src="Programs/Tool/VerifyCode.aspx" alt="" width="75" height="24" /></span>
					</p>
					<div class="login_bar">
						<input class="sub" id="adminLogin" type="button" value=" " />
					</div>
				</form>
			</div>
			<div class="login_banner"><img src="themes/default/images/login_banner.jpg" /></div>
			<div class="login_main">
				<%--<ul class="helpList">
					<li><a href="#">下载驱动程序</a></li>
					<li><a href="#">如何安装密钥驱动程序？</a></li>
					<li><a href="#">忘记密码怎么办？</a></li>
					<li><a href="#">为什么登录失败？</a></li>
				</ul>--%>
				<div class="login_inner">
					
				</div>
			</div>
		</div>
		<div id="login_footer">
			Copyright &copy; 2012 Rock Inc. All Rights Reserved.
		</div>
	</div>
</body>
</html>