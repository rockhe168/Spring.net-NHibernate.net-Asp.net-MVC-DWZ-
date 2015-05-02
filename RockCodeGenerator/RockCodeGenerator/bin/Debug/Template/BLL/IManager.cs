using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayout;
using {Domain}.Entities.{ConfigNameSpace};
using RockFramework.Repository;

namespace {NameSpace}
{
    public interface I{TableName}Manager : IBusinessRepository<{TableName}>
    {

    }
}
