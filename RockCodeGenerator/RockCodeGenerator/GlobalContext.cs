using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockCodeGenerator
{
    /// <summary>
    /// 全局常量
    /// </summary>
    public class GlobalContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string SqlConnectionString = "";

        /// <summary>
        /// 应用程序启动跟目录
        /// </summary>
        public static string ApplicationStartPath { get; set; }

        private static List<TableContext> tableContexts = null;

        /// <summary>
        /// 获取当前所有表的集合
        /// </summary>
        public static List<TableContext> TableContexts
        {
            get
            {
                if (tableContexts != null)
                {
                    return tableContexts;
                }
                else
                {
                    tableContexts = DAOHelper.GetCurrentDatabaseAllTableContextList();
                }
                return tableContexts;
            }
            set { tableContexts = value; }
        }


        /// <summary>
        /// 获取当前的配置二级命名空间
        /// </summary>
        public static string CurrentConfigNameSpace { get; set; }

        /// <summary>
        /// HibernateTemplate模块地址
        /// </summary>
        public static string HibernateTemplate
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HibernateTemplate"]; }
        }

        /// <summary>
        /// AdoTemplate模块地址
        /// </summary>
        public static string AdoTemplate
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["AdoTemplate"]; }
        }

        /// <summary>
        /// 领域模型一级命名空间
        /// </summary>
        public static string Domain
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Domain"]; }
        }

        /// <summary>
        /// 业务逻辑层一级命名空间
        /// </summary>
        public static string Bll
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Bll"]; }
        }

        /// <summary>
        /// Web层一级命名空间
        /// </summary>
        public static string Web
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Web"]; }
        }

        /// <summary>
        /// Dao一级命名空间
        /// </summary>
        public static string Dao
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Dao"]; }
        }



        #region 模板标记常量

        /// <summary>
        /// 命名空间
        /// </summary>
        public const string NameSpace = "{NameSpace}";

        /// <summary>
        /// 二级命名空间
        /// </summary>
        public const string ConfigNameSpace = "{ConfigNameSpace}";

        /// <summary>
        /// 表名，也是类名
        /// </summary>
        public const string TableName = "{TableName}";

        /// <summary>
        /// 属性内容列表、用于实体类
        /// </summary>
        public const string ProPertyList = "{ProPertyList}";

        /// <summary>
        /// xml属性内容列表、用于xml配置文件
        /// </summary>
        public const string ProPertyXmlList = "{ProPertyXMLList}";

        /// <summary>
        /// 搜索字符串列表
        /// </summary>
        public const string PropertySearchListStr = "{PropertySearchListStr}";

        /// <summary>
        /// 列表头部部分字符串
        /// </summary>
        public const string PropertyTabelHeaderListStr = "{PropertyTabelHeaderListStr}";

        /// <summary>
        /// 列表内容部分字符串
        /// </summary>
        public const string PropertyTabelContextListStr = "{PropertyTabelContextListStr}";

        /// <summary>
        /// 主键字段名称
        /// </summary>
        public const string PkName = "{PKName}";

        /// <summary>
        /// 领域模型程序集标签名
        /// </summary>
        public const string DomainTag = "{Domain}";

        /// <summary>
        /// Dao层程序集标签名
        /// </summary>
        public const string DaoTag = "{Dao}";

        /// <summary>
        /// 业务逻辑层程序集标签名
        /// </summary>
        public const string BllTag = "{BLL}";

        /// <summary>
        /// Web 层程序集签名
        /// </summary>
        public const string WebTag = "{Web}";



        #endregion

        #region 模板地址

        /// <summary>
        /// Domain实体类模板地址
        /// </summary>
        public const string DomainEntityTemplateUrl = "Template\\Domain\\Entity.cs";

        /// <summary>
        /// Domain Xml模板地址
        /// </summary>
        public const string DomainXmlTemplateUrl = "Template\\Domain\\Xml.hbm.xml";

        /// <summary>
        /// Dao IDao模板地址
        /// </summary>
        public const string DaoIDaoTemplateUrl = "Template\\Dao\\IDao.cs";

        /// <summary>
        /// Dao层模板地址
        /// </summary>
        public const string DaoDaoTemplateUrl = "Template\\Dao\\Dao.cs";

        /// <summary>
        /// Dao层配置文件模板地址
        /// </summary>
        public const string DaoConfigTemplateUrl = "Template\\Dao\\Dao.xml";

        /// <summary>
        /// Dao层配置文件模板地址--->对应生成文件的路径
        /// </summary>
        public const string DaoConfigTemplateUrlGenerator = "CodeGenerator\\Dao\\Config\\{0}.xml";

        /// <summary>
        /// Dao层Dao对象工厂配置文件模板地址
        /// </summary>
        public const string DaoDataObjectManagerFactoryConfigUrl = "Template\\Dao\\DataObjectManagerFactoryConfig.xml";

        /// <summary>
        /// Dao层Dao对象工厂配置文件模板地址
        /// </summary>
        public const string DaoDataObjectManagerFactoryConfigUrlGenerator = "CodeGenerator\\Dao\\Config\\DataObjectManagerFactoryConfig.xml";


        /// <summary>
        /// Dao层DataObjectManagerFactory 对象
        /// </summary>
        public const string DaoDataObjectManagerFactoryUrl = "Template\\Dao\\DataObjectManagerFactory.cs";

        /// <summary>
        /// Dao层DataObjectManagerFactory 对象
        /// </summary>
        public const string DaoDataObjectManagerFactoryUrlGenerator = "CodeGenerator\\Dao\\DataObjectManagerFactory.cs";

        /// <summary>
        /// BLL层 IBLL地址
        /// </summary>
        public const string BllIbllTemplateUrl = "Template\\BLL\\IManager.cs";

        /// <summary>
        /// BLL层 BLL地址
        /// </summary>
        public const string BllbllTemplateUrl = "Template\\BLL\\Manager.cs";

        /// <summary>
        /// BLL层配置文件模板地址
        /// </summary>
        public const string BllConfigTemplateUrl = "Template\\BLL\\BLL.xml";
        /// <summary>
        /// BLL层配置文件模板地址--->对应生成文件的路径
        /// </summary>
        public const string BllConfigTemplateUrlGenerator = "Template\\BLL\\Config\\{0}.xml";

        /// <summary>
        /// BLL层对象工厂配置文件模板地址
        /// </summary>
        public const string BllBusinessObjectManagerFactoryConfigUrlGenerator = "CodeGenerator\\BLL\\Config\\BusinessObjectManagerFactoryConfig.xml";

        /// <summary>
        /// BLL层对象工厂配置文件模板地址
        /// </summary>
        public const string BllBusinessObjectManagerFactoryConfigUrl = "Template\\BLL\\BusinessObjectManagerFactoryConfig.xml";

        /// <summary>
        /// BLL层BusinessObjectManagerFactory 对象 模板地址
        /// </summary>
        public const string BllBusinessObjectManagerFactoryUrl = "Template\\BLL\\BusinessObjectManagerFactory.cs";

        /// <summary>
        /// Dao层BusinessObjectManagerFactory 对象生存地址
        /// </summary>
        public const string BllBusinessObjectManagerFactoryUrlGenerator = "CodeGenerator\\BLL\\BusinessObjectManagerFactory.cs";

        /// <summary>
        /// Web层 Ajax一般处理程序
        /// </summary>
        public const string WebAjaxAshxTemplateUrl = "Template\\Web\\Service.ashx";

        /// <summary>
        /// Web层 Ajax一般处理程序 。cs文件
        /// </summary>
        public const string WebAjaxAshxCsTemplateUrl = "Template\\Web\\Service.ashx.cs";


        /// <summary>
        /// Web层 列表页 aspx文件
        /// </summary>
        public const string WebProgramsListaspxTemplateUrl = "Template\\Web\\List.aspx";

        /// <summary>
        /// Web层 列表页 aspx   cs文件
        /// </summary>
        public const string WebProgramsListaspxCsTemplateUrl = "Template\\Web\\List.aspx.cs";

        /// <summary>
        /// Web层 列表页 aspx   designer.cs文件
        /// </summary>
        public const string WebProgramsListaspxDesignerCsTemplateUrl = "Template\\Web\\List.aspx.designer.cs";


        /// <summary>
        /// Web层 Edit页 aspx文件
        /// </summary>
        public const string WebProgramsEditaspxTemplateUrl = "Template\\Web\\Edit.aspx";

        /// <summary>
        /// Web层 Edit页 aspx   cs文件
        /// </summary>
        public const string WebProgramsEditaspxCsTemplateUrl = "Template\\Web\\Edit.aspx.cs";

        /// <summary>
        /// Web层 Edit页 aspx   designer.cs文件
        /// </summary>
        public const string WebProgramsEditaspxDesignerCsTemplateUrl = "Template\\Web\\Edit.aspx.designer.cs";

        /// <summary>
        /// Web层 Add页 aspx文件
        /// </summary>
        public const string WebProgramsAddaspxTemplateUrl = "Template\\Web\\Add.aspx";

        /// <summary>
        /// Web层 Add页 aspx   cs文件
        /// </summary>
        public const string WebProgramsAddaspxCsTemplateUrl = "Template\\Web\\Add.aspx.cs";

        /// <summary>
        /// Web层 Edit页 aspx   designer.cs文件
        /// </summary>
        public const string WebProgramsAddaspxDesignerCsTemplateUrl = "Template\\Web\\Add.aspx.designer.cs";

        /// <summary>
        /// Web Ajax一般处理程序配置文件
        /// </summary>
        public const string WebAjaxConfigFileTemplateUrl = "Template\\Web\\ServiceIoc.xml";

        /// <summary>
        /// Web 页面配置文件模板
        /// </summary>
        public const string WebProgramsConfigFileTemplateUrl = "Template\\Web\\PageIoc.xml";


        /// <summary>
        /// 用于自定义Sql 的文件模板
        /// </summary>
        public const string WebConfigSqlFileTemplateUrl = "Template\\Web\\Sql.xml";

        #endregion

    }
}
