using IFreeSql fsql = DB.Sqlite;

var isok = fsql.Ado.ExecuteConnectTest();

Console.WriteLine(isok.ToString());

