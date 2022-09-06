using System;
using System.Threading;

namespace winforms_netframework
{
    public class DB
    {
        static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
            //.UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句,Trace在输入选项卡中查看
            .UseMonitorCommand(
                cmd => Console.WriteLine("线程" + Thread.CurrentThread.ManagedThreadId + ",Sql: " + cmd.CommandText)
            )
            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|/freedb.db")
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build());
        public static IFreeSql Sqlite => sqliteLazy.Value;
    }
}