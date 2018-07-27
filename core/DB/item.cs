using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DB
{
	public class item
	{
		public table Parent { get; set; }
		public void Save() {
			Parent.Save(this);
		}
	}
}
