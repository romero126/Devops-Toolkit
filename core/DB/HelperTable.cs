using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Reflection;

namespace core.DB
{
	public abstract class HelperTable
	{
		//public static abstract string value;

		private static Type datatype;
		private static List<object> data;
		private static List<Tuple<string, string>> schema;
		private static string Name;

		public static void SetType(Type value) {
			datatype = value;
		}
		public static void Initialize() {
			// Initialize the default data structure
			SetSchema();
		}
		private static void SetSchema() {
			data = new List<object>();
			string[] names = datatype.ToString().Split('.');
			Name = names[names.Count() - 1];
			schema = new List<Tuple<string, string>>();
			System.Reflection.FieldInfo[] fields = datatype.GetFields();
			foreach (System.Reflection.FieldInfo i in fields)
			{
				schema.Add(new Tuple<string, string>(i.Name, FieldTypeConverter(i.FieldType)));
			}
		}

		private static string FieldTypeConverter(Type type)
		{
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
		private static string QueryFieldTypeConverter(Type type, object value)
		{
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
		private static Dictionary<String, Object> ConvertToDictionary(object obj)
		{
			Dictionary<String, Object> dict = new Dictionary<string, object>();
			System.Reflection.FieldInfo[] fields = obj.GetType().GetFields();
			foreach (System.Reflection.FieldInfo i in fields)
			{
				dict.Add(i.Name, i.GetValue(obj));
			}
			return dict;
		}
		public static void CreateTable()
		{
			// Creates Table if not exists
			string Query = string.Format("CREATE TABLE IF NOT EXISTS `{0}` (", Name);
			List<String> TableData = new List<string>();
			foreach (Tuple<string, string> i in schema)
			{
				TableData.Add(string.Format(" `{0}` {1}", i.Item1, i.Item2));
			}
			Query = Query + String.Join(",", TableData) + " )";
			Core2.ExecuteNonQuery(Query);
		}
		public static List<object> RefreshRows()
		{
			string Query = String.Format("SELECT * FROM `{0}`", Name);
			return RefreshRows(Query);
		}
		public static List<object> RefreshRows(string ID, string value)
		{
			string Query = String.Format("SELECT * FROM `{0}` WHERE {1}='{2}'", Name, ID, value);
			return RefreshRows(Query);
		}
		private static List<object> RefreshRows(string query) {
			if (query == null) {
				throw new ArgumentNullException("query must equal a value");
			}
			data = new List<object>();
			List<object> rowlist = Core2.ExecuteQuery(query);
			foreach (List<object> row in rowlist)
			{
				object o = Activator.CreateInstance(datatype); // Let o be Instance Of objects
															   // Fields
				foreach (Tuple<String, object> field in row)
				{
					datatype.GetField(field.Item1).SetValue(o, field.Item2);
				}
				data.Add(o);
			}
			return data;
		}
		public static dynamic AddRow()
		{
			object obj = Activator.CreateInstance(datatype);
			// Using Reflection sets Unique ID
			datatype.GetField("UID").SetValue(obj, Guid.NewGuid().ToString());
			data.Add(obj);
			return AddRow(obj);
		}
		public static dynamic AddRow(object obj)
		{
			string Query = string.Format("INSERT INTO `{0}`(", Name);
			List<String> _values = new List<string>();
			List<String> _schema = new List<string>();
			System.Reflection.FieldInfo[] fields = obj.GetType().GetFields();
			foreach (System.Reflection.FieldInfo i in fields)
			{
				_schema.Add(String.Format(" `{0}`", i.Name.ToString()));
				_values.Add(QueryFieldTypeConverter(i.FieldType, i.GetValue(obj)));
			}
			Query = Query + String.Join(",", _schema);
			Query = Query + ") VALUES ( ";
			Query = Query + String.Join(",", _values);
			Query = Query + ")";
			int QueryResult = Core2.ExecuteNonQuery(Query);
			if (QueryResult == -1)
			{
				throw new Exception("SQL Error Unable to execute AddRow Invalid Data.");
			}
			return obj;
		}
		public static void RemoveRow(object obj)
		{
			string Query = String.Format("DELETE FROM `{0}` where {1}=\'{2}\'", Name, "UID", datatype.GetField("UID").GetValue(obj).ToString());
			Core2.ExecuteNonQuery(Query);
			data.Remove(obj);
		}
		public static void Save(object obj)
		{
			if (obj.GetType() != datatype)
			{
				throw new InvalidCastException(
					String.Format("Expected object of type {0} recieved {1}", datatype, obj.GetType())
				);
			}
			Dictionary<String, Object> dict = ConvertToDictionary(obj);
			string whereobj = String.Format("{0}=\'{1}\'", "UID", datatype.GetField("UID").GetValue(obj).ToString());
			Core2.Update(Name, dict, whereobj);
		}

		public static List<object> GetRows()
		{
			return data;
		}
	}
}
