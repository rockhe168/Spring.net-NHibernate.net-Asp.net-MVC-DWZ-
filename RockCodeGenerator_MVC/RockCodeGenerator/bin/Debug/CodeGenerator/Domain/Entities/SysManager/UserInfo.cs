using System;
using RockFramework.DomainModel;

namespace Domain.Entities.SysManager
{
  /// <summary>
	///UserInfo
	/// </summary>
  [Serializable]
	public class UserInfo : Entity
	{
		/// <summary>
///登录名
/// </summary>
public virtual string UserName { get; set; }

/// <summary>
///登录密码
/// </summary>
public virtual string UserPassword { get; set; }

/// <summary>
///真实姓名
/// </summary>
public virtual string UseActurlName { get; set; }

/// <summary>
///所属角色
/// </summary>
public virtual string RolID { get; set; }

/// <summary>
///所在公司
/// </summary>
public virtual string CompanyId { get; set; }

/// <summary>
///所在部门
/// </summary>
public virtual string DepartmentId { get; set; }

/// <summary>
///用户电话
/// </summary>
public virtual string UserTel { get; set; }

/// <summary>
///手机
/// </summary>
public virtual string UserMobile { get; set; }

/// <summary>
///邮件
/// </summary>
public virtual string UserEmail { get; set; }

      	      	      	      	      	      	 
	}
}

