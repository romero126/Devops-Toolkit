using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DB
{
    public class Table<T> where T : class
    {
        private string _tablename;
        private string _key;
        private string _filter;
        private string _filterkey;
        private string _filtervalue;
        private List<core.DB.Item<T>> _rows;
        public Table(string name, string key)
        {
            _tablename = name;
            _key = key;
        }
        public string GetKey()
        {
            return _key;
        }
        public void DropTable()
        {
            core.DB.Database.ExecuteNonQuery(String.Format("DROP TABLE `{0}`", _tablename));
        }
        public void CreateTable()
        {
            this.CreateTable(true);
        }
        public void CreateTable(bool ignoreExists)
        {
            core.DB.Database.CreateTable(_tablename, typeof(T), ignoreExists);
        }
        public void Insert(object item)
        {
			this.Insert(new core.DB.Item<T>(this, item as T));
		}
        public void Insert(core.DB.Item<T> item)
        {
			core.DB.Database.Insert(_tablename, item.BaseObject, null);
        }
        public void Update(object item)
        {
			this.Update(new core.DB.Item<T>(this, item as T));
		}
        public void Update(core.DB.Item<T> item)
        {
			string where = String.Format("`{0}` = {1}", _key, item.GetValue(_key));
			core.DB.Database.Update(_tablename, item.BaseObject, where);
        }
        public void Remove(object item)
        {
			this.Remove(new core.DB.Item<T>(this, item as T));
		}
        public void Remove(core.DB.Item<T> item)
        {
			core.DB.Database.RemoveRow(_tablename, _key, item.GetValue(_key));
        }
		public List<core.DB.Item<T>> GetRows() {
			return this.GetRows("");
		}
		public List<core.DB.Item<T>> GetRows(string key, string value)
		{
			return this.GetRows(string.Format("`{0}` == `{1}`", key, value));
		}
		public List<core.DB.Item<T>> GetRows(String filter)
		{
			string where = "";
			if (!string.IsNullOrEmpty(filter))
				where = string.Format("WHERE {0}", filter);
			List<T> rows = core.DB.Database.ExecuteQuery<T>(String.Format("SELECT * FROM `{0}` {1}", _tablename, where));
			List<core.DB.Item<T>> result = new List<core.DB.Item<T>>();
			foreach (T row in rows)
				result.Add(new core.DB.Item<T>(this, row));
			return result;
		}
	}
}
