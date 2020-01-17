using Hybrid.Extensions;

using MySql.Data.MySqlClient;

using Quartz.Impl.AdoJobStore;

using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Hybrid.Quartz.MySql
{
    public class MySqlObjectsInstaller
    {
        //TODO:ILog
        //private static readonly ILog Logger = LogProvider.For<MySqlObjectsInstaller>();

        internal static void Initialize(string connectionString, string tablePrefix)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            //Logger.Info("Start installing Quartz SQL objects...");

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    //if (TablesExists(connection, tablePrefix))
                    //{
                    //    logger.Info("DB tables already exist. Exit install");
                    //    return;
                    //}

                    //Install(connection, tablePrefix);

                    string checkTablesExists;

                    using (var cmd = new MySqlCommand($"SHOW TABLES LIKE '{tablePrefix}Job';", connection))
                    {
                        checkTablesExists = (string)cmd.ExecuteScalar();//对连接执行sql语句，并返回受影响的行数
                    }

                    if (!string.IsNullOrEmpty(checkTablesExists))
                    {
                        //Logger.Info("DB tables already exist. Exit install");
                        return;
                    }

                    //string script = AssemblyExtensions.GetStringResource(typeof(MySqlObjectsInstaller).GetTypeInfo().Assembly, "Hybrid.Quartz.MySql.Install.sql");

                    string script = typeof(MySqlObjectsInstaller).GetTypeInfo().Assembly.GetStringByResource("Hybrid.Quartz.MySql.Install.sql");

                    string sql = script.Replace("$(TablePrefix)", !string.IsNullOrWhiteSpace(tablePrefix) ? tablePrefix : AdoConstants.DefaultTablePrefix);

                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();//对连接执行sql语句，并返回受影响的行数
                    }

                    connection.Close();
                }
                //Logger.Info("Quartz SQL objects installed.");
            }
            catch (DbException)
            {
                //Logger.WarnException("An exception occurred while trying to perform the migration.", ex);
                //Logger.Warn("Was unable to perform the Quartz schema migration due to an exception. Ignore this message unless you've just installed or upgraded Quartz.");
            }
        }

        //private static bool TablesExists(MySqlConnection connection, string tablesPrefix)
        //{
        //    return connection.ExecuteScalar<string>($"SHOW TABLES LIKE '{tablesPrefix}Job';") != null;
        //}

        //private static void Install(MySqlConnection connection, string tablePrefix)
        //{
        //    if (connection == null) throw new ArgumentNullException(nameof(connection));

        //    string script = GetStringResource(typeof(MySqlObjectsInstaller).GetTypeInfo().Assembly, "Hybrid.Quartz.MySql.Install.sql");

        //    script = script.Replace("$(TablePrefix)", !string.IsNullOrWhiteSpace(tablePrefix) ? tablePrefix : AdoConstants.DefaultTablePrefix);

        //    connection.Execute(script);
        //}

        //private static string GetStringResource(Assembly assembly, string resourceName)
        //{
        //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //    {
        //        if (stream == null)
        //        {
        //            throw new InvalidOperationException(
        //                $"Requested resource `{resourceName}` was not found in the assembly `{assembly}`.");
        //        }

        //        using (var reader = new StreamReader(stream))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //    }
        //}
    }
}