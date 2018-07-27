using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace core.DB
{
	public class table
	{
		private Type datatype;
		public List<dynamic> data;
		
		public List<Tuple<string, string>> schema;
		public string Name { get; set; }
		public table(Type type) {
			// Initializes Initial datastructure
			datatype = type;
			data = new List<dynamic>();
			string[] names = type.ToString().Split('.');
			Name = names[names.Count() - 1];
			schema = new List<Tuple<string, string>>();
			System.Reflection.FieldInfo[] fields = type.GetFields();
			foreach (System.Reflection.FieldInfo i in fields) {
				schema.Add(new Tuple<string, string>(i.Name, FieldTypeConverter(i.FieldType)));
			}
		}
		private string FieldTypeConverter(Type type) {
			// Converts base type objects to SQLiteData
			Dictionary<Type, String> typeval = new Dictionary<Type, string>()
			{
				{ typeof(string), "TEXT" },
				//{ typeof(int), "INT8" },
				//{ typeof(bool), "INT2" },
				{ typeof(double), "INTEGER" },
				{ typeof(byte[]), "BLOB"},
				{ typeof(char[]), "BLOB"}
			};
			if (typeval.ContainsKey(type))
			{
				return typeval[type];
			}
			return type.ToString();
		}
		private string QueryFieldTypeConverter(Type type, object value) {
			Dictionary<Type, String> typeval = new Dictionary<Type, string>()
			{
				{ typeof(string), "\'{0}\'" },
				{ typeof(int), "{0}" },
				{ typeof(bool), "{0}" },
				{ typeof(char[]), "\'{0}\'"},
				{ typeof(byte[]), "\'{0}\'"}
			};
			if (typeval.ContainsKey(type))
			{
				return String.Format(typeval[type], value);
			}
			return String.Format("{0}", value);
		}
		public Dictionary<String, Object> ConvertToDictionary(object obj)
		{
			Dictionary<String, Object> dict = new Dictionary<string, object>();
			System.Reflection.FieldInfo[] fields = obj.GetType().GetFields();
			foreach (System.Reflection.FieldInfo i in fields)
			{
				dict.Add(i.Name, i.GetValue(obj));
			}
			return dict;
		}
		public void CreateTable() {
			// Creates Table if not exists
			string Query = string.Format("CREATE TABLE IF NOT EXISTS `{0}` (", Name);
			List<String> TableData = new List<string>();
			foreach (Tuple<string,string> i in schema) {
				TableData.Add(string.Format(" `{0}` {1}", i.Item1, i.Item2) );
			}
			Query = Query + String.Join(",", TableData) + " )";
			core.ExecuteNonQuery(Query);
		}
		public List<object> GetRows()
		{
			return data;
		}
		public List<dynamic> RefreshRows()
		{
			string Query = String.Format("SELECT * FROM `{0}`", Name);
			data = new List<dynamic>();

			List<object> rowlist = core.ExecuteQuery(Query);
			foreach (List<object> row in rowlist)
			{
				dynamic o = Activator.CreateInstance(datatype); // Let o be Instance Of objects
															   // Fields
				foreach (Tuple<String, object> field in row)
				{
					datatype.GetField(field.Item1).SetValue(o, field.Item2);
				}
				data.Add(o);
			}
			return data;
		}

		public List<object> RefreshRows(string ID, string value)
		{
			string Query = String.Format("SELECT * FROM `{0}` WHERE {1}='{2}'", Name, ID, value);
			data = new List<object>();

			List<object> rowlist = core.ExecuteQuery(Query);
			foreach (List<object> row in rowlist)
			{
				var o = Activator.CreateInstance(datatype); // Let o be Instance Of objects
															    // Fields
				foreach (Tuple<String, object> field in row)
				{
					datatype.GetField(field.Item1).SetValue(o, field.Item2);
				}
				data.Add(o);
			}
			return data;
		}

		public dynamic AddRow() {
			dynamic obj = Activator.CreateInstance(datatype);
			// Using Reflection sets Unique ID
			datatype.GetField("UID").SetValue(obj, Guid.NewGuid().ToString());
			data.Add(obj);
			return this.AddRow(obj);
		}
		public dynamic AddRow(object obj) {
			string Query = string.Format("INSERT INTO `{0}`(", Name);
			List<String> _values = new List<string>();
			List<String> _schema = new List<string>();
			System.Reflection.FieldInfo[] fields = obj.GetType().GetFields();
			foreach (System.Reflection.FieldInfo i in fields) {
				_schema.Add(String.Format(" `{0}`", i.Name.ToString() ) );
				_values.Add(QueryFieldTypeConverter(i.FieldType, i.GetValue(obj)));
			}
			
			Query = Query + String.Join(",", _schema);
			Query = Query + ") VALUES ( ";
			Query = Query + String.Join(",", _values);
			Query = Query + ")";
			int QueryResult = core.ExecuteNonQuery(Query);
			if (QueryResult == -1) {
				throw new Exception("SQL Error Unable to execute AddRow Invalid Data. " + Query);
			}
			return obj;
		}
		public void RemoveRow(object obj) {
			string Query = String.Format("DELETE FROM `{0}` where {1}=\'{2}\'", Name, "UID", datatype.GetField("UID").GetValue(obj).ToString());
			core.ExecuteNonQuery(Query);
			data.Remove(obj);
		}
		public void Save(object obj) {
			if (obj.GetType() != datatype) {
				throw new InvalidCastException (
					String.Format("Expected object of type {0} recieved {1}", datatype, obj.GetType())
				);
			}
			Dictionary<String, Object> dict = ConvertToDictionary(obj);
			string whereobj = String.Format("{0}=\'{1}\'", "UID", datatype.GetField("UID").GetValue(obj).ToString());
			core.Update(Name, dict, whereobj);
		}
	}
}
