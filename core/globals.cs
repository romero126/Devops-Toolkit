using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
	public static class globals
	{
		private static Dictionary<string, dynamic> registrar = new Dictionary<string, dynamic>();
		public static void Add(string name, dynamic value)
		{
			if (!registrar.ContainsKey(name))
				registrar.Add(name, value);
		}
		public static void Remove(string name)
		{
			if (registrar.ContainsKey(name))
				registrar.Remove(name);
		}
		public static dynamic Get(string name)
		{
			if (registrar.ContainsKey(name)) { return registrar[name]; }
			return null;
		}
		public static Dictionary<string, dynamic> GetAll()
		{
			return registrar;
		}
	}
}
