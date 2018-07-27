using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.Reflection;

namespace core.DB
{
    public static class Database
    {
        private static SQLiteConnection _database;
        static Database()
        {
            if (_database != null)
                return;
            string datasource = "data.sqlite";
            string pooling = "true";
            string failifmissing = "false";
            string syncronous = "Full";
            string connectionstring = String.Format("Data Source={0};Pooling={1};FailIfMissing={2};Synchronous={3};", datasource, pooling, failifmissing, syncronous);

            Logging.Log("Core.DB.Database", "Creating Database");
            Logging.Log("Core.DB.Database", "DataSource {0}", datasource);
            Logging.Log("Core.DB.Database", "Pooling {0}", pooling);
            Logging.Log("Core.DB.Database", "FailIfMissing {0}", failifmissing);
            Logging.Log("Core.DB.Database", "Synchronous {0}", syncronous);
            _database = new SQLiteConnection(connectionstring);
        }
        public static DbType ConvertToDbType(Type type)
        {
            var typeMap = new Dictionary<Type, dynamic>();
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(Int32)] = DbType.Int32;
            typeMap[typeof(Int16)] = DbType.Int16;
            typeMap[typeof(Int64)] = DbType.Int64;
            typeMap[typeof(Byte[])] = DbType.Binary;
            typeMap[typeof(Boolean)] = DbType.Boolean;
            typeMap[typeof(DateTime)] = DbType.DateTime2;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(Decimal)] = DbType.Decimal;
            typeMap[typeof(Double)] = DbType.VarNumeric;
            typeMap[typeof(Byte)] = DbType.Byte;
            typeMap[typeof(TimeSpan)] = DbType.Time;
            return typeMap[(type)];
        }
        public static int ExecuteNonQuery(string query)
        {
            _database.Open();
            int result;
            using (SQLiteCommand call = new SQLiteCommand(query, _database))
            {
                try
                {
                    Logging.Log("Core.DB.Database", "Query: {0}", query);
                    result = call.ExecuteNonQuery();
                    Logging.Log("Core.DB.Database", "Results: {0}", result);
                }
                catch
                {
                    result = -1;
                }
            }
            _database.Close();
            return result;
        }
        public static List<TOutput> ExecuteQuery<TOutput>(string query) where TOutput : class
        {
            List<TOutput> list = new List<TOutput>();
            Type type = typeof(TOutput);
            Logging.Log("Core.DB.Database", "<Type: {0}>", type.Name);
            List<IDictionary<string, object>> dictionaries = Database.ExecuteQuery(query);
            foreach (IDictionary<string, object> dictionary in dictionaries)
            {
                TOutput r = Activator.CreateInstance<TOutput>();
                foreach (KeyValuePair<String, Object> p in dictionary)
                    type.GetField(p.Key).SetValue(r, p.Value);
                list.Add(r);
            }
            return list;
        }
        public static List<IDictionary<string, object>> ExecuteQuery(string query)
        {
            _database.Open();
            List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
            Logging.Log("Core.DB.Database", "Query: {0}", query);
            using (SQLiteCommand call = new SQLiteCommand(query, _database))
            {
                using (SQLiteDataReader reader = call.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        for (int i = 0; i <= (reader.FieldCount - 1); i++)
                        {
                            string name = reader.GetName(i);
                            object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            if (dict.ContainsKey(name))
                                dict[name] = value;
                            else
                                dict.Add(name, value);
                        }
                        result.Add(dict);
                    }
                }
            }
            _database.Close();
            Logging.Log("Core.DB.Database", "return Results: {0}", result.Count);
            return result;
        }
        public static int CreateTable(string name, Type type, bool ifnotexists)
        {
            System.Text.StringBuilder query = new System.Text.StringBuilder();
            query.AppendFormat("CREATE TABLE {0} `{1}` ( ", ifnotexists ? "IF NOT EXISTS" : "", name);
            foreach (System.Reflection.FieldInfo field in type.GetFields())
                query.AppendFormat("`{0}` {1}, ", field.Name, ConvertToDbType(field.FieldType));
            query.Remove(query.Length - 2, 2);
            query.Append(");");
            Logging.Log("Core.DB.Database", "Query: {0}", query.ToString());
            return ExecuteNonQuery(query.ToString());
        }
        public static int Insert(string tablename, object data, string where)
        {
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                _database.Open();
                List<string> fieldNames = new List<string>();
                foreach (System.Reflection.FieldInfo i in data.GetType().GetFields())
                {
                    fieldNames.Add(i.Name);
                    command.Parameters.Add(i.Name, ConvertToDbType(i.FieldType)).Value = i.GetValue(data);
                }
                command.CommandText = string.Format("INSERT INTO `{0}` ( {1} ) VALUES (@{2}) {3}", tablename, String.Join(",", fieldNames), String.Join(",@", fieldNames), !String.IsNullOrEmpty(where) ? String.Format("WHERE {0}", where) : "");
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch
                {
                    return -1;
                }
                finally
                {
                    _database.Close();
                }
            }
        }
        public static int Update(string tablename, object data, string where)
        {
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                _database.Open();
                List<string> fieldNames = new List<string>();
                foreach (System.Reflection.FieldInfo i in data.GetType().GetFields())
                {
                    fieldNames.Add(String.Format("`{0}` = @{0}", i.Name));
                    command.Parameters.Add(i.Name, ConvertToDbType(i.FieldType)).Value = i.GetValue(data);
                }
                command.CommandText = string.Format("UPDATE `{0}` SET {1} WHERE {2}", tablename, String.Join(", ", fieldNames), where);
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch
                {
                    return -1;
                }
                finally
                {
                    _database.Close();
                }

            }
        }
        public static int RemoveRow(string tablename, string key, string value)
        {
            string query = String.Format("DELETE FROM `{0}` WHERE `{1}`=\'{2}\'", tablename, key, value);
            return core.DB.Database.ExecuteNonQuery(query);
        }
        /*
        //Static classes cannot contain Destructors.
        static ~Database()
        {
            _database.Dispose();
        }
        */
    }
}
