using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DllDependencies
{
	public static class DB
	{
		public static DBConnection database;
		
		public static void init() {
			string datasource = "data.sqlite";
			string pooling = "true";
			string failifmissing = "false";
			string syncronous = "Full";
			string connectionstring = String.Format("Data Source={0};Pooling={1};FailIfMissing={2};Synchronous={3};", datasource, pooling, failifmissing, syncronous);

			if (database == null)
				database = new DBConnection(connectionstring);
		}
		public static int ExecuteNonQuery(string query) {
			return database.ExecuteNonQuery(query);
		}
	}
}
