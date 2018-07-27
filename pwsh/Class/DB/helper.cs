using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;



namespace pwsh.DB
{
	public static class helper
	{



		// any and all helper objcets here such as UPDATE
		public static List<object> FindByID(List<object> objlist, string key, string value) {
			List<object> result = new List<object>();
			foreach (object i in objlist) {
				Type datatype = i.GetType();
				if (datatype.GetField(key).GetValue(i).ToString() == value) {
					result.Add(i);
				}
			}
			return result;
		}

		public static void RefreshRows() {
			

		}
	}
}
