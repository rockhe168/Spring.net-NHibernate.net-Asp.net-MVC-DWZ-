using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace RockCodeGenerator
{
    /// <summary>
    /// 数据库访问层
    /// </summary>
    public class DAOHelper
    {
        //private static Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("sqlConnectionString");
        private static Database _db =EnterpriseLibraryContainer.Current.GetInstance<Database>(GlobalContext.SqlConnectionString);

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        public static List<DataBaseContext> GetAllDataBase()
        {
            var list = new List<DataBaseContext>();

            const string sql = @"SELECT name FROM master.Sys.Databases ORDER BY Name";

            var cmd = new SqlCommand(sql);

            using (var dataReader = _db.ExecuteReader(cmd))
            {
                // Processing code 
                while (dataReader.Read())
                {
                    var dataBase = new DataBaseContext()
                                       {
                                           DataBaseName = dataReader.GetString(0) //数据库名称
                                       };
                    list.Add(dataBase);

                }
            }
            return list;
        }

        /// <summary>
        /// 获取当前连接所有表集合
        /// </summary>
        /// <returns></returns>
        public static  List<TableContext> GetCurrentDatabaseAllTableContextList()
        {
            var list = new List<TableContext>();
            const string sql = @" SELECT name FROM sysobjects WHERE xtype ='u' order by name";

            var cmd = new SqlCommand(sql);

            using (var dataReader = _db.ExecuteReader(cmd))
            {
                // Processing code 
                while (dataReader.Read())
                {
                    var table = new TableContext();

                    table.TableName = dataReader.GetString(0);
                    table.ColumnList = GetColumnContextListByTableName(table.TableName);

                    list.Add(table);

                }
            }

            return list;
        }

        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllViewList()
        {
            var list=new List<string>();

            const string sql = @"SELECT name FROM sysobjects WHERE xtype ='v' order by name";

            var cmd = new SqlCommand(sql);

            using (var dataReader=_db.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    list.Add(dataReader.GetString(0));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取储存过程列表
        /// </summary>
        /// <returns></returns>
        public static  List<string> GetProcedureList()
        {
            var list = new List<string>();

            const string sql = @"SELECT name FROM sysobjects WHERE xtype ='p' order by name";

            var cmd = new SqlCommand(sql);

            using (var dataReader=_db.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    list.Add(dataReader.GetString(0));
                }
            }

            return list;
        }

        /// <summary>
        /// 根据表名获取所有字段集合
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static  List<ColumnContext>  GetColumnContextListByTableName(string tableName)
        {
            var  list=new List<ColumnContext>();

            var sql = @"	select
	col.COLUMN_NAME as columnName,
	(case when PKeyCol.COLUMN_NAME is null then '' else 'PK' end) +
	(case when KeyCol2.COLUMN_NAME is null then '' 
	      when NOT PKeyCol.COLUMN_NAME is null then ',FK' else 'FK' end) as kk,--PK=主键,FK=外键,PKFK=是主键同时外键
	col.DATA_TYPE as dataType,
	(case when CHARACTER_MAXIMUM_LENGTH is null then '' else CAST(CHARACTER_MAXIMUM_LENGTH as varchar(50)) end) as columnLength,
	col.IS_NULLABLE as isNullAble,--NO=不能为空,YES=可空
	col.COLUMN_DEFAULT as defaultValue,
	ISNULL (CAST(coldesc.[value] AS nvarchar(50)) , '') AS columnDescription
	from INFORMATION_SCHEMA.COLUMNS as col 
	LEFT OUTER JOIN 
	(select COLUMN_NAME,TABLE_NAME FROM
	INFORMATION_SCHEMA.KEY_COLUMN_USAGE KeyCol 
	LEFT OUTER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RefCol ON 
	KeyCol.CONSTRAINT_CATALOG=RefCol.CONSTRAINT_CATALOG AND 
	KeyCol.CONSTRAINT_NAME=RefCol.CONSTRAINT_NAME
	WHERE RefCol.CONSTRAINT_NAME IS NULL) PKeyCol
	ON PKeyCol.COLUMN_NAME=Col.COLUMN_NAME AND PKeyCol.TABLE_NAME=Col.TABLE_NAME 
	LEFT OUTER JOIN 
	(INFORMATION_SCHEMA.KEY_COLUMN_USAGE KeyCol2 
	INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RefCol2 ON 
	KeyCol2.CONSTRAINT_CATALOG=RefCol2.CONSTRAINT_CATALOG AND 
	KeyCol2.CONSTRAINT_NAME=RefCol2.CONSTRAINT_NAME)
	ON KeyCol2.COLUMN_NAME=Col.COLUMN_NAME AND KeyCol2.TABLE_NAME=Col.TABLE_NAME 
	LEFT OUTER JOIN ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table', '"+tableName+"', 'column', default) as coldesc ON col.COLUMN_NAME = coldesc.objname COLLATE Chinese_PRC_CI_AS where col.TABLE_NAME='"+tableName+"'";

            var cmd = new SqlCommand(sql);

            using (var dataReader = _db.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    var column = new ColumnContext();

                    column.Name = dataReader.GetString(0);//列名
                    column.IsPk = dataReader.GetString(1).Equals("PK");
                    column.IsFk = dataReader.GetString(1).Equals("FK");
                    column.DateType = dataReader.GetString(2);//数据类型
                    column.Length = string.IsNullOrEmpty(dataReader.GetString(3)) ? 0 : int.Parse(dataReader.GetString(3));//数据长度
                    column.IsNull = dataReader.GetString(4).ToLower().Equals("yes");//是否为空
                    column.DefaultValue = dataReader.GetValue(5) == null ? string.Empty : dataReader.GetValue(5).ToString(); //string.IsNullOrEmpty(dataReader.GetString(5)) ? string.Empty : dataReader.GetString(5);//默认值
                    column.NameDescription = dataReader.GetString(6);//字段说明

                    list.Add(column);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据数据名获取所有表集合
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static  List<TableContext> GetTableContextListByDataBaseName(string databaseName)
        {
            var list = new List<TableContext>();
            const string sql = @" SELECT name FROM sysobjects WHERE xtype ='u' order by name";

            var cmd = new SqlCommand(sql);

            using (var dataReader = _db.ExecuteReader(cmd))
            {
                // Processing code 
                while (dataReader.Read())
                {
                    var table = new TableContext();

                    table.TableName = dataReader.GetString(0);
                    table.ColumnList = GetColumnContextListByTableName(table.TableName);

                    list.Add(table);

                }
            }

            return list;
        }
}
}
