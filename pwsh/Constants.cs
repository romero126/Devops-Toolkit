using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwsh
{
	internal static class Constants
	{
		// UI Elements

		internal static pwsh.UX UX;
		internal static pwsh.UI.Body UX_Body;
		internal static pwsh.UI.Editor UX_Editor;
		internal static pwsh.UI.Console UX_Console;

		


		//internal static pwsh.UI.Actionbar.Console Actionbar_Console;
		internal static pwsh.UI.Actionbar.Explorer Actionbar_Explorer;
		internal static pwsh.UI.Actionbar.Explorer_Consoles Actionbar_Explorer_Consoles;
		internal static pwsh.UI.Actionbar.Explorer_Cmdlets Actionbar_Explorer_Cmdlets;
		internal static pwsh.UI.Actionbar.Search Actionbar_Search;


		//internal static pwsh.UI.Tabs UI_Tabs;
		//internal static pwsh.UI.IDE UI_IDE;

		// Global Variables
		internal static core.DB.table DB_table_pwsh;
		internal static core.DB.table DB_table_pwsh_cmdlet;

		internal static List<object> DB_pwsh;
		internal static List<object> DB_pwsh_cmdlet;

		internal static List<object> UX_Helper_code;
		internal static List<object> UX_Helper_console;
		//internal static pwsh.DB.pwsh_cmdlet[] DB_pwsh_cmdlet;
		internal static void RegisterComponents() {
			
		}
		internal static void InitializeComponents() {
			core.DB.Core2.Init();

			/*
			DB.pwsh.SetType(typeof(DB.pwsh));
			DB.pwsh.Initialize();
			DB.pwsh.CreateTable();
			DB_pwsh = DB.pwsh.RefreshRows();
			DB.pwsh_cmdlet.SetType(typeof(DB.pwsh_cmdlet));
			DB.pwsh_cmdlet.Initialize();
			DB.pwsh_cmdlet.CreateTable();
			DB_pwsh_cmdlet = DB.pwsh.RefreshRows();
			*/

			
			DB_table_pwsh = new core.DB.table(typeof(DB.pwsh));
			DB_table_pwsh_cmdlet = new core.DB.table(typeof(DB.pwsh_cmdlet));
			DB_table_pwsh.CreateTable(); //Creates table for pwsh only if it doesnt already exists
			DB_table_pwsh_cmdlet.CreateTable(); //Creates table for pwsh_cmdlet only if it doesnt already exists
			DB_pwsh = DB_table_pwsh.RefreshRows(); // Refreshes query and populates values
			DB_pwsh_cmdlet = DB_table_pwsh_cmdlet.RefreshRows(); // Refreshes query and populates values
			
			Actionbar_Explorer_Consoles.UpdateDataContext();

		}
	}
}
