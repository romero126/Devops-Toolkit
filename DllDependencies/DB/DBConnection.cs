using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace DllDependencies
{
	public class DBConnection
	{
		private SQLiteConnection database;

		public DBConnection(string connectionstring) {
			database = new SQLiteConnection(connectionstring);
		}
		~DBConnection()
		{
			database.Dispose();
		}
		public int ExecuteNonQuery(string query)
		{
			database.Open();

			SQLiteCommand call = new SQLiteCommand(query, database);
			int result = -1;
			//try {
				result = call.ExecuteNonQuery();
			//}
			//catch { }
			database.Close();
			return result;
		}
	}
}
