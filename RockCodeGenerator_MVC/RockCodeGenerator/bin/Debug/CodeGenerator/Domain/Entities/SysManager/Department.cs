using System;
using RockFramework.DomainModel;

namespace Domain.Entities.SysManager
{
  /// <summary>
	///Department
	/// </summary>
  [Serializable]
	public class Department : Entity
	{
		/// <summary>
///
/// </summary>
public virtual string Name { get; set; }

/// <summary>
///
/// </summary>
public virtual string Code { get; set; }

/// <summary>
///
/// </summary>
public virtual string Description { get; set; }

/// <summary>
///
/// </summary>
public virtual string Company { get; set; }

      	      	      	      	      	      	 
	}
}

