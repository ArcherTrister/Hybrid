using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hybrid.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// Based on below link to finished this solution
    /// https://stackoverflow.com/questions/10080601/how-to-add-description-to-columns-in-entity-framework-4-3-code-first-using-migra
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class SchemaDescriptionUpdater<TContext> where TContext : DbContext
    {
        private Type contextType;
        private TContext context;
        private IDbContextTransaction transaction;
        private XmlAnnotationReader reader;

        public SchemaDescriptionUpdater(TContext context)
        {
            this.context = context;
            reader = new XmlAnnotationReader();
        }

        public SchemaDescriptionUpdater(TContext context, string xmlDocumentationPath)
        {
            this.context = context;
            reader = new XmlAnnotationReader(xmlDocumentationPath);
        }

        public void UpdateDatabaseDescriptions()
        {
            contextType = typeof(TContext);
            var props = contextType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            transaction = null;
            try
            {
                context.Database.OpenConnection();
                transaction = context.Database.BeginTransaction();
                foreach (var prop in props)
                {
                    if (prop.PropertyType.InheritsOrImplements((typeof(DbSet<>))))
                    {
                        var tableType = prop.PropertyType.GetGenericArguments()[0];
                        SetTableDescriptions(tableType);
                    }
                }
                transaction.Commit();
            }
            catch
            {
                if (transaction != null)
                    transaction.Rollback();
                throw;
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        private string GetTableName(Type tableType)
        {
            string tableName = tableType.Name;
            var customAttributes = tableType.GetCustomAttributes(typeof(TableAttribute), false);
            if (customAttributes.Count() > 0)
            {
                tableName = (customAttributes.First() as TableAttribute).Name;
            }

            return tableName;
        }

        private void SetTableDescriptions(Type tableType)
        {
            string fullTableName = GetTableName(tableType); //context.GetTableName(tableType);

            Regex regex = new Regex(@"(\[\w+\]\.)?\[(?<table>.*)\]");
            Match match = regex.Match(fullTableName);
            string tableName;
            if (match.Success)
                tableName = match.Groups["table"].Value;
            else
                tableName = fullTableName;

            var tableAttrs = tableType.GetCustomAttributes(typeof(TableAttribute), false);
            if (tableAttrs.Length > 0)
                tableName = ((TableAttribute)tableAttrs[0]).Name;

            // set the description for the table
            string tableComment = reader.GetCommentsForResource(tableType, null, XmlResourceType.Type);
            if (!string.IsNullOrEmpty(tableComment))
                SetDescriptionForObject(tableName, null, tableComment);

            // get all of the documentation for each property/column
            ObjectDocumentation[] columnComments = reader.GetCommentsForResource(tableType);
            foreach (var column in columnComments)
            {
                SetDescriptionForObject(tableName, column.PropertyName, column.Documentation);
            }
        }

        private void SetDescriptionForObject(string tableName, string columnName, string description)
        {
            string strGetDesc = "";
            // determine if there is already an extended description
            if (string.IsNullOrEmpty(columnName))
                strGetDesc = "select top 1 CONVERT(varchar(max), [value]) from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "',null,null)";
            else
                strGetDesc = "select top 1 CONVERT(varchar(max), [value]) from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "','column',null) where objname = N'" + columnName + "'";
            var prevDesc = (string)RunSqlScalar(strGetDesc);

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@table", tableName),
                new SqlParameter("@desc", description)
            };

            // is it an update, or new?
            string funcName = "sp_addextendedproperty";
            if (!string.IsNullOrEmpty(prevDesc))
                funcName = "sp_updateextendedproperty";

            string query = @"EXEC " + funcName + @" @name = N'MS_Description', @value = @desc,@level0type = N'Schema', @level0name = 'dbo',@level1type = N'Table',  @level1name = @table";

            // if a column is specified, add a column description
            if (!string.IsNullOrEmpty(columnName))
            {
                parameters.Add(new SqlParameter("@column", columnName));
                query += ", @level2type = N'Column', @level2name = @column";
            }
            RunSql(query, parameters.ToArray());
        }

        private void RunSql(string cmdText, params SqlParameter[] parameters)
        {
            context.Database.ExecuteSqlRaw(cmdText, parameters);
        }

        private object RunSqlScalar(string cmdText)
        {
            var resultParameter = new SqlParameter("@result", SqlDbType.VarChar);
            resultParameter.Size = 2000;
            resultParameter.Direction = ParameterDirection.Output;

            context.Database.ExecuteSqlRaw($"set @result = ({cmdText})", resultParameter);
            return resultParameter.Value as string;
        }
    }

    public static class ReflectionUtil
    {
        public static bool InheritsOrImplements(this Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                                   ? child.GetGenericTypeDefinition()
                                   : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                    return true;

                currentChild = currentChild.BaseType != null
                               && currentChild.BaseType.IsGenericType
                                   ? currentChild.BaseType.GetGenericTypeDefinition()
                                   : currentChild.BaseType;

                if (currentChild == null)
                    return false;
            }
            return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            var shouldUseGenericType = true;
            if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
                shouldUseGenericType = false;

            if (parent.IsGenericType && shouldUseGenericType)
                parent = parent.GetGenericTypeDefinition();
            return parent;
        }
    }

    public class XmlAnnotationReader
    {
        public string XmlPath { get; protected internal set; }
        public XmlDocument Document { get; protected internal set; }

        public XmlAnnotationReader()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = String.Format("{0}.App_Data.{0}.XML", assembly.GetName().Name);
            this.XmlPath = resourceName;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);
                    this.Document = doc;
                }
            }
        }

        public XmlAnnotationReader(string xmlPath)
        {
            this.XmlPath = xmlPath;
            if (File.Exists(xmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.XmlPath);
                this.Document = doc;
            }
            else
                throw new FileNotFoundException(String.Format("Could not find the XmlDocument at the specified path: {0}\r\nCurrent Path: {1}", xmlPath, Assembly.GetExecutingAssembly().Location));
        }

        /// <summary>
        /// Retrievethe XML comments documentation for a given resource
        /// Eg. ITN.Data.Models.Entity.TestObject.MethodName
        /// </summary>
        /// <returns></returns>
        public string GetCommentsForResource(string resourcePath, XmlResourceType type)
        {
            XmlNode node = Document.SelectSingleNode(string.Format("//member[starts-with(@name, '{0}:{1}')]/summary", GetObjectTypeChar(type), resourcePath));
            if (node != null)
            {
                string xmlResult = node.InnerText;
                string trimmedResult = Regex.Replace(xmlResult, @"\s+", " ");
                return trimmedResult;
            }
            return string.Empty;
        }

        /// <summary>
        /// Retrievethe XML comments documentation for a given resource
        /// Eg. ITN.Data.Models.Entity.TestObject.MethodName
        /// </summary>
        /// <returns></returns>
        public ObjectDocumentation[] GetCommentsForResource(Type objectType)
        {
            List<ObjectDocumentation> comments = new List<ObjectDocumentation>();
            string resourcePath = objectType.FullName;

            PropertyInfo[] properties = objectType.GetProperties();
            FieldInfo[] fields = objectType.GetFields();
            List<ObjectDocumentation> objectNames = new List<ObjectDocumentation>();
            objectNames.AddRange(properties.Select(x => new ObjectDocumentation() { PropertyName = x.Name, Type = XmlResourceType.Property }).ToList());
            objectNames.AddRange(properties.Select(x => new ObjectDocumentation() { PropertyName = x.Name, Type = XmlResourceType.Field }).ToList());

            foreach (var property in objectNames)
            {
                XmlNode node = Document.SelectSingleNode(string.Format("//member[starts-with(@name, '{0}:{1}.{2}')]/summary", GetObjectTypeChar(property.Type), resourcePath, property.PropertyName));
                if (node != null)
                {
                    string xmlResult = node.InnerText;
                    string trimmedResult = Regex.Replace(xmlResult, @"\s+", " ");
                    property.Documentation = trimmedResult;
                    comments.Add(property);
                }
            }
            return comments.ToArray();
        }

        /// <summary>
        /// Retrievethe XML comments documentation for a given resource
        /// </summary>
        /// <param name="objectType">The type of class to retrieve documenation on</param>
        /// <param name="propertyName">The name of the property in the specified class</param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public string GetCommentsForResource(Type objectType, string propertyName, XmlResourceType resourceType)
        {
            List<ObjectDocumentation> comments = new List<ObjectDocumentation>();
            string resourcePath = objectType.FullName;

            string scopedElement = resourcePath;
            if (propertyName != null && resourceType != XmlResourceType.Type)
                scopedElement += "." + propertyName;
            XmlNode node = Document.SelectSingleNode(String.Format("//member[starts-with(@name, '{0}:{1}')]/summary", GetObjectTypeChar(resourceType), scopedElement));
            if (node != null)
            {
                string xmlResult = node.InnerText;
                string trimmedResult = Regex.Replace(xmlResult, @"\s+", " ");
                return trimmedResult;
            }
            return string.Empty;
        }

        private string GetObjectTypeChar(XmlResourceType type)
        {
            switch (type)
            {
                case XmlResourceType.Field:
                    return "F";

                case XmlResourceType.Method:
                    return "M";

                case XmlResourceType.Property:
                    return "P";

                case XmlResourceType.Type:
                    return "T";
            }
            return string.Empty;
        }
    }

    public class ObjectDocumentation
    {
        public string PropertyName { get; set; }
        public string Documentation { get; set; }
        public XmlResourceType Type { get; set; }
    }

    public enum XmlResourceType
    {
        Method,
        Property,
        Field,
        Type
    }

    ///// <summary>
    ///// 在遷移文件中添加
    ///// </summary>
    ///// <returns></returns>
    //private DefaultDbContext CreateDbContext()

    //{
    //    var builder = new DbContextOptionsBuilder<DefaultDbContext>();

    //    var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

    //    abpDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString("default"));

    //    return new DefaultDbContext(builder.Options);

    //}
    // 在遷移文件中添加
    //protected override void Up(MigrationBuilder migrationBuilder)
    //{
    //    string projectName = "abpDemo.Core";

    //    SchemaDescriptionUpdater<DefaultDbContext> updater = new SchemaDescriptionUpdater<DefaultDbContext>(CreateDbContext(),

    //     $@"{Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf(@"\"))}\{projectName}\App_Data\{projectName}.xml");

    //}
}