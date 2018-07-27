using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;


namespace core.DB
{
	public class connection
	{
		private SQLiteConnection database;

		public connection(string connectionstring)
		{
			database = new SQLiteConnection(connectionstring);
		}
		~connection()
		{
			database.Dispose();
		}

		public int ExecuteNonQuery(string query)
		{
			database.Open();
			SQLiteCommand call = new SQLiteCommand(query, database);
			int result = -1;
			try {
				result = call.ExecuteNonQuery();
			}
			catch { result = -1; }
			database.Close();
			return result;
		}
		public List<Object> ExecuteQuery(string query) {
			database.Open();
			List<object> result = new List<object>();
			using (SQLiteCommand cmd = new SQLiteCommand(query, database)) {
				using (SQLiteDataReader r = cmd.ExecuteReader()) {
					while (r.Read()) {
						List<Object> _obj = new List<Object>();
						for (int i = 0; i <= (r.FieldCount -1); i++) {
							_obj.Add(new Tuple<string, object>(r.GetName(i), r.IsDBNull(i) ? null : r.GetValue(i)));
						}
						result.Add(_obj);
					}
				}
			}
			database.Close();
			return result;
		}
		public int Insert(string tableName, Dictionary<String, Object> data) {
			if (data.Count < 1)
			{
				return 0;
			}
			SQLiteCommand command = new SQLiteCommand(database);
			database.Open();

			List<string> vals = new List<string>();
			foreach (KeyValuePair<string, object> val in data)
			{
				vals.Add(String.Format("{0} = @{0}", val.Key));
			}
			command.CommandText = String.Format("insert into {0} set {1} where {2}", tableName, String.Join(",",vals));
			foreach (KeyValuePair<string, object> val in data)
			{
				command.Parameters.AddWithValue(val.Key, val.Value);
			}
			int result = -1;
			try { result = command.ExecuteNonQuery(); }
			catch { result = -1; }
			database.Close();
			return result;
		}
		public int Update(string tableName, Dictionary<String, Object> data, String whereobj) {
			if (data.Count < 1) {
				return 0;
			}
			SQLiteCommand command = new SQLiteCommand(database);
			database.Open();

			List<string> vals = new List<string>();
			foreach (KeyValuePair<string, object> val in data) {
				vals.Add(String.Format("{0} = @{0}", val.Key));
			}
			command.CommandText = String.Format("update {0} set {1} where {2}", tableName, String.Join(",",vals), whereobj);
			foreach (KeyValuePair<string, object> val in data) {
				command.Parameters.AddWithValue(val.Key, val.Value);
			}
			int result = -1;
			try { result = command.ExecuteNonQuery(); }
			catch { result = -1; }
			if (result == -1) {
				throw new Exception(
					String.Format("Invalid Query: {0}", command.CommandText)
				);
			}
			database.Close();
			return result;
		}
	}	
}
