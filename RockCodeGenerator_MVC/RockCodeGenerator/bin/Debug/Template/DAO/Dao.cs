using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using {Dao}.I{Dao}.{ConfigNameSpace};
using {Domain}.Entities.{ConfigNameSpace};
using RockFramework.Repository;

namespace {Dao}.NHibernateImpl.{ConfigNameSpace}
{
    public class {TableName}Dao : NHibernateRepository<{TableName}>, I{TableName}Dao
    {
    }
}
