using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace shell
{
	internal static class Constants
	{
        internal static core.DB.Table<Classes.DB.Shell> DB_table_shell = new core.DB.Table<Classes.DB.Shell>("shell", "ID");
		internal static core.DB.Table<Classes.DB.Code> DB_table_code = new core.DB.Table<Classes.DB.Code>("code", "ID");
		
		internal static void RegisterComponents()
		{

		}
		internal static void InitializeComponents()
		{
			//DB_table_shell.DropTable();
			//DB_table_code.DropTable();
            DB_table_shell.CreateTable();
			DB_table_code.CreateTable();
		}
	}
}
