using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using RockCodeGenerator.Properties;

namespace RockCodeGenerator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            GlobalContext.ApplicationStartPath = Application.StartupPath;

            BindTreeView();
        }

        /// <summary>
        /// 绑定TreeView
        /// </summary>
        void BindTreeView()
        {
            List<TableContext> tableList = GlobalContext.TableContexts;

            this.tvTables.Nodes.Clear();

            foreach (var tableContext in tableList)
            {
                var node = new TreeNode()
                             {
                                 Text = tableContext.TableName,
                                 Name = tableContext.TableName
                             };
                this.tvTables.Nodes.Add(node);

            }

        }

        //一般生成
        private void btnNormalGenerator_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            //获取当前要生成的表
            List<TableContext> tableContexts = GetSelectedTableList();

            //生成代码
            foreach (var table in tableContexts)
            {
                //------------------------------->生成Domian层，Entity类
                GeneratorDomainEntity(table);
                //------------------------------->生成Domian层，Xml文件
                GeneratorDomainXml(table);
                //------------------------------->生成Dao层，接口
                GeneratorDaoIDao(table);
                //------------------------------->生成Dao层，接口实现
                GeneratorDaoDao(table);
                //------------------------------->生成Bll层，接口
                GeneratorBLLIBLL(table);
                //------------------------------->生成Bll层，接口实现
                GeneratorBLLBLL(table);
                //------------------------------->生成Web层，Ajax一般处理程序
                GeneratorWebAjaxAshx(table);
                //------------------------------->生成Web层，Ajax一般处理程序 cs
                GeneratorWebAjaxAshxcs(table);
                //------------------------------->生成Web层，Programs List Designer Cs
                GeneratorWebProgramsListDesignerCs(table);
                //------------------------------->生成Web层，Programs List Cs
                GeneratorWebProgramsListCs(table);
                //------------------------------->生成Web层，Programs List Aspx
                GeneratorWebProgramsListAspx(table);
                //------------------------------->生成Web层，Programs Edit Designer Cs
                GeneratorWebProgramsEditDesignerCs(table);
                //------------------------------->生成Web层，Programs Edit Cs
                GeneratorWebProgramsEditCs(table);
                //------------------------------->生成Web层，Programs Edit Aspx
                GeneratorWebProgramsEditAspx(table);
                //------------------------------->生成Web层，Programs Add Designer Cs
                GeneratorWebProgramsAddDesignerCs(table);
                //------------------------------->生成Web层，Programs Add Cs
                GeneratorWebProgramsAddCs(table);
                //------------------------------->生成Web层，Programs Add Aspx
                GeneratorWebProgramsAddAspx(table);
            }

            //--------------------->生成Dao层配置文件
            GeneratorDaoConfigXml(tableContexts);

            //--------------------->生成DataObjectManagerFactoryConfig.xml？？？
            GeneratorDaoDataObjectManagerFactoryConfigXml(tableContexts);

            //--------------------->生成DataObjectManagerFactory？？？
            GeneratorDaoDataObjectManagerFactory(tableContexts);

            //--------------------->生成Bll层配置文件
            GeneratorBLLConfigXml(tableContexts);

            //--------------------->生成BusinessObjectManagerFactoryConfig.xml？？？
            GeneratorBusinessObjectManagerFactoryConfigXml(tableContexts);

            //--------------------->生成BusinessObjectManagerFactory？？？
            GeneratorBusinessObjectManagerFactory(tableContexts);

            //--------------------->生成Web Page配置文件
            GeneratorWebProgramsConfigXml(tableContexts);

            //--------------------->生成Web Programs配置文件
            GeneratorWebAjaxConfigXml(tableContexts);

            //--------------------->生成 Web 自定义SQL文件
            GeneratorWebAdoTemplateSqlXml(tableContexts);

            MessageBox.Show(Resources.Main_btnNormalGenerator_Click_生成成功);
        }

        private void GeneratorWebAdoTemplateSqlXml(List<TableContext> tableContexts)
        {
            foreach (TableContext table in tableContexts)
            {
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebConfigSqlFileTemplateUrl);

                //获取模板
                StringBuilder webConfigSqlFileTemplate = Tools.GetTemplate(templatePath);
                //替换类名
                webConfigSqlFileTemplate.Replace(GlobalContext.TableName, table.TableName);

                var strList = new StringBuilder();
                var strListExcel = new StringBuilder();

                foreach (var column in table.ColumnList)
                {
                    //是否基类部分
                    if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version")
                    {
                        continue;
                    }
                    //检查是否为主键
                    if (column.IsPk)
                    {
                        webConfigSqlFileTemplate.Replace(GlobalContext.PkName, column.Name);
                        //continue;
                    }

                    strList.AppendLine("a." + column.Name + ",");
                    strListExcel.AppendLine("a." + column.Name + " as " + column.NameDescription + ",");

                }

                if (strList.ToString().Length > 0)
                {
                    var proListStr = strList.ToString();
                    var proPertyList = proListStr.Substring(0, proListStr.Length - 3);

                    var proListExcelStr = strListExcel.ToString();
                    var proPertyExcelList = proListExcelStr.Substring(0, proListExcelStr.Length - 3);

                    webConfigSqlFileTemplate.Replace(GlobalContext.ProPertyList, proPertyList);

                    webConfigSqlFileTemplate.Replace(GlobalContext.ProPertyXmlList, proPertyExcelList);

                    var nameSpace = GlobalContext.Web + ".config.sql." + GlobalContext.CurrentConfigNameSpace;

                    //写入文件的文件夹路径
                    string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                         nameSpace.Replace('.', '\\'));

                    var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + ".xml";

                    Tools.CheckDirectory(fileWriterFolderPath);

                    Tools.WriterTemplateFile(webConfigSqlFileTemplate, fileWriterFilePath);
                }
            }
        }

        /// <summary>
        /// 生成Domain（领域模型） Entity（实体类）
        /// </summary>
        /// <param name="table"></param>
        void GeneratorDomainEntity(TableContext table)
        {

            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DomainEntityTemplateUrl);

            //获取模板
            StringBuilder domainEntityTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Domain + ".Entities." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            domainEntityTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            domainEntityTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换属性列表

            var strList = new StringBuilder();

            foreach (var column in table.ColumnList)
            {
                //是否基类部分
                if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version" || column.IsPk)
                {
                    continue;
                }
                strList.AppendLine("/// <summary>");
                strList.AppendLine("///" + column.NameDescription);
                strList.AppendLine("/// </summary>");
                strList.AppendLine("public virtual " + (column.DateType.Equals("int") ? "int" : Tools.ConvertVSType(column.DateType).Name.ToLower()) + " " + column.Name + " { get; set; }");
                strList.AppendLine();
            }

            //替换属性列表
            domainEntityTemplate.Replace(GlobalContext.ProPertyList, strList.ToString());

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + ".cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainEntityTemplate, fileWriterFilePath);

        }

        /// <summary>
        /// 生产Domain（类型模型） Xml（文件）
        /// </summary>
        /// <param name="table"></param>
        void GeneratorDomainXml(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DomainXmlTemplateUrl);

            //获取模板
            StringBuilder domainXmlTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Domain + ".Entities." + GlobalContext.CurrentConfigNameSpace;

            var nameSpaceEntity = GlobalContext.Domain + ".Mappings." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            domainXmlTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            domainXmlTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            domainXmlTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);

            //替换属性列表

            var strList = new StringBuilder();

            foreach (var column in table.ColumnList)
            {
                //是否基类部分
                if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version")
                {
                    continue;
                }

                //检查是否为主键
                if (column.IsPk)
                {
                    domainXmlTemplate.Replace(GlobalContext.PkName, column.Name);
                    continue;
                }

                //length=\"36\"
                //not-null=\"true\"
                var row = "<property name=\"" + column.Name + "\" column=\"" + column.Name + "\" type=\"" + (column.DateType.Equals("int") ? "integer" : Tools.ConvertVSType(column.DateType).Name.ToLower()) + "\" {0} {1} />";
                var lengthStr = "";
                if (column.DateType.Equals("nvarchar") || column.DateType.Equals("varchar"))
                {
                    lengthStr = "length=\"" + column.Length + "\"";
                }

                var isNullStr = "";
                if (!column.IsNull)
                {
                    isNullStr = "not-null=\"true\"";
                }

                var rowFormat = string.Format(row, lengthStr, isNullStr);

                strList.AppendLine(rowFormat);
            }
            strList.AppendLine();

            //替换属性列表
            domainXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpaceEntity.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + ".hbm.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);
        }

        /// <summary>
        /// 生成Dao   IDao（Dao接口层）
        /// </summary>
        /// <param name="table"></param>
        void GeneratorDaoIDao(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoIDaoTemplateUrl);

            //获取模板
            StringBuilder daoIDaoTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Dao + ".I" + GlobalContext.Dao + "." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            daoIDaoTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            daoIDaoTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            daoIDaoTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            //替换二级命名空间
            daoIDaoTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\I" + table.TableName + "Dao.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(daoIDaoTemplate, fileWriterFilePath);

        }

        /// <summary>
        /// 生成Dao  Dao（Dao实现层）
        /// </summary>
        /// <param name="table"></param>
        void GeneratorDaoDao(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoDaoTemplateUrl);

            //获取模板
            StringBuilder daoDaoTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Dao + ".NHibernateImpl." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            daoDaoTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            daoDaoTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            daoDaoTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            daoDaoTemplate.Replace(GlobalContext.DaoTag, GlobalContext.Dao);
            //替换二级命名空间
            daoDaoTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);


            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Dao.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(daoDaoTemplate, fileWriterFilePath);
        }

        /// <summary>
        /// 生成Dao config文件
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorDaoConfigXml(List<TableContext> tableList)
        {
            //获取模板
            StringBuilder domainXmlTemplate = null;

            //检查已生存文件是否已经存在
            string templatePathGnenrator = Path.Combine(GlobalContext.ApplicationStartPath, string.Format(GlobalContext.DaoConfigTemplateUrlGenerator, GlobalContext.CurrentConfigNameSpace));

            //存在则用存在的文件作为模板
            if (File.Exists(templatePathGnenrator))
            {
                domainXmlTemplate = Tools.GetTemplate(templatePathGnenrator);
            }
            else
            {
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoConfigTemplateUrl);
                domainXmlTemplate = Tools.GetTemplate(templatePath);
            }


            foreach (var table in tableList)
            {
                //替换属性列表
                var strList = new StringBuilder();
                //strList.AppendLine();

                strList.AppendLine("<!--" + table.TableName + "-->");
                strList.AppendLine(
                    "<object id=\"" + GlobalContext.Dao + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + GlobalContext.Dao + "\" type=\"" + GlobalContext.Dao + ".NHibernateImpl." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + GlobalContext.Dao + "," + GlobalContext.Dao + "\">");
                strList.AppendLine("  <property name=\"" + GlobalContext.HibernateTemplate + "\" ref=\"" + GlobalContext.HibernateTemplate + "\" />");
                //strList.AppendLine("  <property name=\"" + GlobalContext.AdoTemplate + "\" ref=\"" + GlobalContext.AdoTemplate + "\" />");
                strList.AppendLine("</object>");

                //如果存在则、跳过此对象
                string isExistsStr = "id=\"" + GlobalContext.Dao + "." + GlobalContext.CurrentConfigNameSpace + "." +
                                     table.TableName + GlobalContext.Dao + "\"";
                if (domainXmlTemplate.ToString().Contains(isExistsStr))
                    continue;

                strList.AppendLine();

                strList.AppendLine("<!--" + GlobalContext.ProPertyXmlList + "-->");
                //替换属性列表
                domainXmlTemplate.Replace("<!--" + GlobalContext.ProPertyXmlList + "-->", strList.ToString());
            }


            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Dao + "\\Config";

            var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_DaoConfig.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);

            //覆盖项目中的文件
            Tools.ConverProjectFile("DataObjectConfigSpaceConfigPath", fileWriterFilePath, GlobalContext.CurrentConfigNameSpace + "_DaoConfig");

        }


        /// <summary>
        /// 生成DaoDataObjectManagerFactoryConfig config文件
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorDaoDataObjectManagerFactoryConfigXml(List<TableContext> tableList)
        {

            //获取模板
            StringBuilder domainXmlTemplate = null;

            //检查已生存文件是否已经存在
            string templatePathGnenrator = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoDataObjectManagerFactoryConfigUrlGenerator);

            //存在则用存在的文件作为模板
            if (File.Exists(templatePathGnenrator))
            {
                domainXmlTemplate = Tools.GetTemplate(templatePathGnenrator);
            }
            else
            {
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoDataObjectManagerFactoryConfigUrl);
                domainXmlTemplate = Tools.GetTemplate(templatePath);
            }


            //替换属性列表
            foreach (var table in tableList)
            {
                string independObject = "  <property name=\"" + table.TableName + "Dao\" ref=\"" + GlobalContext.Dao +
                                        "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Dao\" /> ";

                //如果存在则、跳过此对象
                if (domainXmlTemplate.ToString().Contains(independObject))
                    continue;


                var strList = new StringBuilder(independObject);
                strList.AppendLine("<!--{Tag}-->");

                //替换当前对象
                domainXmlTemplate.Replace("<!--{Tag}-->", strList.ToString());

            }

            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Dao + "\\Config";

            var fileWriterFilePath = fileWriterFolderPath + "\\DataObjectManagerFactoryConfig.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);

            //覆盖项目中的文件
            Tools.ConverProjectFile("DataObjectManagerFactoryConfigPath", fileWriterFilePath);
        }


        /// <summary>
        /// 生成GeneratorDaoDataObjectManagerFactory 工厂类
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorDaoDataObjectManagerFactory(List<TableContext> tableList)
        {

            //获取模板
            StringBuilder domainXmlTemplate = null;

            //检查已生存文件是否已经存在
            string templatePathGnenrator = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoDataObjectManagerFactoryUrlGenerator);

            //存在则用存在的文件作为模板
            if (File.Exists(templatePathGnenrator))
            {
                domainXmlTemplate = Tools.GetTemplate(templatePathGnenrator);
            }
            else
            {
                //从新启动模板
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoDataObjectManagerFactoryUrl);
                domainXmlTemplate = Tools.GetTemplate(templatePath);
            }


            //替换属性列表
            foreach (var table in tableList)
            {
                var strList = new StringBuilder();
                strList.AppendLine(" /// <summary>");
                strList.AppendLine("/// " + table.TableName + "Dao");
                strList.AppendLine("/// </summary>");

                var propteryStr = "public I" + table.TableName + "Dao " + table.TableName + "Dao { get; set; }";

                strList.AppendLine(propteryStr);



                //如果存在则、跳过此对象
                if (domainXmlTemplate.ToString().Contains(propteryStr))
                    continue;


                strList.AppendLine();
                strList.AppendLine("//{Tag}");

                //替换当前对象
                domainXmlTemplate.Replace("//{Tag}", strList.ToString());

            }


            #region 解决命名空间问题

            string currentConfigNameSpace = "Dao.IDao." + GlobalContext.CurrentConfigNameSpace + ";";
            //检查命名空间
            if (!domainXmlTemplate.ToString().Contains(currentConfigNameSpace))
            {
                var tempConfigNameSpace = new StringBuilder("using " + currentConfigNameSpace);
                tempConfigNameSpace.AppendLine("//{SpaceTag}");

                domainXmlTemplate.Replace("//{SpaceTag}", tempConfigNameSpace.ToString());
            }

            #endregion


            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Dao;

            var fileWriterFilePath = fileWriterFolderPath + "\\DataObjectManagerFactory.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);

            //覆盖项目中的文件
            Tools.ConverProjectFile("DataObjectManagerFactoryPath", fileWriterFilePath);
        }

        /// <summary>
        /// 生成BLL IBll（bll接口层)
        /// </summary>
        /// <param name="table"></param>
        void GeneratorBLLIBLL(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllIbllTemplateUrl);

            //获取模板
            StringBuilder daoIDaoTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            daoIDaoTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            daoIDaoTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            daoIDaoTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            //替换二级命名空间
            daoIDaoTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\I" + table.TableName + "Manager.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(daoIDaoTemplate, fileWriterFilePath);
        }

        /// <summary>
        /// 生成BLL BLL实现
        /// </summary>
        /// <param name="table"></param>
        void GeneratorBLLBLL(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllbllTemplateUrl);

            //获取模板
            StringBuilder daoIDaoTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Bll + ".Impl." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            daoIDaoTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            daoIDaoTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            daoIDaoTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            daoIDaoTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            //替换二级命名空间
            daoIDaoTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Manager.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(daoIDaoTemplate, fileWriterFilePath);
        }

        /// <summary>
        /// 生成BLL层配置文件
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorBLLConfigXml(List<TableContext> tableList)
        {

            //获取模板
            StringBuilder bllXmlTemplate = null;

            //检查已生存文件是否已经存在
            string templatePathGnenrator = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllConfigTemplateUrlGenerator);

            //存在则用存在的文件作为模板
            if (File.Exists(templatePathGnenrator))
            {
                bllXmlTemplate = Tools.GetTemplate(templatePathGnenrator);
            }
            else
            {
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllConfigTemplateUrl);
                bllXmlTemplate = Tools.GetTemplate(templatePath);
            }


            foreach (var table in tableList)
            {
                //替换属性列表
                var strList = new StringBuilder();

                strList.AppendLine("<!--" + table.TableName + "-->");
                strList.AppendLine("<object id=\"" + GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" parent=\"BaseTransactionManager\">");
                strList.AppendLine("  <property name=\"Target\">");
                strList.AppendLine("    <object type=\"" + GlobalContext.Bll + ".Impl." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager," + GlobalContext.Bll + "\">");
                strList.AppendLine("      <property name=\"CurrentDao\" ref=\"" + GlobalContext.Dao + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Dao\"/>");
                strList.AppendLine("      <property name=\"DataObjectFactory\" ref=\"Dao.DataObjectManagerFactory\"/>");
                strList.AppendLine("      <property name=\"AdoTemplate\" ref=\"AdoTemplate\" />");
                strList.AppendLine("    </object>");
                strList.AppendLine("  </property>");
                strList.AppendLine("</object>");

                //如果存在则、跳过此对象
                string isExistsStr = "id=\"" + GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\"";
                if (bllXmlTemplate.ToString().Contains(isExistsStr))
                    continue;

                strList.AppendLine();

                strList.AppendLine("<!--" + GlobalContext.ProPertyXmlList + "-->");
                //替换属性列表
                bllXmlTemplate.Replace("<!--" + GlobalContext.ProPertyXmlList + "-->", strList.ToString());
            }

            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Bll + "\\Config";

            var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_BLLConfig.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(bllXmlTemplate, fileWriterFilePath);

            //覆盖项目中的文件
            Tools.ConverProjectFile("BusinessObjectConfigSpaceConfigPath", fileWriterFilePath, GlobalContext.CurrentConfigNameSpace + "_BLLConfig");
        }

        /// <summary>
        /// 生成DaoDataObjectManagerFactoryConfig config文件
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorBusinessObjectManagerFactoryConfigXml(List<TableContext> tableList)
        {

            //获取模板
            StringBuilder domainXmlTemplate = null;

            //检查已生存文件是否已经存在
            string templatePathGnenrator = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllBusinessObjectManagerFactoryConfigUrlGenerator);

            //存在则用存在的文件作为模板
            if (File.Exists(templatePathGnenrator))
            {
                domainXmlTemplate = Tools.GetTemplate(templatePathGnenrator);
            }
            else
            {
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllBusinessObjectManagerFactoryConfigUrl);
                domainXmlTemplate = Tools.GetTemplate(templatePath);
            }


            //替换属性列表
            foreach (var table in tableList)
            {
                string independObject = "  <property name=\"" + table.TableName + "Manager\" ref=\"" + GlobalContext.Bll +
                                        "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" /> ";

                //如果存在则、跳过此对象
                if (domainXmlTemplate.ToString().Contains(independObject))
                    continue;


                var strList = new StringBuilder(independObject);
                strList.AppendLine("<!--{Tag}-->");

                //替换当前对象
                domainXmlTemplate.Replace("<!--{Tag}-->", strList.ToString());

            }

            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Bll + "\\Config";

            var fileWriterFilePath = fileWriterFolderPath + "\\BusinessObjectManagerFactoryConfig.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);

            //覆盖项目中的文件
            Tools.ConverProjectFile("BusinessObjectManagerFactoryConfig", fileWriterFilePath);
        }

        /// <summary>
        /// 生成BusinessObjectManagerFactory 工厂类
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorBusinessObjectManagerFactory(List<TableContext> tableList)
        {

            //获取模板
            StringBuilder domainXmlTemplate = null;

            //检查已生存文件是否已经存在
            string templatePathGnenrator = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllBusinessObjectManagerFactoryUrlGenerator);

            //存在则用存在的文件作为模板
            if (File.Exists(templatePathGnenrator))
            {
                domainXmlTemplate = Tools.GetTemplate(templatePathGnenrator);
            }
            else
            {
                //从新启动模板
                string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllBusinessObjectManagerFactoryUrl);
                domainXmlTemplate = Tools.GetTemplate(templatePath);
            }


            //替换属性列表
            foreach (var table in tableList)
            {
                var strList = new StringBuilder();
                strList.AppendLine(" /// <summary>");
                strList.AppendLine("/// " + table.TableName + "Manager");
                strList.AppendLine("/// </summary>");

                var propteryStr = "public I" + table.TableName + "Manager " + table.TableName + "Manager { get; set; }";

                strList.AppendLine(propteryStr);



                //如果存在则、跳过此对象
                if (domainXmlTemplate.ToString().Contains(propteryStr))
                    continue;


                strList.AppendLine();
                strList.AppendLine("//{Tag}");

                //替换当前对象
                domainXmlTemplate.Replace("//{Tag}", strList.ToString());

            }


            #region 解决命名空间问题

            string currentConfigNameSpace = "BLL." + GlobalContext.CurrentConfigNameSpace + ";";
            //检查命名空间
            if (!domainXmlTemplate.ToString().Contains(currentConfigNameSpace))
            {
                var tempConfigNameSpace = new StringBuilder("using " + currentConfigNameSpace);
                tempConfigNameSpace.AppendLine("//{SpaceTag}");

                domainXmlTemplate.Replace("//{SpaceTag}", tempConfigNameSpace.ToString());
            }

            #endregion


            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Bll;

            var fileWriterFilePath = fileWriterFolderPath + "\\BusinessObjectManagerFactory.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);

            //覆盖项目中的文件
            Tools.ConverProjectFile("BusinessObjectManagerFactory", fileWriterFilePath);
        }


        /// <summary>
        /// 生成Web Ajax一般处理程序 cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebAjaxAshxcs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebAjaxAshxCsTemplateUrl);

            //获取模板
            StringBuilder webAjaxAshxcsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Ajax." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webAjaxAshxcsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webAjaxAshxcsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webAjaxAshxcsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webAjaxAshxcsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webAjaxAshxcsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webAjaxAshxcsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webAjaxAshxcsTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Service.ashx.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webAjaxAshxcsTemplate, fileWriterFilePath);
        }

        /// <summary>
        /// 生成Web Ajax一般处理程序
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebAjaxAshx(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebAjaxAshxTemplateUrl);

            //获取模板
            StringBuilder webAjaxAshxTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Ajax." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webAjaxAshxTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webAjaxAshxTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webAjaxAshxTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webAjaxAshxTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webAjaxAshxTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webAjaxAshxTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Service.ashx";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webAjaxAshxTemplate, fileWriterFilePath);
        }

        /// <summary>
        ///  Web层 列表页 aspx   designer.cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsListDesignerCs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsListaspxDesignerCsTemplateUrl);

            //获取模板
            StringBuilder webProgramsListDesignerCsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "List.aspx.designer.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsListDesignerCsTemplate, fileWriterFilePath);
        }

        /// <summary>
        ///  Web层 列表页 aspx   .cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsListCs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsListaspxCsTemplateUrl);

            //获取模板
            StringBuilder webProgramsListDesignerCsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);
            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webProgramsListDesignerCsTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //替换搜索列表
            var strList = new StringBuilder();

            foreach (var column in table.ColumnList)
            {
                //是否基类部分
                if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version" || column.IsPk)
                {
                    continue;
                }
                strList.AppendLine("//" + column.NameDescription);
                strList.AppendLine("if (Request[\"" + column.Name + "\"] != null && !string.IsNullOrWhiteSpace(Request[\"" + column.Name + "\"]))");
                strList.AppendLine("{");

                if (column.DateType.Equals("int"))
                {
                    strList.AppendLine("WhereStr += \" and " + column.Name + " =\" + Request[\"" + column.Name + "\"];");
                }
                else if (column.DateType.Equals("varchar") || column.DateType.Equals("nvarchar"))
                {
                    strList.AppendLine("WhereStr += \" and " + column.Name + "  like '%\" + Request[\"" + column.Name + "\"]+\"%'\";");
                }

                strList.AppendLine("}");

                strList.AppendLine();
            }

            //替换属性列表
            webProgramsListDesignerCsTemplate.Replace(GlobalContext.PropertySearchListStr, strList.ToString());



            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "List.aspx.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsListDesignerCsTemplate, fileWriterFilePath);
        }


        /// <summary>
        ///  Web层 列表页 aspx   .cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsListAspx(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsListaspxTemplateUrl);

            //获取模板
            StringBuilder webProgramsListAspxTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsListAspxTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsListAspxTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsListAspxTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsListAspxTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsListAspxTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsListAspxTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);
            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webProgramsListAspxTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //替换搜索列表
            var strList = new StringBuilder();

            //表头部分
            var strTableHeaderListStr = new StringBuilder();

            //列表页内容部分
            var strTableContextListStr = new StringBuilder();

            foreach (var column in table.ColumnList)
            {
                //是否基类部分
                if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version" || column.IsPk)
                {
                    continue;
                }

                //查询部分
                strList.AppendLine("<td>");
                strList.AppendLine(column.NameDescription + "<input type=\"text\" name=\"" + column.Name +
                                   "\" value=\"<%=Request[\"" + column.Name + "\"] ?? string.Empty %>\" />");
                strList.AppendLine("</td>");

                strList.AppendLine();

                //表头部分
                strTableHeaderListStr.AppendLine("<th>");
                strTableHeaderListStr.AppendLine(column.NameDescription);
                strTableHeaderListStr.AppendLine("</th>");

                //表内容部分
                strTableContextListStr.AppendLine("<td>");
                strTableContextListStr.AppendLine("<%=model." + column.Name + " %>");
                strTableContextListStr.AppendLine("</td>");
            }

            //替换查询部分
            webProgramsListAspxTemplate.Replace(GlobalContext.PropertySearchListStr, strList.ToString());
            //表头部分
            webProgramsListAspxTemplate.Replace(GlobalContext.PropertyTabelHeaderListStr, strTableHeaderListStr.ToString());
            //内容部分
            webProgramsListAspxTemplate.Replace(GlobalContext.PropertyTabelContextListStr, strTableContextListStr.ToString());



            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "List.aspx";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsListAspxTemplate, fileWriterFilePath);
        }



        /// <summary>
        ///  Web层 修改页 aspx   designer.cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsEditDesignerCs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsEditaspxDesignerCsTemplateUrl);

            //获取模板
            StringBuilder webProgramsEditDesignerCsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Edit.aspx.designer.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsEditDesignerCsTemplate, fileWriterFilePath);
        }

        /// <summary>
        ///  Web层 修改页 aspx   .cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsEditCs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsEditaspxCsTemplateUrl);

            //获取模板
            StringBuilder webProgramsEditDesignerCsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsEditDesignerCsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);
            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webProgramsEditDesignerCsTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Edit.aspx.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsEditDesignerCsTemplate, fileWriterFilePath);
        }


        /// <summary>
        ///  Web层 修改页 aspx   .cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsEditAspx(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsEditaspxTemplateUrl);

            //获取模板
            StringBuilder webProgramsListAspxTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsListAspxTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsListAspxTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsListAspxTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsListAspxTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsListAspxTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsListAspxTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);
            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webProgramsListAspxTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //内容部分
            var strList = new StringBuilder();


            foreach (var column in table.ColumnList)
            {
                //是否基类部分
                if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version" || column.IsPk)
                {
                    continue;
                }

                //内容部分
                strList.AppendLine("        <div class=\"unit\">");
                strList.AppendLine("           <label>");
                strList.AppendLine("            " + column.NameDescription + "：</label>");
                if ((column.DateType.Equals("varchar") || column.DateType.Equals("nvarchar")) && column.Length >= 300)
                {
                    strList.AppendLine("<textarea type=\"text\" name=\"" + column.Name + "\" cols=\"30\" rows=\"4\"><%=this.DefaultObject." + column.Name + " %></textarea>");
                }
                else
                {
                    strList.AppendLine("            <input type=\"text\" name=\"" + column.Name + "\" size=\"30\" value='<%=this.DefaultObject." + column.Name + " %>'  " + (column.IsNull ? "" : "class=\"required\" ") + " />");
                }

                strList.AppendLine("       </div>");


                strList.AppendLine();


            }

            //替换内容部分
            webProgramsListAspxTemplate.Replace(GlobalContext.ProPertyList, strList.ToString());

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Edit.aspx";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsListAspxTemplate, fileWriterFilePath);
        }

        /// <summary>
        ///  Web层 Add页 aspx   designer.cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsAddDesignerCs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsAddaspxDesignerCsTemplateUrl);

            //获取模板
            StringBuilder webProgramsAddDesignerCsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Add.aspx.designer.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsAddDesignerCsTemplate, fileWriterFilePath);
        }

        /// <summary>
        ///  Web层 Add页 aspx   .cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsAddCs(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsAddaspxCsTemplateUrl);

            //获取模板
            StringBuilder webProgramsAddDesignerCsTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsAddDesignerCsTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);
            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webProgramsAddDesignerCsTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Add.aspx.cs";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsAddDesignerCsTemplate, fileWriterFilePath);
        }


        /// <summary>
        ///  Web层 Add页 aspx   .cs文件
        /// </summary>
        /// <param name="table"></param>
        void GeneratorWebProgramsAddAspx(TableContext table)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsAddaspxTemplateUrl);

            //获取模板
            StringBuilder webProgramsListAspxTemplate = Tools.GetTemplate(templatePath);

            var nameSpace = GlobalContext.Web + ".Programs." + GlobalContext.CurrentConfigNameSpace;

            //替换命名空间
            webProgramsListAspxTemplate.Replace(GlobalContext.NameSpace, nameSpace);
            //替换类名
            webProgramsListAspxTemplate.Replace(GlobalContext.TableName, table.TableName);
            //替换程序集名称
            webProgramsListAspxTemplate.Replace(GlobalContext.DomainTag, GlobalContext.Domain);
            webProgramsListAspxTemplate.Replace(GlobalContext.BllTag, GlobalContext.Bll);
            webProgramsListAspxTemplate.Replace(GlobalContext.WebTag, GlobalContext.Web);

            //替换二级命名空间
            webProgramsListAspxTemplate.Replace(GlobalContext.ConfigNameSpace, GlobalContext.CurrentConfigNameSpace);
            //替换主键
            var columnPk = table.ColumnList.FirstOrDefault(p => p.IsPk == true);
            if (columnPk != null)
            {
                webProgramsListAspxTemplate.Replace(GlobalContext.PkName, columnPk.Name);
            }

            //内容部分
            var strList = new StringBuilder();


            foreach (var column in table.ColumnList)
            {
                //是否基类部分
                if (column.Name == "CreateTime" || column.Name == "UpdateTime" || column.Name == "IsDelete" || column.Name == "CreateUser" || column.Name == "UpdateUser" || column.Name == "Version" || column.IsPk)
                {
                    continue;
                }

                //内容部分
                strList.AppendLine("        <div class=\"unit\">");
                strList.AppendLine("           <label>");
                strList.AppendLine("            " + column.NameDescription + "：</label>");
                if ((column.DateType.Equals("varchar") || column.DateType.Equals("nvarchar")) && column.Length >= 300)
                {
                    strList.AppendLine("<textarea type=\"text\" name=\"" + column.Name + "\" cols=\"30\" rows=\"4\"></textarea>");
                }
                else
                {
                    strList.AppendLine("            <input type=\"text\" name=\"" + column.Name + "\" size=\"30\" value=''  " + (column.IsNull ? "" : "class=\"required\" ") + " />");
                }

                strList.AppendLine("       </div>");


                strList.AppendLine();


            }

            //替换内容部分
            webProgramsListAspxTemplate.Replace(GlobalContext.ProPertyList, strList.ToString());

            //写入文件的文件夹路径
            string fileWriterFolderPath = Path.Combine(GlobalContext.ApplicationStartPath + "\\CodeGenerator",
                                                 nameSpace.Replace('.', '\\'));

            var fileWriterFilePath = fileWriterFolderPath + "\\" + table.TableName + "Add.aspx";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsListAspxTemplate, fileWriterFilePath);
        }

        /// <summary>
        /// 生成Ajax一般处理程序配置文件
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorWebAjaxConfigXml(List<TableContext> tableList)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebAjaxConfigFileTemplateUrl);

            //获取模板
            StringBuilder webAjaxXmlTemplate = Tools.GetTemplate(templatePath);

            //替换属性列表
            var strList = new StringBuilder();

            foreach (var table in tableList)
            {

                strList.AppendLine("<!--" + table.TableName + "-->");
                strList.AppendLine("  <object id=\"" + table.TableName + "Service\" name=\"~/Ajax/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Service.ashx\">");
                strList.AppendLine("     <property name=\"BllObjectFactory\" ref=\"BLL.BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");

                strList.AppendLine();
            }
            strList.AppendLine();

            //替换属性列表
            webAjaxXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Web + "\\config\\serviceIoc";

            var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_ServiceConfig.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webAjaxXmlTemplate, fileWriterFilePath);
        }



        /// <summary>
        /// 生成页面配置文件
        /// </summary>
        /// <param name="tableList"></param>
        void GeneratorWebProgramsConfigXml(List<TableContext> tableList)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsConfigFileTemplateUrl);

            //获取模板
            StringBuilder webProgramsXmlTemplate = Tools.GetTemplate(templatePath);

            //替换属性列表
            var strList = new StringBuilder();

            foreach (var table in tableList)
            {

                strList.AppendLine("<!--" + table.TableName + "  Start-->");
                strList.AppendLine("<object id=\"" + table.TableName + "List\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "List.aspx\">");
                strList.AppendLine("<property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");
                strList.AppendLine("<object id=\"" + table.TableName + "Edit\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Edit.aspx\">");
                strList.AppendLine("<property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");
                strList.AppendLine("<object id=\"" + table.TableName + "Add\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Add.aspx\">");
                strList.AppendLine("   <property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");

                strList.AppendLine("<!--" + table.TableName + "  End-->");
                strList.AppendLine();
            }
            strList.AppendLine();

            //替换属性列表
            webProgramsXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Web + "\\config\\pageIoc";

            var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_PageConfig.xml";

            Tools.CheckDirectory(fileWriterFolderPath);

            Tools.WriterTemplateFile(webProgramsXmlTemplate, fileWriterFilePath);
        }


        /// <summary>
        /// 获取当前选中的表
        /// </summary>
        /// <returns></returns>
        List<TableContext> GetSelectedTableList()
        {
            var list = new List<TableContext>();
            foreach (TreeNode node in this.tvTables.Nodes)
            {
                if (node.Checked)
                {
                    list.AddRange(GlobalContext.TableContexts.Where(p => p.TableName.Equals(node.Name)));
                }
            }

            return list;
        }


        /// <summary>
        /// 生成页面配置文件
        /// </summary>
        /// <param name="tableList"></param>
        string GeneratorWebProgramsConfigXmlReturnString(List<TableContext> tableList)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebProgramsConfigFileTemplateUrl);

            //获取模板
            StringBuilder webProgramsXmlTemplate = Tools.GetTemplate(templatePath);

            //替换属性列表
            var strList = new StringBuilder();

            foreach (var table in tableList)
            {

                //strList.AppendLine("<!--" + table.TableName + "  Start-->");
                //strList.AppendLine("<object id=\"" + table.TableName + "List\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "List.aspx\">");
                //strList.AppendLine("<property name=\"" + table.TableName + "Manager\" ref=\"" + GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" />");
                //strList.AppendLine("  </object>");
                //strList.AppendLine("<object id=\"" + table.TableName + "Edit\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Edit.aspx\">");
                //strList.AppendLine("<property name=\"" + table.TableName + "Manager\" ref=\"" + GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" />");
                //strList.AppendLine("  </object>");

                strList.AppendLine("<!--" + table.TableName + "  Start-->");
                strList.AppendLine("<object id=\"" + table.TableName + "List\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "List.aspx\">");
                strList.AppendLine("   <property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");
                strList.AppendLine("<object id=\"" + table.TableName + "Edit\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Edit.aspx\">");
                strList.AppendLine("   <property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");
                strList.AppendLine("<object id=\"" + table.TableName + "Add\" type=\"~/Programs/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Add.aspx\">");
                strList.AppendLine("   <property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");
                strList.AppendLine("<!--" + table.TableName + "  End-->");
                strList.AppendLine();
            }
            strList.AppendLine();

            //替换属性列表
            webProgramsXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            //string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Web + "\\config\\pageIoc";

            //var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_PageConfig.xml";

            //Tools.CheckDirectory(fileWriterFolderPath);

            //Tools.WriterTemplateFile(webProgramsXmlTemplate, fileWriterFilePath);
            return webProgramsXmlTemplate.ToString();
        }

        /// <summary>
        /// 生成Ajax一般处理程序配置文件
        /// </summary>
        /// <param name="tableList"></param>
        string GeneratorWebAjaxConfigXmlReturnString(List<TableContext> tableList)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.WebAjaxConfigFileTemplateUrl);

            //获取模板
            StringBuilder webAjaxXmlTemplate = Tools.GetTemplate(templatePath);

            //替换属性列表
            var strList = new StringBuilder();

            foreach (var table in tableList)
            {

                strList.AppendLine("<!--" + table.TableName + "-->");
                strList.AppendLine("  <object id=\"" + table.TableName + "Service\" name=\"~/Ajax/" + GlobalContext.CurrentConfigNameSpace + "/" + table.TableName + "Service.ashx\">");
                //strList.AppendLine("     <property name=\"" + table.TableName + "Manager\" ref=\"" + GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" />");
                strList.AppendLine("<property name=\"BllObjectFactory\" ref=\"" + GlobalContext.Bll + ".BusinessObjectManagerFactory\" />");
                strList.AppendLine("  </object>");

                strList.AppendLine();
            }
            strList.AppendLine();

            //替换属性列表
            webAjaxXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            //string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Web + "\\config\\serviceIoc";

            //var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_ServiceConfig.xml";

            //Tools.CheckDirectory(fileWriterFolderPath);

            //Tools.WriterTemplateFile(webAjaxXmlTemplate, fileWriterFilePath);
            return webAjaxXmlTemplate.ToString();
        }

        /// <summary>
        /// 生成BLL层配置文件
        /// </summary>
        /// <param name="tableList"></param>
        string GeneratorBLLConfigXmlReturnString(List<TableContext> tableList)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.BllConfigTemplateUrl);

            //获取模板
            StringBuilder bllXmlTemplate = Tools.GetTemplate(templatePath);

            //替换属性列表
            var strList = new StringBuilder();

            foreach (var table in tableList)
            {

                strList.AppendLine("<!--" + table.TableName + "-->");
                strList.AppendLine("<object id=\"" + GlobalContext.Bll + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" parent=\"BaseTransactionManager\">");
                strList.AppendLine("  <property name=\"Target\">");
                strList.AppendLine("    <object type=\"" + GlobalContext.Bll + ".Impl." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager," + GlobalContext.Bll + "\">");
                strList.AppendLine("      <property name=\"CurrentDao\" ref=\"" + GlobalContext.Dao + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Dao\"/>");
                strList.AppendLine("      <property name=\"DataObjectFactory\" ref=\"" + GlobalContext.Dao + ".DataObjectManagerFactory\" />");
                strList.AppendLine("      <property name=\"AdoTemplate\" ref=\"AdoTemplate\" />");
                strList.AppendLine("    </object>");
                strList.AppendLine("  </property>");
                strList.AppendLine("</object>");
                strList.AppendLine();
            }
            strList.AppendLine();

            //替换属性列表
            bllXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            //string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Bll + "\\Config";

            //var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_BLLConfig.xml";

            //Tools.CheckDirectory(fileWriterFolderPath);

            //Tools.WriterTemplateFile(bllXmlTemplate, fileWriterFilePath);

            return bllXmlTemplate.ToString();
        }

        /// <summary>
        /// 生成Dao config文件
        /// </summary>
        /// <param name="tableList"></param>
        string GeneratorDaoConfigXmlRetrunString(List<TableContext> tableList)
        {
            string templatePath = Path.Combine(GlobalContext.ApplicationStartPath, GlobalContext.DaoConfigTemplateUrl);

            //获取模板
            StringBuilder domainXmlTemplate = Tools.GetTemplate(templatePath);

            //替换属性列表
            var strList = new StringBuilder();

            foreach (var table in tableList)
            {

                strList.AppendLine("<!--" + table.TableName + "-->");
                strList.AppendLine(
                    "<object id=\"" + GlobalContext.Dao + "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + GlobalContext.Dao + "\" type=\"" + GlobalContext.Dao + ".NHibernateImpl." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + GlobalContext.Dao + "," + GlobalContext.Dao + "\">");
                strList.AppendLine("  <property name=\"" + GlobalContext.HibernateTemplate + "\" ref=\"" + GlobalContext.HibernateTemplate + "\" />");
                strList.AppendLine("</object>");
                strList.AppendLine();
            }
            strList.AppendLine();

            //替换属性列表
            domainXmlTemplate.Replace(GlobalContext.ProPertyXmlList, strList.ToString());

            //写入文件的文件夹路径
            //string fileWriterFolderPath = GlobalContext.ApplicationStartPath + "\\CodeGenerator\\" + GlobalContext.Dao + "\\Config";

            //var fileWriterFilePath = fileWriterFolderPath + "\\" + GlobalContext.CurrentConfigNameSpace + "_DaoConfig.xml";

            //Tools.CheckDirectory(fileWriterFolderPath);

            //Tools.WriterTemplateFile(domainXmlTemplate, fileWriterFilePath);
            return domainXmlTemplate.ToString();
        }

        //生成BLL配置文件
        private void btnBllConfig_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            //获取当前要生成的表
            List<TableContext> tableContexts = GetSelectedTableList();

            this.txtContext.Text = "";

            this.txtContext.Text = GeneratorBLLConfigXmlReturnString(tableContexts);
        }

        //生成Dao层配置文件
        private void btnDaoConfig_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            //获取当前要生成的表
            List<TableContext> tableContexts = GetSelectedTableList();

            this.txtContext.Text = "";

            this.txtContext.Text = GeneratorDaoConfigXmlRetrunString(tableContexts);
        }

        //生成Ajax配置文件
        private void btnAjaxConfig_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            //获取当前要生成的表
            List<TableContext> tableContexts = GetSelectedTableList();

            this.txtContext.Text = "";

            this.txtContext.Text = GeneratorWebAjaxConfigXmlReturnString(tableContexts);
        }

        //生成Web页面的配置文件
        private void btnWebConfig_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            //获取当前要生成的表
            List<TableContext> tableContexts = GetSelectedTableList();

            this.txtContext.Text = "";

            this.txtContext.Text = GeneratorWebProgramsConfigXmlReturnString(tableContexts);
        }

        //生成Dao层 工厂配置文件
        private void btnDaoFactoryConfig_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            List<TableContext> tableList = GetSelectedTableList();

            var strList = new StringBuilder();
            //替换属性列表
            foreach (var table in tableList)
            {
                strList.AppendLine("  <property name=\"" + table.TableName + "Dao\" ref=\"" + GlobalContext.Dao +
                                        "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Dao\" /> ");


                strList.AppendLine();


            }

            this.txtContext.Text = "";

            this.txtContext.Text = strList.ToString();
        }

        //生成Dao层 工厂对象
        private void btnDaoFactoryObject_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            List<TableContext> tableList = GetSelectedTableList();

            var strList = new StringBuilder();
            //替换属性列表
            foreach (var table in tableList)
            {
                strList.AppendLine(" /// <summary>");
                strList.AppendLine("/// " + table.TableName + "Dao");
                strList.AppendLine("/// </summary>");

                var propteryStr = "public I" + table.TableName + "Dao " + table.TableName + "Dao { get; set; }";

                strList.AppendLine(propteryStr);

                strList.AppendLine();


            }

            this.txtContext.Text = "";

            this.txtContext.Text = strList.ToString();
        }
        //生成BLL层 工厂配置文件
        private void btnBllFactoryConfig_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            List<TableContext> tableList = GetSelectedTableList();

            var strList = new StringBuilder();
            //替换属性列表
            foreach (var table in tableList)
            {
                strList.AppendLine("  <property name=\"" + table.TableName + "Manager\" ref=\"" + GlobalContext.Bll +
                                  "." + GlobalContext.CurrentConfigNameSpace + "." + table.TableName + "Manager\" /> ");


                strList.AppendLine();


            }

            this.txtContext.Text = "";

            this.txtContext.Text = strList.ToString();
        }
        //生成BLL层 工厂对象
        private void btnBLLFactoryObject_Click(object sender, EventArgs e)
        {
            //设置二级命名空间
            GlobalContext.CurrentConfigNameSpace = this.txtNameSpace.Text.Trim();

            List<TableContext> tableList = GetSelectedTableList();

            var strList = new StringBuilder();
            //替换属性列表
            foreach (var table in tableList)
            {
                strList.AppendLine(" /// <summary>");
                strList.AppendLine("/// " + table.TableName + "Manager");
                strList.AppendLine("/// </summary>");

                var propteryStr = "public I" + table.TableName + "Manager " + table.TableName + "Manager { get; set; }";

                strList.AppendLine(propteryStr);

                strList.AppendLine();


            }

            this.txtContext.Text = "";

            this.txtContext.Text = strList.ToString();
        }

        //生成SQL
        private void btnSQL_Click(object sender, EventArgs e)
        {

        }
    }
}
