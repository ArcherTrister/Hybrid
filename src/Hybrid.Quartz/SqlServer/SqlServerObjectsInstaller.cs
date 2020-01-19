using Hybrid.Extensions;

using Microsoft.Extensions.Logging;

using Quartz.Impl.AdoJobStore;

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Hybrid.Quartz.SqlServer
{
    public class SqlServerObjectsInstaller
    {
        internal static void Initialize(string connectionString, string tablePrefix)
        {
            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var Logger = loggerFactory.CreateLogger<SqlServerObjectsInstaller>();

                if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

                Logger.LogInformation("Start installing Quartz SQL objects...");
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        //Install(connection, tablePrefix);

                        string checkTablesExists;

                        using (var cmd = new SqlCommand($"select name from sys.tables where name like '{tablePrefix}Job%';", connection))
                        {
                            checkTablesExists = (string)cmd.ExecuteScalar();//对连接执行sql语句，并返回受影响的行数
                        }

                        if (!string.IsNullOrEmpty(checkTablesExists))
                        {
                            Logger.LogInformation("DB tables already exist. Exit install");
                            return;
                        }

                        //string script = GetStringResource(
                        //    typeof(SqlServerObjectsInstaller).GetTypeInfo().Assembly,
                        //    "Hybrid.Quartz.SqlServer.Install.sql");

                        string script = typeof(SqlServerObjectsInstaller).GetTypeInfo().Assembly.GetStringByResource("Hybrid.Quartz.SqlServer.Install.sql");

                        string sql = script.Replace("$(TablePrefix)", !string.IsNullOrWhiteSpace(tablePrefix) ? tablePrefix : AdoConstants.DefaultTablePrefix);

                        using (var cmd = new SqlCommand(sql, connection))
                        {
                            cmd.ExecuteNonQuery();//对连接执行sql语句，并返回受影响的行数
                        }

                        connection.Close();
                    }
                    Logger.LogInformation("Quartz SQL objects installed.");
                }
                catch (DbException ex)
                {
                    Logger.LogError(ex, "An exception occurred while trying to perform the migration.");
                    Logger.LogError("Was unable to perform the Quartz schema migration due to an exception. Ignore this message unless you've just installed or upgraded Quartz.");
                }
            }
        }

        //private static void Install(SqlConnection connection, string tablePrefix)
        //{
        //    if (connection == null) throw new ArgumentNullException(nameof(connection));

        //    string script = GetStringResource(
        //        typeof(SqlServerObjectsInstaller).GetTypeInfo().Assembly,
        //        "Hybrid.Quartz.SqlServer.Install.sql");

        //    script = script.Replace("$(TablePrefix)", !string.IsNullOrWhiteSpace(tablePrefix) ? tablePrefix : AdoConstants.DefaultTablePrefix);

        //    connection.Execute(script, commandTimeout: 0);
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