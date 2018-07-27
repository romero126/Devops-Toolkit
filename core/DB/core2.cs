using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DB
{
	public static class Core2
	{
		private static connection database;
		public static void Init()
		{
			string datasource = "data.sqlite";
			string pooling = "true";
			string failifmissing = "false";
			string syncronous = "Full";
			string connectionstring = String.Format("Data Source={0};Pooling={1};FailIfMissing={2};Synchronous={3};", datasource, pooling, failifmissing, syncronous);

			if (database == null)
				database = new connection(connectionstring);
		}

		public static int ExecuteNonQuery(string query)
		{
			return database.ExecuteNonQuery(query);
		}
		public static List<object> ExecuteQuery(string query) {
			return database.ExecuteQuery(query);
		}
		public static int Update(string tableName, Dictionary<String, Object> data, String whereobj)
		{
			return database.Update(tableName, data, whereobj);
		}
	}
}
