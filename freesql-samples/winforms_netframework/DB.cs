using System;
using System.Threading;
using System.Configuration;
namespace winforms_netframework
{
    public class DB
    {

        static readonly string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"]; //登陆用户名
        static readonly Lazy<IFreeSql> SqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
            //.UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句,Trace在输入选项卡中查看
            .UseMonitorCommand(
                cmd => Console.WriteLine("线程" + Thread.CurrentThread.ManagedThreadId + ",Sql: " + cmd.CommandText)
            )
            .UseConnectionString(FreeSql.DataType.Sqlite, ConnectionString)
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build());
        public static IFreeSql Sqlite => SqliteLazy.Value;
    }
}