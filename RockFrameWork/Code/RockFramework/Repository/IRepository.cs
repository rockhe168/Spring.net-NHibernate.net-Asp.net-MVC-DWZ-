using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RockFramework.Repository.Data;

namespace RockFramework.Repository
{
    /// <summary>
    /// 类型顶级行为接口
    /// </summary>
    /// <typeparam name="T">类型名称</typeparam>
    public interface IRepository<T>
    {

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity">增的对象</param>
        /// <returns>返回对象主键</returns>
        object Save(T entity);

        /// <summary>
        /// 删除（从数据库中彻底删除）
        /// </summary>
        /// <param name="entity">被删对象</param>
        bool Delete(T entity);

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="whereStr">删除条件</param>
        /// <returns>true=删除成功、false=删除失败</returns>
        bool Delete(string whereStr);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">被修改的对象</param>
        /// <returns>true=删除成功、false=删除失败</returns>
        bool Update(T entity);

        /// <summary>
        /// 查询该对象
        /// </summary>
        /// <param name="entity">根据主键查询该对象</param>
        /// <returns>被查询的对象</returns>
        T Get(object id);

        /// <summary>
        /// 查询该对象
        /// </summary>
        /// <param name="entity">根据主键查询该对象</param>
        /// <returns>被查询的对象</returns>
        T Load(object id);

        /// <summary>
        /// 查询全部对象
        /// </summary>
        /// <returns>全部对象IList集合</returns>
        IList<T> SelectAll();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <returns>符合条件的结果集</returns>
        IList<T> Select(string whereStr);

        /// <summary>
        /// 条件查询 - 去掉重复项
        /// </summary>
        /// <param name="field">列名，用","分开，不带别名</param>
        /// <param name="whereStr"></param>
        /// <param name="alias">别名</param>
        IList<T> SelectDistinct(string whereStr, string field, string alias);


        /// <summary>
        /// 条件查询 - 排序
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="propertyName">排序列名</param>
        /// <param name="ascending">排序方向（true=升序、false=降序）</param>
        IList<T> SelectWithOrder(string whereStr, string propertyName, bool ascending);

        /// <summary>
        ///统计总条数
        /// </summary>
        /// <returns>总记录数</returns>
        int Count();

        /// <summary>
        /// 统计总条数--条件
        /// </summary>
        /// <param name="whereStr">条件字符串</param>
        /// <returns>总记录数</returns>
        int Count(string whereStr);

        /// <summary>
        /// 统计总条数--条件
        /// </summary>
        /// <param name="whereStr">条件字符串</param>
        /// <param name="hqlParameterDic">参数集合</param>
        /// <returns>总记录数</returns>
        int Count(string whereStr, Dictionary<string, HqlParameter> hqlParameterDic);

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize">每页多少条</param>
        /// <returns>下一页数据集合</returns>
        IList<T> SelectObjectToPagination(int currentPageNo,int pageSize);

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSiz">每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <returns></returns>
        IList<T> SelectObjectToPagination(int currentPageNo, int pageSiz, string whereStr);

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSiz">每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        ///  <param name="hqlParameterDic">参数集合</param>
        ///<returns></returns>
        IList<T> SelectObjectToPagination(int currentPageNo, int pageSiz, string whereStr, Dictionary<string, HqlParameter> hqlParameterDic);

        /// <summary>
        /// 获取指定范围数据
        /// </summary>
        /// <param name="currentPageNo">当前页面</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="orderbyStr">排序字符串 </param>
        /// <param name="orderByDirection">排序方向 </param>
        /// <returns>查询结果集</returns>
        IList<T> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr, Direction orderByDirection);

        /// <summary>
        /// 根据条件查询指定页数据集合
        /// </summary>
        /// <param name="currentPageNo">当前页码</param>
        /// <param name="pageSize"> 每页多少条</param>
        /// <param name="whereStr">过滤条件</param>
        /// <param name="orderbyStr">排序字段</param>
        /// <param name="orderByDirection">排序方向</param>
        /// <param name="hqlParameterDic">参数字典集合 </param>
        /// <returns>过滤后指定页数集合</returns>
        IList<T> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr,
                                          Direction orderByDirection, Dictionary<string, HqlParameter> hqlParameterDic);

        /// <summary>
        /// 通过存储过程查询分页信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKeyName"></param>
        /// <param name="colName">列名集合</param>
        /// <param name="orderCol">排序列名</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="currentPageNo">当前页</param>
        /// <param name="orderType">升降序，true-0为升序，false-非0为降序</param>
        /// <param name="condition">条件</param>
        /// <returns>结果集</returns>
        DataTable SelectObjectToPaginationStoredProc(string tableName, string primaryKeyName, string colName, string orderCol,int pageSize, int currentPageNo, bool orderType, string condition);

        /// <summary>
        /// 执行存储过程(返回ILIST)
        /// </summary>
        /// <param name="spName">储存过程名称</param>
        /// <param name="paramInfos">参数表</param>
        IList ExecuteStoredProc(string spName, ICollection paramInfos);
    }
}
