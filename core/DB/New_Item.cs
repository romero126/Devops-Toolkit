using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Reflection;

namespace core.DB
{
    public class Item<T> where T : class
    {
        private core.DB.Table<T> _table;
        private T _obj;
        public T BaseObject { get { return _obj; } }
        public Item(core.DB.Table<T> table, T obj)
        {
            _table = table;
            _obj = obj;
        }
		public bool ContainsKey(string name)
		{
			PropertyInfo[] properties = _obj.GetType().GetProperties();
			foreach (PropertyInfo i in properties)
				if (i.Name == name)
					return true;
			return false;
		}
		public PropertyInfo[] GetProperties() {
			return _obj.GetType().GetProperties();
		}
		public PropertyInfo GetProperty(string name) {
			return _obj.GetType().GetProperty(name);
		}
		public dynamic GetValue(string name) {
			return _obj.GetType().GetProperty(name).GetValue(_obj);
		}
		public bool ValueEquals(string name, string value)
		{
			if (!this.ContainsKey(name))
				return false;
			if (this.GetValue(name).ToString() == value)
				return true;
			return false;
		}
		public void Update()
		{
			string name = _table.GetKey();
			_obj = _table.GetRows(name, this.GetValue(name));
		}
		public void Remove()
        {
            _table.Remove(this);
        }
        public void Save()
        {
            _table.Update(this);
        }
    }
}
