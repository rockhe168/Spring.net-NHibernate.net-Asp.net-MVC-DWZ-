using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RockFramework.Repository.Data;
using Spring.Data.Common;
using Spring.Data.Core;
using RockFramework.Repository.Config;


namespace RockFramework.Repository
{
    /// <summary>
    /// Dao操作辅助类
    /// </summary>
    public class RockAdoTemplate:AdoTemplate
    {
        #region 检查sql是否合法

        /// 判断字符串中是否有SQL攻击代码
        /// 
        /// 传入用户提交数据
        /// true-安全；false-有注入攻击现有；
        private bool ProcessSqlStr(string inputString)
        {
            //string SqlStr = @"exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            const string sqlStr = @"exec|execute|insert|delete|update|alter|create|drop|\*|chr|char|mid|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            try
            {
                if (!string.IsNullOrEmpty(inputString))
                {
                    const string strRegex = @"\b(" + sqlStr + @")\b";

                    var regex = new Regex(strRegex, RegexOptions.IgnoreCase);
                    //string s = Regex.Match(inputString).Value; 
                    if (true == regex.IsMatch(inputString))
                        return false;

                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 检查SQL是否含有非法语句
        /// </summary>
        /// <param name="whereStr">sql语句</param>
        private void CheckSQLStringFormat(string whereStr)
        {
            try
            {
                if (!ProcessSqlStr(whereStr))
                {
                    throw new Exception("抱歉，查询内容有非法字符串！");
                }
            }
            catch (Exception e)
            {
                var ae = new Exception("抱歉，查询内容有非法字符串！", e);
                throw ae;
            }
        }

        /// <summary>
        /// 格式化分页sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="currentPageNo">当前页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="whereStr">where条件</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="orderByDirection">排序方向</param>
        /// <returns>返回格式化后的SQL</returns>
        private string ReplacePaginationStr(string sql, int currentPageNo, int pageSize, string whereStr, string orderbyStr, RockFramework.Repository.Data.Direction orderByDirection)
        {
            sql = sql.Replace("@where", whereStr);
            sql = sql.Replace("@pageSize", (pageSize == 0 ? 20 : pageSize).ToString(CultureInfo.InvariantCulture));
            sql = sql.Replace("@currentPageNo", currentPageNo.ToString(CultureInfo.InvariantCulture));
            sql = sql.Replace("@orderbyStr", orderbyStr + " " + orderByDirection);

            return sql;
        }
        #endregion

        /// <summary>
        /// 根据条件统计总条数
        /// </summary>
        /// <param name="sqlKey"> 配置的SQL对应 Key名称</param>
        /// <param name="whereStr">过滤条件</param>
        /// <returns>返回查询总条数</returns>
        public int Count(string sqlKey, string whereStr)
        {
            try
            {
                var sql = SqlConfigManager.GetSQLStringByKey(sqlKey);

                //替换字符串参数
                sql = sql.Replace("@where", whereStr);

                //过滤sql是否包含sql注入
                CheckSQLStringFormat(sql);

                object count=base.ExecuteScalar(CommandType.Text, sql);

                return Convert.ToInt32(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 根据条件统计总条数
        /// </summary>
        /// <param name="sqlKey"> 配置的SQL对应 Key名称</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="parameters">参数化集合 </param>
        /// <returns>返回查询总条数</returns>
        public int Count(string sqlKey, string whereStr, IDbParameters parameters)
        {
            try
            {
                var sql = SqlConfigManager.GetSQLStringByKey(sqlKey);

                //替换字符串参数
                sql = sql.Replace("@where", whereStr);

                //过滤sql是否包含sql注入
                CheckSQLStringFormat(sql);

                object count = base.ExecuteScalar(CommandType.Text, sql, parameters);

                return Convert.ToInt32(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 根据条件查询指定页数据集合
        /// </summary>
        /// <param name="sqlKey">配置的SQL对应 Key名称</param>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize"> 每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="orderByDirection">排序方向</param>
        /// <returns>过滤后指定页数DataTable</returns>
        public DataTable SelectObjectToPagination(string sqlKey, int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection)
        {

            try
            {
                var sql = SqlConfigManager.GetSQLStringByKey(sqlKey);

                //替换字符串参数
                sql = ReplacePaginationStr(sql, currentPageNo, pageSize, whereStr, orderbyStr, orderByDirection);
                //过滤sql是否包含sql注入
                CheckSQLStringFormat(sql);

                return base.DataTableCreate(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据条件查询指定页数据集合
        /// </summary>
        /// <param name="sqlKey">配置的SQL对应 Key名称</param>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize"> 每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="orderByDirection">排序方向</param>
        /// <param name="parameters">参数化集合 </param>
        /// <returns>过滤后指定页数DataTable</returns>
        public DataTable SelectObjectToPagination(string sqlKey, int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection, IDbParameters parameters)
        {
            
            try
            {
                var sql = SqlConfigManager.GetSQLStringByKey(sqlKey);

                //替换字符串参数
                sql=ReplacePaginationStr(sql,currentPageNo,pageSize,whereStr,orderbyStr,orderByDirection);
                //过滤sql是否包含sql注入
                CheckSQLStringFormat(sql);

                return base.DataTableCreateWithParams(CommandType.Text, sql,parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// 执行 SQL key 对应的sql，并返回查询结果集DataTable
        /// </summary>
        /// <param name="sqlKey">SQL 配置文件对应的Key名称</param>
        /// <returns>查询结果集 DataTable</returns>
        public DataTable SelectDataTableBySql(string sqlKey)
        {
            var sql = SqlConfigManager.GetSQLStringByKey(sqlKey);

            return base.DataTableCreate(CommandType.Text, sql);
        }


        /// <summary>
        /// 执行 SQL key 对应的sql，并返回查询结果集DataTable
        /// </summary>
        /// <param name="sqlKey">SQL 配置文件对应的Key名称</param>
        /// <param name="parameters">参数化集合 </param>
        /// <returns>查询结果集 DataTable</returns>
        public DataTable SelectataTableBySql(string sqlKey, IDbParameters parameters)
        {
            var sql = SqlConfigManager.GetSQLStringByKey(sqlKey);

            return base.DataTableCreateWithParams(CommandType.Text, sql, parameters);
        }
    }
}
