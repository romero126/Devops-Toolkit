using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwsh.Class.UX
{
	internal class console
	{
		private DB.pwsh DBO;
		public console(DB.pwsh obj) {
			DBO = obj;
		}
		public void UpdateObject() {
			
		}
		public void UpdateObject(DB.pwsh obj)
		{
			if (obj.UID == DBO.UID) {
				DBO = obj;
			}
		}
		public void UpdateObjects() {
			
		}
		
	}
}
